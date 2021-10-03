using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace DataAccess.Handlers
{
    public static class DBInit
    {
        private static IConfigurationRoot configuration;
        private static IDbConnection db;
        public static void Init()
        {
            string filepath = Path.Combine(Directory.GetParent(AppContext.BaseDirectory).FullName, "INIT.sql");
            string Sql = File.ReadAllText(filepath, Encoding.Default);

            db = new SqlConnection(getConnectionString());

            db.Execute(Sql);
            db.Close();

        }

        private static string getConnectionString()
        {
            configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();
            return configuration.GetConnectionString("Connection");
        }
    }
}
