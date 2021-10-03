using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using Dapper;

using Microsoft.Extensions.Configuration;
using DataAccess.Interfaces;
using System.Data.SqlClient;

namespace DataAccess.Repositories
{
    public class MsSqlDataRepository : IDataRepository
    {
        private IConfigurationRoot configuration;
        private IDbConnection db;
        public MsSqlDataRepository()
        {
            db = new SqlConnection(getConnectionString());           
        }
        public List<DataRecord> GetDataRecords()
        {
            string sql = "SELECT * from [DATA] d JOIN [CATEGORY] c ON c.[Id] = d.[CategoryId] JOIN [RECIPIENT] r ON r.[Id] = d.[RecipientId];";
            var data = db.Query<Data, Category, Recipient, Data>(
                    sql,
                    (data, category, recipient) =>
                    {
                        data.Category = category;
                        data.Recipient = recipient;
                        return data;
                    }).ToList();
            return data.Select(x => new DataRecord { Id = x.Id,
                Category = x.Category.Name,
                Recipient = x.Recipient.Name,
                Description = x.Description,
                Summ = x.Summ,
                Operation = x.OperationType
                }).ToList();
        }
        public int AddDataRecord(DataRecord rec)
        {
            Category cat = db.Query<Category>("SELECT * FROM CATEGORY WHERE Name = @Category", new { rec.Category }).FirstOrDefault();
            int category_id;
            if (cat == null)
            {
                var _sqlQuery = "INSERT INTO CATEGORY (Name) VALUES(@Category); SELECT CAST(SCOPE_IDENTITY() as int)";
                category_id = db.Query<int>(_sqlQuery, new { rec.Category }).FirstOrDefault();
            }
            else
            {
                category_id = cat.Id;
            }

            Recipient rcp = db.Query<Recipient>("SELECT * FROM RECIPIENT WHERE Name = @Recipient", new { rec.Recipient }).FirstOrDefault();
            int recipient_id;
            if (rcp == null)
            {
                var _sqlQuery = "INSERT INTO RECIPIENT (Name) VALUES(@Recipient); SELECT CAST(SCOPE_IDENTITY() as int)";
                recipient_id = db.Query<int>(_sqlQuery, new { rec.Recipient }).FirstOrDefault();
            }
            else
            {
                recipient_id = rcp.Id;
            }

            object data = new
            {
                Summ = rec.Summ,
                Description = rec.Description,
                CategoryId = category_id,
                RecipientId = recipient_id,
                OperationType = (int)rec.Operation,
                Inserted = DateTime.Now
            };

            var sqlQuery = "INSERT INTO DATA (Summ, Description, CategoryId, RecipientId, OperationType, Inserted) VALUES(@Summ, @Description, @CategoryId, @RecipientId, @OperationType, @Inserted); SELECT CAST(SCOPE_IDENTITY() as int)";
            int record_id = db.Query<int>(sqlQuery, data).FirstOrDefault();
            return record_id;
        }
        public void DeleteDataRecord(DataRecord rec)
        {
            var sqlQuery = "DELETE FROM DATA WHERE Id = @Id";
            db.Execute(sqlQuery, new { rec.Id });
        }

        public int GetBalance()
        {
            int profitSum = db.ExecuteScalar<int>("SELECT SUM(Summ) FROM [DATA] WHERE OperationType = 0;");
            int expanseSum = db.ExecuteScalar<int>("SELECT SUM(Summ) FROM [DATA] WHERE OperationType = 1;");
            return profitSum - expanseSum;
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
