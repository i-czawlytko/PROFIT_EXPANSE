using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public enum OperationType
    {
        Profit,
        Expanse
    }

    public enum RoleType
    {
        Undefinded = 0,
        Admin = 1,
        User = 2       
    }
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string FullName { get; set; }
        public Role Role { get; set; }
    }

    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class  Recipient
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Data
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int Summ { get; set; }
        public Category Category { get; set; }
        public Recipient Recipient { get; set; }
        public OperationType OperationType { get; set; }
        public DateTime Inserted { get; set; }
    }

    public class UserRecord
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string FullName { get; set; }
        public string RoleName { get; set; }
        public RoleType RoleType { get; set; }
    }

    public class UserInfo
    {
        public string FullName { get; set; }
        public string RoleName { get; set; }
        public RoleType RoleType { get; set; }
        public string Login { get; set; }
    }
    public class DataRecord
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int Summ { get; set; }
        public string Category { get; set; }
        public string Recipient { get; set; }
        public OperationType Operation { get; set; }
        public string OperationName { get
            {
                if (this.Operation == OperationType.Profit) return "Доход";              
                else if (this.Operation == OperationType.Expanse)  return "Расход"; 
                else return "_";
            } 
        }
    }
}
