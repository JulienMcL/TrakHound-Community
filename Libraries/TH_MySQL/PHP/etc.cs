﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Collections.Specialized;
using System.Data;
using System.Text;

using System.Threading;

using TH_Global;
using TH_Global.Web;

namespace TH_MySQL.PHP
{
    public static class etc
    {

        public static string CustomCommand(MySQL_Configuration config, string commandText)
        {
            string Result = null;

            NameValueCollection values = new NameValueCollection();
            if (config.Port > 0) values["server"] = config.Server + ":" + config.Port;
            else values["server"] = config.Server;

            values["user"] = config.Username;
            values["password"] = config.Password;
            values["db"] = config.Database;

            values["query"] = commandText;

            string PHP_Directory = "";
            if (config.PHP_Directory != "") PHP_Directory = "/" + config.PHP_Directory;

            string url = "http://" + config.PHP_Server + PHP_Directory + "/Retrieve.php";

            Result = HTTP.SendData(url, values);

            return Result;

        }

        //public static object[] CustomCommand(MySQL_Configuration config, string commandText)
        //{
        //    object[] Result = null;

        //    NameValueCollection values = new NameValueCollection();
        //    if (config.Port > 0) values["server"] = config.Server + ":" + config.Port;
        //    else values["server"] = config.Server;

        //    values["user"] = config.Username;
        //    values["password"] = config.Password;
        //    values["db"] = config.Database;

        //    values["query"] = commandText;

        //    string PHP_Directory = "";
        //    if (config.PHP_Directory != "") PHP_Directory = "/" + config.PHP_Directory;

        //    string url = "http://" + config.PHP_Server + PHP_Directory + "/Retrieve.php";

        //    Result = Web.HttpSendData(url, values);

        //    return Result;

        //}

        public static object GetValue(MySQL_Configuration config, string tablename, string column, string filterExpression)
        {

            object Result = null;

            NameValueCollection values = new NameValueCollection();
            if (config.Port > 0) values["server"] = config.Server + ":" + config.Port;
            else values["server"] = config.Server;

            values["user"] = config.Username;
            values["password"] = config.Password;
            values["db"] = config.Database;

            values["query"] = "SELECT " + column + " FROM " + tablename + " " + filterExpression;

            string PHP_Directory = "";
            if (config.PHP_Directory != "") PHP_Directory = "/" + config.PHP_Directory;


            string url = "http://" + config.PHP_Server + PHP_Directory + "/Retrieve.php";

            string responseString = HTTP.SendData(url, values);

            DataTable dt = JSON.ToTable(responseString);
            if (dt != null) if (dt.Rows.Count > 0) Result = dt.Rows[0][column];

            return Result;

        }

        public static DataTable GetGrants(MySQL_Configuration config, string username)
        {

            DataTable Result = null;

            NameValueCollection values = new NameValueCollection();
            if (config.Port > 0) values["server"] = config.Server + ":" + config.Port;
            else values["server"] = config.Server;

            values["server"] = config.Server;
            values["user"] = config.Username;
            values["password"] = config.Password;
            values["db"] = config.Database;

            values["query"] = "SHOW GRANTS FOR '" + username + "'@'%'";

            string PHP_Directory = "";
            if (config.PHP_Directory != "") PHP_Directory = "/" + config.PHP_Directory;



            string url = "http://" + config.PHP_Server + PHP_Directory + "/Retrieve.php";

            string responseString = HTTP.SendData(url, values);

            Result = JSON.ToTable(responseString);

            return Result;

        }

    }
}
