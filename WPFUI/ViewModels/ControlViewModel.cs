using DataAccess;
using DataAccess.Handlers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Linq;
using System.Security;
using System.Windows;
using System.Data.SqlClient;

namespace WPFUI
{
    class ControlViewModel : INotifyPropertyChanged
    {
        private BaseControlHandler controlHandler;
        public ObservableCollection<UserRecord> UserRecords { get; set; }
        public UserRecord SelectedUserRecord { get; set; }
        RelayCommand addCommand;
        RelayCommand deleteCommand;
        public ControlViewModel()
        {
            try
            {
                AuthHandler auth = new AuthHandler();
                UserInfo userInfo = auth.GetUserInfo();
                controlHandler = ControlHandlerFactory.CreateControlHandler(userInfo.RoleType);

                UserRecords = new ObservableCollection<UserRecord>(controlHandler.GetUserRecords());
            }
            catch (SecurityException e)
            {
                MessageBox.Show(e.Message);
            }

        }

        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand((o) =>
                  {
                      try
                      {
                          AddUserWindow addUserWindow = new AddUserWindow();
                          if (addUserWindow.ShowDialog() == true)
                          {
                              controlHandler.AddUser(addUserWindow.VM.UserRecord);
                              UserRecords.Clear();
                              controlHandler.GetUserRecords().ForEach(x => UserRecords.Add(x));
                          }
                      }
                      catch (SecurityException e)
                      {
                          MessageBox.Show(e.Message);
                      }
                      catch (SqlException)
                      {
                          MessageBox.Show("Введены неверные данные");
                      }
                  }));
            }
        }

        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ??
                    (deleteCommand = new RelayCommand((o) =>
                    {
                        try
                        {
                            if (SelectedUserRecord == null) return;
                            controlHandler.DeleteUser(SelectedUserRecord);
                            UserRecords.Remove(SelectedUserRecord);
                        }
                        catch (SecurityException e)
                        {
                            MessageBox.Show(e.Message);
                        }
                    }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
