using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using DataAccess.Interfaces;

namespace DataAccess.Handlers
{
    public class MsSqlDBInit : IDBInit
    {
        private IConfigurationRoot configuration;
        private IDbConnection db;
        public void Init()
        {
            string filepath = Path.Combine(Directory.GetParent(AppContext.BaseDirectory).FullName, "INIT.sql");
            string Sql = File.ReadAllText(filepath, Encoding.Default);

            db = new SqlConnection(getConnectionString());

            db.Execute(Sql);
            db.Close();

        }

        private string getConnectionString()
        {
            configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();
            return configuration.GetConnectionString("Connection");
        }
    }
}
