﻿// Copyright (c) 2016 Feenux LLC, All Rights Reserved.

// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Media.Imaging;

using TH_Configuration;
using TH_Global.Functions;
using TH_Plugins_Client;

using TH_DeviceCompare.Controls.DeviceDisplay;

namespace TH_DeviceCompare
{
    public class DeviceDisplay
    {
        /// <summary>
        /// Create a DeviceDisplay object for a Device Configuration
        /// Also loads DataItems (Cells) based on the 'plugins' and 'configs' arguments
        /// </summary>
        /// <param name="config">Device Configuration object to use</param>
        /// <param name="plugins">List of IClientPlugin objects to use for DataItems (Cells)</param>
        /// <param name="configs">List of PluginConfiguration objects to determine 'Enabled' state for each IClientPlugin</param>
        public DeviceDisplay(Configuration config, List<IClientPlugin> plugins, List<PluginConfiguration> configs)
        {
            Configuration = config;

            if (config != null)
            {
                // Set Unique ID
                UniqueId = config.UniqueId;

                // Create DeviceDisplay Components
                var header = new Header(config);
                var column = new Column(this);
                var overlay = new Overlay(config);

                // Setup Group of Components
                Group = new GroupInfo();
                Group.Header = header;
                Group.Column = column;
                Group.Overlay = overlay;

                // Load Plugins
                ProcessPlugins(plugins, configs);
            } 
        }

        const System.Windows.Threading.DispatcherPriority Priority_Background = System.Windows.Threading.DispatcherPriority.Background;

        const System.Windows.Threading.DispatcherPriority Priority_Context = System.Windows.Threading.DispatcherPriority.ContextIdle;

        public Configuration Configuration { get; set; }

        /// <summary>
        /// Unique Id for identifying which Device this DeviceDisplay related to
        /// </summary>
        public string UniqueId { get; set; }

        /// <summary>
        /// Group containing the Controls associated with this DeviceDisplay
        /// </summary>
        public GroupInfo Group { get; set; }

        public class GroupInfo
        {
            public Header Header { get; set; }

            public Column Column { get; set; }

            public Overlay Overlay { get; set; }
        }


        private ObservableCollection<Cell> _cells;
        /// <summary>
        /// Collection of Cell controls that represent each IClientPlugin
        /// </summary>
        public ObservableCollection<Cell> Cells
        {
            get
            {
                if (_cells == null) _cells = new ObservableCollection<Cell>();
                return _cells;
            }
            set
            {
                _cells = value;
            }
        }


        #region "Data"

        public bool Connected { get; set; }

        public int connectionAttempts { get; set; }
        public const int maxConnectionAttempts = 5;

        private BitmapImage warningImage;
        private BitmapImage connectionImage;

        public void UpdateData(DataEvent_Data de_d)
        {
            // Update Last Updated Timestamp
            if (Group.Header != null) Group.Header.LastUpdatedTimestamp = DateTime.Now.ToString();

            // Update Connection Status
            if (de_d.id.ToLower() == "statusdata_connection")
            {
                bool connected;
                bool.TryParse(de_d.data02.ToString(), out connected);

                // If connection attempt failed
                if (!connected)
                {
                    // Increment connection attempts
                    connectionAttempts++;

                    var overlay = Group.Overlay;
                    if (overlay != null)
                    {
                        // if still retrying
                        if (connectionAttempts < maxConnectionAttempts)
                        {
                            overlay.Loading = true;
                            overlay.ConnectionImage = null;
                            overlay.ConnectionStatus = "Retrying..." + Environment.NewLine + "Attempt #" + connectionAttempts.ToString();
                        }
                        // if max retries have been exceeded
                        else
                        {
                            overlay.Loading = false;

                            if (warningImage == null)
                            {
                                warningImage = new BitmapImage(new Uri("pack://application:,,,/TH_DeviceCompare;component/Resources/Warning_01_40px.png"));
                                warningImage.Freeze();
                            }

                            overlay.ConnectionImage = warningImage;
                            overlay.ConnectionStatus = "Could Not Connect To Database";
                        }
                    }

                    var header = Group.Header;
                    if (header != null)
                    {
                        header.Connected = false;
                    }

                    Connected = connected;
                }
                else
                {
                    // Reset connectionAttempts
                    connectionAttempts = 0;

                    if (connected != Connected)
                    {
                        var overlay = Group.Overlay;
                        if (overlay != null)
                        {
                            overlay.Loading = false;
                            overlay.ConnectionImage = null;
                            overlay.ConnectionStatus = null;
                        }

                        var header = Group.Header;
                        if (header != null)
                        {
                            header.Connected = true;
                        }

                        Connected = connected;
                    }
                }

                // Update Connected Property
                //if (Connected != connected) Connected = connected;
            }

            if (Connected)
            {
                // Availability Data
                if (de_d.id.ToLower() == "statusdata_availability")
                {
                    if (de_d.data02.GetType() == typeof(bool))
                    {
                        var overlay = Group.Overlay;
                        if (overlay != null)
                        {
                            bool avail = (bool)de_d.data02;
                            if (avail)
                            {
                                overlay.Loading = false;
                                overlay.ConnectionImage = null;
                                overlay.ConnectionStatus = null;
                            }
                            else
                            {
                                if (connectionImage == null)
                                {
                                    connectionImage = new BitmapImage(new Uri("pack://application:,,,/TH_DeviceCompare;component/Resources/Power_01.png"));
                                    connectionImage.Freeze();
                                }

                                overlay.ConnectionImage = connectionImage;
                                overlay.Loading = true;
                                overlay.ConnectionStatus = "Device Not Connected";
                            }
                        }
                    }
                }

                // Snapshot Table Data
                if (de_d.id.ToLower() == "statusdata_snapshots")
                {
                    // Update Header Data
                    if (Group.Header != null) Group.Header.UpdateData_Snapshots(de_d.data02);
                }

                // Variables Table Data
                if (de_d.id.ToLower() == "statusdata_variables")
                {
                    // Update Header Data
                    if (Group.Header != null) Group.Header.UpdateData_Variables(de_d.data02);
                }

                // Update Child Plugins
                UpdatePlugins(de_d);
            }
        }

        /// <summary>
        /// Update each Plugin related to each Cell
        /// </summary>
        /// <param name="de_d"></param>
        public void UpdatePlugins(DataEvent_Data de_d)
        {
            foreach (var cell in Cells)
            {
                var o = cell.Data;

                if (o != null)
                {
                    var plugin = (IClientPlugin)o;
                    plugin.Update_DataEvent(de_d);
                }
            }
        }

        #endregion

        #region "Plugins"

        public event RoutedEventHandler CellAdded;

        /// <summary>
        /// Initial load of all of the IClientPlugin objects
        /// </summary>
        /// <param name="plugins"></param>
        /// <param name="configs"></param>
        public void ProcessPlugins(List<IClientPlugin> plugins, List<PluginConfiguration> configs)
        {
            Cells.Clear();

            if (plugins != null)
            {
                foreach (var plugin in plugins)
                {
                    AddPlugin(plugin, configs);
                    //Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                    //{
                    //    var p = CreatePluginInstance(plugin, configs);

                    //    var cell = CreateCell(p);
                    //    if (cell != null) Cells.Add(cell);

                    //    if (CellAdded != null) CellAdded(this, new RoutedEventArgs());
                    //}), Priority_Context, new object[] { });
                }
            }
        }

        private IClientPlugin CreatePluginInstance(IClientPlugin plugin, List<PluginConfiguration> configs)
        {
            IClientPlugin result = null;

            var config = configs.Find(x => x.Name == plugin.Title);
            if (config != null)
            {
                if (config.Enabled)
                {
                    ConstructorInfo ctor = plugin.GetType().GetConstructor(BindingFlags.Instance | BindingFlags.Public, null, CallingConventions.HasThis, new Type[] { }, null);

                    Object_Functions.ObjectActivator<IClientPlugin> createdActivator = Object_Functions.GetActivator<IClientPlugin>(ctor);

                    result = createdActivator();
                }
            }

            return result;
        }

        private Cell CreateCell(IClientPlugin plugin)
        {
            var cell = new Cell();
            cell.Link = plugin.Title;
            cell.Index = Cells.Count;
            cell.Data = plugin;
            cell.SizeChanged += Cell_SizeChanged;
            return cell;
        }

        /// <summary>
        /// Add a single IClientPlugin object and assign it to a new Cell
        /// </summary>
        /// <param name="plugin"></param>
        /// <param name="configs"></param>
        public void AddPlugin(IClientPlugin plugin, List<PluginConfiguration> configs)
        {
            var config = configs.Find(x => x.Name == plugin.Title);
            if (config != null)
            {
                if (config.Enabled)
                {
                    Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        var p = CreatePluginInstance(plugin, configs);

                        var cell = CreateCell(p);
                        if (cell != null) Cells.Add(cell);

                        if (CellAdded != null) CellAdded(this, new RoutedEventArgs());
                    }), Priority_Context, new object[] { });
                }
            }
        }

        #endregion

        #region "Cell Size Changed"

        public delegate void CellSizeChanged_Handler(Cell sender);
        public event CellSizeChanged_Handler CellSizeChanged;

        /// <summary>
        /// Event Handler for when a Cell's size (height) is changed so other DeviceDisplays can change as well as the RowHeaders
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Cell_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (sender.GetType() == typeof(Cell))
            {
                Cell cell = (Cell)sender;
                if (CellSizeChanged != null) CellSizeChanged(cell);
            }
        }

        #endregion

    }
}
