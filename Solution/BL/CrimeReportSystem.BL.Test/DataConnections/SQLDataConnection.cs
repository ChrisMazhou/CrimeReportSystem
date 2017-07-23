using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrimeReportSystem.BL.Context;

namespace CrimeReportSystem.BL.Test.DataConnections
{
    [ExcludeFromCodeCoverage]
    public class SQLDataConnection : IDisposable, ITestDataConnection
    {
        private string _DatabaseName
        {
            get
            {
                var conStr = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"];
                System.Data.SqlClient.SqlConnectionStringBuilder builder = new System.Data.SqlClient.SqlConnectionStringBuilder(conStr.ConnectionString);
                return builder.InitialCatalog;
            }
        }

        private string _SQLServerName
        {
            get
            {
                var conStr = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"];
                System.Data.SqlClient.SqlConnectionStringBuilder builder = new System.Data.SqlClient.SqlConnectionStringBuilder(conStr.ConnectionString);
                return builder.DataSource;
            }
        }

        private string _TestUserName
        {
            get
            {
                var conStr = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"];
                System.Data.SqlClient.SqlConnectionStringBuilder builder = new System.Data.SqlClient.SqlConnectionStringBuilder(conStr.ConnectionString);
                return builder.UserID;
            }
        }

        private string _TestPassword
        {
            get
            {
                var conStr = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"];
                System.Data.SqlClient.SqlConnectionStringBuilder builder = new System.Data.SqlClient.SqlConnectionStringBuilder(conStr.ConnectionString);
                return builder.Password;
            }
        }
        
        private string _ConnectionString = "";

        private DbConnection _db;
        public DataContext Context { get; set; }
        public DbConnection DataBase { get { return _db; } }

        public string SQLServerName
        {
            get
            {
                return _SQLServerName;
            }
        }

        public string DBConnectionString
        {
            get
            {
                return _ConnectionString;
            }
        }

        public SQLDataConnection(bool teardownDatabase = true)
        {
            var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;

            if (teardownDatabase)
                TearDownDatabase();

            _ConnectionString = String.Format("Data Source={0};Initial Catalog={1};Persist Security Info=False;User ID={2};Password={3}",
                _SQLServerName, 
                _DatabaseName,
                _TestUserName,
                _TestPassword);
            using (DbConnection setupCon = new SqlConnection(_ConnectionString))
            {
                DataContext.Setup(setupCon, false);
            }

            _db = new SqlConnection(_ConnectionString);
            Context = new DataContext(_db);
        }

        private void TearDownDatabase()
        {
            string conStr = String.Format("Data Source={0};Initial Catalog={1};Persist Security Info=False;User ID={2};Password={3}", SQLServerName, "MASTER",
                _TestUserName,
                _TestPassword);
            using (DbConnection setupCon = new SqlConnection(conStr))
            {
                setupCon.Open();
                //try force single connection mode
                try
                {
                    using (var dbCommand = setupCon.CreateCommand())
                    {
                        dbCommand.CommandText = "ALTER DATABASE [" + _DatabaseName + "] SET SINGLE_USER WITH ROLLBACK IMMEDIATE";
                        dbCommand.ExecuteNonQuery();
                    }
                }
                catch
                {
                }

                try
                {
                    using (var dbCommand = setupCon.CreateCommand())
                    {
                        dbCommand.CommandText = "USE master DROP DATABASE [" + _DatabaseName + "]";
                        dbCommand.ExecuteNonQuery();
                    }
                }
                catch
                {
                }
            }
        }

        public void Dispose()
        {
            this.Context.Dispose();
            this._db.Dispose();
            SqlConnection.ClearAllPools();
        }



    }
}
