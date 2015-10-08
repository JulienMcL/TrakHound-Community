﻿using System;
using System.Collections.Generic;

using System.Data;

using TH_Configuration;
using TH_Database;

namespace TH_MySQL
{
    public class Plugin : Database_Plugin
    {
        public string Name { get { return "MySQL Database Plugin"; } }

        public string Type { get { return "MYSQL"; } }

        string databasename;
        public string DatabaseName { get { return databasename; } }

        public void Initialize(Database_Configuration config)
        {
            if (config.Type.ToLower() == Type.ToLower())
            {
                MySQL_Configuration c = MySQL_Configuration.ReadXML(config.Node);
                
                config.Configuration = c;
            }
        }


        public bool Ping(Database_Configuration config)
        {
            return true;
        }

        // Database Functions -----------------------------------------------------------

        static string GetDatabaseName(MySQL_Configuration config)
        {
            return config.Database;
        }


        public bool Database_Create(object settings)
        {
            bool result = false;

            MySQL_Configuration config = MySQL_Configuration.Get(settings);
            if (config != null)
            {
                if (config.PHP_Server != null)
                {
                    result = PHP.Database.Create(config, config.Database);
                }
                else
                {
                    result =  Connector.Database.Create(config, config.Database);
                }
            }

            return result;
        }

        public bool Database_Drop(object settings)
        {
            bool result = false;

            MySQL_Configuration config = MySQL_Configuration.Get(settings);
            if (config != null)
            {
                if (config.PHP_Server != null)
                {
                    result = PHP.Database.Drop(config, config.Database);
                }
                else
                {
                    result = Connector.Database.Drop(config, config.Database);
                }
            }

            return result;
        }

        // ------------------------------------------------------------------------------


        // Table ------------------------------------------------------------------------

        public bool Table_Create(object settings, string tablename, ColumnDefinition[] columnDefinitions, string primaryKey) 
        {
            bool result = false;

            MySQL_Configuration config = MySQL_Configuration.Get(settings);
            if (config != null)
            {
                object[] coldefs = MySQL_Tools.ConvertColumnDefinitions(columnDefinitions);

                if (config.PHP_Server != null)
                {
                    result = PHP.Table.Create(config, tablename, coldefs, primaryKey);
                }
                else
                {
                    result = Connector.Table.Create(config, tablename, coldefs, primaryKey);
                }
            }

            return result; 
        }

        public bool Table_Drop(object settings, string tablename)
        {
            bool result = false;

            MySQL_Configuration config = MySQL_Configuration.Get(settings);
            if (config != null)
            {
                if (config.PHP_Server != null)
                {
                    result = PHP.Table.Drop(config, tablename);
                }
                else
                {
                    result = Connector.Table.Drop(config, tablename);
                }
            }

            return result;
        }

        public bool Table_Drop(object settings, string[] tablenames)
        {
            bool result = false;

            MySQL_Configuration config = MySQL_Configuration.Get(settings);
            if (config != null)
            {
                if (config.PHP_Server != null)
                {
                    result = PHP.Table.Drop(config, tablenames);
                }
                else
                {
                    result = Connector.Table.Drop(config, tablenames);
                }
            }

            return result;
        }

        public bool Table_Truncate(object settings, string tablename)
        {
            bool result = false;

            MySQL_Configuration config = MySQL_Configuration.Get(settings);
            if (config != null)
            {
                if (config.PHP_Server != null)
                {
                    result = PHP.Table.Truncate(config, tablename);
                }
                else
                {
                    result = Connector.Table.Truncate(config, tablename);
                }
            }

            return result;
        }

        public DataTable Table_Get(object settings, string tablename)
        {
            DataTable result = null;

            MySQL_Configuration config = MySQL_Configuration.Get(settings);
            if (config != null)
            {
                if (config.PHP_Server != null)
                {
                    result = PHP.Table.Get(config, tablename);
                }
                else
                {
                    result = Connector.Table.Get(config, tablename);
                }
            }

            return result;
        }

        public DataTable Table_Get(object settings, string tablename, string filterExpression)
        {
            DataTable result = null;

            MySQL_Configuration config = MySQL_Configuration.Get(settings);
            if (config != null)
            {
                if (config.PHP_Server != null)
                {
                    result = PHP.Table.Get(config, tablename, filterExpression);
                }
                else
                {
                    result = Connector.Table.Get(config, tablename, filterExpression);
                }
            }

            return result;
        }

        public DataTable Table_Get(object settings, string tablename, string filterExpression, string columns)
        {
            DataTable result = null;

            MySQL_Configuration config = MySQL_Configuration.Get(settings);
            if (config != null)
            {
                if (config.PHP_Server != null)
                {
                    result = PHP.Table.Get(config, tablename, filterExpression, columns);
                }
                else
                {
                    result = Connector.Table.Get(config, tablename, filterExpression, columns);
                }
            }

            return result;
        }

        public string[] Table_List(object settings)
        {
            string[] result = null;

            MySQL_Configuration config = MySQL_Configuration.Get(settings);
            if (config != null)
            {
                if (config.PHP_Server != null)
                {
                    result = PHP.Table.List(config);
                }
                else
                {
                    result = Connector.Table.List(config);
                }
            }

            return result;
        }

        public Int64 Table_GetRowCount(object settings, string tablename)
        {
            Int64 result = -1;

            MySQL_Configuration config = MySQL_Configuration.Get(settings);
            if (config != null)
            {
                if (config.PHP_Server != null)
                {
                    result = PHP.Table.GetRowCount(config, tablename);
                }
                else
                {
                    result = Connector.Table.RowCount(config, tablename);
                }
            }

            return result;
        }

        public Int64 Table_GetSize(object settings, string tablename)
        {
            Int64 result = -1;

            MySQL_Configuration config = MySQL_Configuration.Get(settings);
            if (config != null)
            {
                if (config.PHP_Server != null)
                {
                    result = PHP.Table.GetSize(config, tablename);
                }
                else
                {
                    result = Connector.Table.Size(config, tablename);
                }
            }

            return result;
        }

        // ------------------------------------------------------------------------------


        // Column -----------------------------------------------------------------------

        public List<string> Column_Get(object settings, string tablename)
        {
            List<string> result = null;

            MySQL_Configuration config = MySQL_Configuration.Get(settings);
            if (config != null)
            {
                if (config.PHP_Server != null)
                {
                    result = PHP.Column.Get(config, tablename);
                }
                else
                {
                    result = Connector.Column.Get(config, tablename);
                }
            }

            return result;
        }

        public bool Column_Add(object settings, string tablename, ColumnDefinition columnDefinition)
        {
            bool result = false;

            MySQL_Configuration config = MySQL_Configuration.Get(settings);
            if (config != null)
            {
                string coldef = MySQL_Tools.ConvertColumnDefinition(columnDefinition);

                if (config.PHP_Server != null)
                {
                    result = PHP.Column.Add(config, tablename, coldef);
                }
                else
                {
                    result = Connector.Column.Add(config, tablename, coldef);
                }
            }

            return result; 
        }

        // ------------------------------------------------------------------------------


        // Row --------------------------------------------------------------------------

        public bool Row_Insert(object settings, string tablename, object[] columns, object[] values, bool update)
        {
            bool result = false;

            MySQL_Configuration config = MySQL_Configuration.Get(settings);
            if (config != null)
            {
                if (config.PHP_Server != null)
                {
                    result = PHP.Row.Insert(config, tablename, columns, values, true);
                }
                else
                {
                    result = Connector.Row.Insert(config, tablename, columns, values, true);
                }
            }

            return result;
        }

        public bool Row_Insert(object settings, string tablename, object[] columns, List<List<object>> values, bool update)
        {
            bool result = false;

            MySQL_Configuration config = MySQL_Configuration.Get(settings);
            if (config != null)
            {
                if (config.PHP_Server != null)
                {
                    result = PHP.Row.Insert(config, tablename, columns, values, true);
                }
                else
                {
                    result = Connector.Row.Insert(config, tablename, columns, values, true);
                }
            }

            return result;
        }

        public bool Row_Insert(object settings, string tablename, List<object[]> columnsList, List<object[]> valuesList, bool update)
        {
            bool result = false;

            MySQL_Configuration config = MySQL_Configuration.Get(settings);
            if (config != null)
            {
                if (config.PHP_Server != null)
                {
                    //result = PHP.Row.Insert(config, tablename, columnsList, valuesList, true);
                }
                else
                {
                    result = Connector.Row.Insert(config, tablename, columnsList, valuesList, true);
                }
            }

            return result;
        }

        public bool Row_Insert(object settings, string query)
        {
            bool result = false;

            MySQL_Configuration config = MySQL_Configuration.Get(settings);
            if (config != null)
            {
                if (config.PHP_Server != null)
                {
                    result = PHP.Row.Insert(config, query);
                }
                else
                {
                    result = Connector.Row.Insert(config, query);
                }
            }

            return result;
        }


        public DataRow Row_Get(object settings, string tablename, string tableKey, string rowKey)
        {
            DataRow result = null;

            MySQL_Configuration config = MySQL_Configuration.Get(settings);
            if (config != null)
            {
                if (config.PHP_Server != null)
                {
                    result = PHP.Row.Get(config, tablename, tableKey, rowKey);
                }
                else
                {
                    result = Connector.Row.Get(config, tablename, tableKey, rowKey);
                }
            }

            return result;
        }

        public DataRow Row_Get(object settings, string tablename, string query)
        {
            DataRow result = null;

            MySQL_Configuration config = MySQL_Configuration.Get(settings);
            if (config != null)
            {
                if (config.PHP_Server != null)
                {
                    result = PHP.Row.Get(config, tablename, query);
                }
                else
                {
                    result = Connector.Row.Get(config, tablename, query);
                }
            }

            return result;
        }


        public bool Row_Exists(object settings, string tablename, string filterString)
        {
            bool result = false;

            MySQL_Configuration config = MySQL_Configuration.Get(settings);
            if (config != null)
            {
                if (config.PHP_Server != null)
                {
                    result = PHP.Row.Exists(config, tablename, filterString);
                }
                else
                {
                    result = Connector.Row.Exists(config, tablename, filterString);
                }
            }

            return result;
        }

        // ------------------------------------------------------------------------------


        // Etc --------------------------------------------------------------------------

        public object[] CustomCommand(object settings, string commandText)
        {
            object[] result = null;

            MySQL_Configuration config = MySQL_Configuration.Get(settings);
            if (config != null)
            {
                if (config.PHP_Server != null)
                {
                    result = PHP.etc.CustomCommand(config, commandText);
                }
                else
                {
                    result = Connector.etc.CustomCommand(config, commandText);
                }
            }

            return result;
        }

        public object GetValue(object settings, string tablename, string column, string filterExpression)
        {
            object result = null;

            MySQL_Configuration config = MySQL_Configuration.Get(settings);
            if (config != null)
            {
                if (config.PHP_Server != null)
                {
                    result = PHP.etc.GetValue(config, tablename, column, filterExpression);
                }
                else
                {
                    result = Connector.etc.GetValue(config, tablename, column, filterExpression);
                }
            }

            return result;
        }

        public DataTable GetGrants(object settings, string username)
        {
            DataTable result = null;

            MySQL_Configuration config = MySQL_Configuration.Get(settings);
            if (config != null)
            {
                if (config.PHP_Server != null)
                {
                    result = PHP.etc.GetGrants(config, username);
                }
                else
                {
                    result = Connector.etc.GetGrants(config, username);
                }
            }

            return result;
        }

        // ------------------------------------------------------------------------------

    }
}
