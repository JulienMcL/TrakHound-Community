﻿// Copyright (c) 2015 Feenux LLC, All Rights Reserved.

// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;
using System.Collections.Generic;
using System.Linq;

using TH_Configuration;
using TH_GeneratedData;

namespace TH_ShiftTable
{
    public class GenEventShiftItem
    {
        public GenEventShiftItem() { CaptureItems = new List<TH_GeneratedData.GeneratedData.GeneratedEvents.CaptureItem>(); }

        public string eventName { get; set; }
        public string eventValue { get; set; }
        public int eventNumVal { get; set; }

        public string shiftName { get; set; }

        public Segment segment { get; set; }

        public ShiftTime start { get; set; }
        public ShiftTime end { get; set; }

        public DateTime start_timestamp { get; set; }
        public DateTime end_timestamp { get; set; }

        public ShiftDate shiftDate { get; set; }

        public TimeSpan duration { get; set; }

        public List<TH_GeneratedData.GeneratedData.GeneratedEvents.CaptureItem> CaptureItems;

        public static List<GenEventShiftItem> Get(Configuration config, List<GeneratedData.GeneratedEventItem> genEventItems)
        {
            List<GenEventShiftItem> Result = new List<GenEventShiftItem>();

            ShiftConfiguration sc = ShiftConfiguration.Get(config);
            if (sc != null)
            {
                List<ListInfo> nameInfos = GetListByName(genEventItems);

                foreach (ListInfo nameInfo in nameInfos)
                {
                    GeneratedData.GeneratedEventItem previousItem = previousItems.Find(x => x.eventName.ToLower() == nameInfo.title.ToLower());

                    for (int i = 0; i <= nameInfo.genEventItems.Count - 1; i++)
                    {
                        GeneratedData.GeneratedEventItem item = nameInfo.genEventItems[i];

                        if (previousItem != null)
                        {
                            // Skip items that are not newer (test to see if any real data is lost due to this)
                            if (item.timestamp > previousItem.timestamp)
                            {
                                List<GenEventShiftItem> items = GetItems(sc, previousItem, item);
                                Result.AddRange(items);
                                previousItem = item;
                            }
                        }
                        else
                        {
                            previousItems.Add(item);
                            previousItem = item;
                        }

                        int prevIndex = previousItems.FindIndex(x => x.eventName.ToLower() == nameInfo.title.ToLower());
                        if (prevIndex >= 0) previousItems[prevIndex] = previousItem;
                    }
                }
            }

            return Result;

        }

        // Used to hold information between Samples
        public static List<GeneratedData.GeneratedEventItem> previousItems;

        #region "Private"

        class ListInfo
        {
            public ListInfo() { genEventItems = new List<GeneratedData.GeneratedEventItem>(); }

            public string title { get; set; }
            public List<GeneratedData.GeneratedEventItem> genEventItems { get; set; }
        }

        static List<GenEventShiftItem> GetItems(ShiftConfiguration stc, GeneratedData.GeneratedEventItem item1, GeneratedData.GeneratedEventItem item2)
        {
            List<GenEventShiftItem> Result = new List<GenEventShiftItem>();

            ShiftDate date1 = new ShiftDate(item1.timestamp);
            ShiftDate date2 = new ShiftDate(item2.timestamp);

            int daySpan = date2 - date1;

            for (int x = 0; x <= daySpan; x++)
            {
                DateTime dt = new DateTime(date1.year, date1.month, date1.day);
                ShiftDate date = new ShiftDate(dt.AddDays(x), false);

                foreach (Shift shift in stc.shifts)
                {
                    Result.AddRange(GetItemsDuringShift(item1, item2, date, shift));
                }
            }

            return Result;
        }

        static List<ListInfo> GetListByName(List<GeneratedData.GeneratedEventItem> genEventItems)
        {
            List<ListInfo> Result = new List<ListInfo>();

            // Get a list of all of the distinct (by Event Name) genEventItems
            IEnumerable<string> distinctNames = genEventItems.Select(x => x.eventName).Distinct();

            // Loop through the distinct event names  
            foreach (string distinctName in distinctNames.ToList())
            {
                ListInfo info = new ListInfo();
                info.title = distinctName;
                info.genEventItems = genEventItems.FindAll(x => x.eventName.ToLower() == distinctName.ToLower());
                Result.Add(info);
            }

            return Result;
        }

        static List<GenEventShiftItem> GetItemsDuringShift(GeneratedData.GeneratedEventItem item1, GeneratedData.GeneratedEventItem item2, ShiftDate date, Shift shift)
        {
            List<GenEventShiftItem> Result = new List<GenEventShiftItem>();

            foreach (Segment segment in shift.segments)
            {
                GenEventShiftItem gesi = GetItemDuringSegment(item1, item2, date, segment);
                if (gesi != null) Result.Add(gesi);
            }

            return Result;
        }

        static GenEventShiftItem GetItemDuringSegment(GeneratedData.GeneratedEventItem item1, GeneratedData.GeneratedEventItem item2, ShiftDate date, Segment segment)
        {
            GenEventShiftItem Result = null;

            // -1 = Timestamp does not fall within segment          
            //  0 = Timestamps span entire segment
            //  1 = Start timestamp is more than segment.beginTime
            //  2 = End timestamp is less than segment.endTime
            //  3 = Both timestamps are within segment
            int type = GetItemDuringSegmentType(item1, item2, segment, date);

            if (type >= 0)
            {
                GenEventShiftItem gesi = new GenEventShiftItem();
                gesi.eventName = item1.eventName;
                gesi.eventValue = item1.value;
                gesi.eventNumVal = item1.numval;

                gesi.shiftName = segment.shift.name;
                gesi.segment = segment;

                gesi.CaptureItems = item1.CaptureItems;

                //ShiftDate d = date;
                ////if (segment.beginDayOffset > 0 && 
                ////    segment.endDayOffset > 0 &&
                ////    new ShiftDate(item1.timestamp.ToLocalTime()) == new ShiftDate(item2.timestamp.ToLocalTime()))
                ////{
                ////    d = d - segment.endDayOffset;
                ////} 
 
                //d = d - segment.endDayOffset;
                //gesi.shiftDate = d;

                // Get Times for segment and convert timestamps to Local
                SegmentShiftTimes sst = SegmentShiftTimes.Get(item1.timestamp, item2.timestamp, date, segment);

                gesi.shiftDate = date - segment.endTime.dayOffset;

                // Calculate Start and End timestamps based on the DuringSegmentType
                switch (type)
                {
                    case 0:
                        gesi.start_timestamp = sst.segmentStart;
                        gesi.end_timestamp = sst.segmentEnd;
                        break;

                    case 1:
                        gesi.start_timestamp = sst.start;
                        gesi.end_timestamp = sst.segmentEnd;
                        break;

                    case 2:
                        gesi.start_timestamp = sst.segmentStart;
                        gesi.end_timestamp = sst.end;
                        break;

                    case 3:
                        gesi.start_timestamp = sst.start;
                        gesi.end_timestamp = sst.end;
                        break;
                }

                // Insure that end is not less than start
                // Example ------------
                // start_timestamp = 11:50 PM 9/28/2015
                // end_timestamp   = 12:10 AM 9/28/2015
                // --------------------
                //if (gesi.end_timestamp < gesi.start_timestamp) gesi.end_timestamp = gesi.end_timestamp.AddDays(1);

                // Calculate duration of GeneratedEventShiftItem
                TimeSpan duration = gesi.end_timestamp - gesi.start_timestamp;
                gesi.duration = duration;

                Result = gesi;
            }

            return Result;
        }

        static int GetItemDuringSegmentType(GeneratedData.GeneratedEventItem item1, GeneratedData.GeneratedEventItem item2, Segment segment, ShiftDate date)
        {
            // Get Times for segment and convert timestamps to Local
            SegmentShiftTimes sst = SegmentShiftTimes.Get(item1.timestamp, item2.timestamp, date, segment);

            int type = -1;

            // Timestamp does not fall within segment
            if ((sst.start < sst.segmentStart && sst.end < sst.segmentStart) || (sst.start > sst.segmentEnd && sst.end > sst.segmentEnd))
                type = -1;

            // Timestamps span entire segment
            else if (sst.start <= sst.segmentStart && sst.end >= sst.segmentEnd)
                type = 0;

            // Start timestamp is more than segment.beginTime
            else if ((sst.start >= sst.segmentStart && sst.start < sst.segmentEnd) && sst.end > sst.segmentEnd)
                type = 1;

            // End timestamp is less than segment.endTime
            else if (sst.start <= sst.segmentStart && (sst.end < sst.segmentEnd && sst.end > sst.segmentStart))
                type = 2;

            // Both timestamps are within segment
            else if ((sst.start >= sst.segmentStart && sst.start < sst.segmentEnd) && sst.end < sst.segmentEnd)
                type = 3;

            return type;
        }

        #endregion

    }

    public class GeneratedEventConfiguration
    {
        public string name { get; set; }
    }
}
