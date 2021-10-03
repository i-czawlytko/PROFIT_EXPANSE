using DataAccess;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace WPFUI
{
    public class AddUserViewModel
    {
        public UserRecord UserRecord { get; private set; }
        public List<Status> StatusList { get; private set; }

        public AddUserViewModel()
        {
            UserRecord = new UserRecord { RoleType = RoleType.User };

            StatusList = new List<Status>
            {
                new Status {RoleType = RoleType.User, RoleName = "Пользователь"},
                new Status {RoleType = RoleType.Admin, RoleName = "Администратор"}
            };

        }
    }
    public class Status
    {
        public RoleType RoleType { get; set; }
        public string RoleName { get; set; }
    }
}
