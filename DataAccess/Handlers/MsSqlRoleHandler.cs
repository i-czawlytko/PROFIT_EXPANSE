using Dapper;
using DataAccess.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace DataAccess.Handlers
{
    public class MsSqlRoleHandler : IRoleHandler
    {
        private IConfigurationRoot configuration;
        private IDbConnection db;
        public MsSqlRoleHandler()
        {
            db = new SqlConnection(getConnectionString());
        }
        public UserInfo GetUserInfo(string userName)
        {
            string sql = "SELECT * from [USER] u JOIN [ROLE] r ON r.[Id] = u.[RoleId] WHERE u.Login = @userName";
            User user = db.Query<User, Role, User>(
                    sql,
                    (user, role) =>
                    {
                        user.Role = role;
                        return user;
                    },
                    new { userName }).FirstOrDefault();

            int numOfUsers = db.ExecuteScalar<int>("SELECT COUNT(*) FROM [USER]");
            if (numOfUsers == 0)
            {
                return new UserInfo { FullName = "_noreg", RoleName = "Не добавлены пользователи", RoleType = RoleType.Admin, Login = userName };
            }


            if (user == null)
            {
                return new UserInfo {FullName = "_noname", RoleName = "_norole" , RoleType = RoleType.Undefinded, Login = userName };
            }
            else
            {
                return new UserInfo {FullName = user.FullName,RoleName = user.Role.Name, RoleType = (RoleType)user.Role.Id, Login = userName };
            }
            
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
