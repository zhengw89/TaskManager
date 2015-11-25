using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Configuration;
using TaskManager.DB;

namespace TaskManager.Helper
{
    internal class WebConfigHelper
    {
        private static WebConfigHelper _instance;
        private static readonly object SyncObj = new object();
        private readonly Dictionary<string, string> _dict = new Dictionary<string, string>();

        public static WebConfigHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (SyncObj)
                    {
                        if (_instance == null)
                        {
                            _instance = new WebConfigHelper();
                        }
                    }
                }

                return _instance;
            }
        }

        public DbInfo DbInfo { get; private set; }

        private WebConfigHelper()
        {
            Refresh();
        }

        public void Refresh()
        {
            foreach (string key in WebConfigurationManager.AppSettings.AllKeys)
                _dict.Add(key, WebConfigurationManager.AppSettings[key]);

            RefreshDbConfigInfo();
        }

        private void RefreshDbConfigInfo()
        {
            var dict = ConfigurationManager.GetSection(ConstString.DatabaseInfo) as IDictionary;
            if (dict != null)
            {
                DbInfo = new DbInfo(dict);
            }
        }
    }

    internal class DbInfo
    {
        private const string MySqlConncetionProvider = "MYSQL.DATA.MYSQLCLIENT", SqlServerConnectionProvider = "SYSTEM.DATA.SQLCLIENT";

        public string ConnectinString { get; private set; }
        public string ConnectionProvider { get; private set; }
        public TaskManagerDbType DbType { get; set; }

        public DbInfo(IDictionary dictionary)
        {
            ConnectinString = dictionary[ConstString.ConnectionString].ToString();
            ConnectionProvider = dictionary[ConstString.ConnectionProvider].ToString();

            switch (ConnectionProvider.ToUpper())
            {
                case SqlServerConnectionProvider:
                    this.DbType = TaskManagerDbType.SqlServer;
                    break;
                case MySqlConncetionProvider:
                    this.DbType = TaskManagerDbType.MySql;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    internal class ConstString
    {
        #region DatabaseInfoConfig

        public const string DatabaseInfo = "DatabaseInfo";

        public const string ConnectionString = "ConnectionString";
        public const string ConnectionProvider = "ConnectionProvider";

        #endregion
    }
}