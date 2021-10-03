using DataAccess;
using DataAccess.Handlers;
using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Windows;

namespace WPFUI
{
    class DataViewModel : INotifyPropertyChanged
    {
        private int balance;
        public DataRecord SelectedDataRecord { get; set; }
        private DataModel model;
        public ObservableCollection<DataRecord> DataRecords { get; set; }
        RelayCommand addCommand;
        RelayCommand deleteCommand;
        RelayCommand exportCommand;
        RelayCommand newControlWindowCommand;

        public string UserFullName { get; set; }
        public string UserRole { get; set; }

        public string UserLogin { get; set; }

        public int Balance
        {
            get { return balance; }
            set
            {
                balance = value;
                OnPropertyChanged("Balance");
            }
        }
        public DataViewModel()
        {
            try
            {
                model = new DataModel();

                this.Balance = model.dataHandler.Balance;
                CheckBalance();

                this.UserFullName = model.userInfo.FullName;
                this.UserRole = model.userInfo.RoleName;
                this.UserLogin = model.userInfo.Login;

                DataRecords = new ObservableCollection<DataRecord>(model.dataHandler.GetDataRecords());

                model.dataHandler.DataChanged += updateView;
            }
            catch (SecurityException e)
            {
                MessageBox.Show(e.Message);
                Environment.Exit(0);
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
                          AddRecordWindow addRecordWindow = new AddRecordWindow();
                          if (addRecordWindow.ShowDialog() == true)
                          {
                              DataRecord rec = addRecordWindow.VM.DataRecord;
                              int record_id = model.dataHandler.AddRecord(rec);
                              rec.Id = record_id;
                              DataRecords.Add(rec);
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

        public RelayCommand NewControlWindowCommand
        {
            get
            {
                return newControlWindowCommand ??
                    (newControlWindowCommand = new RelayCommand((o) =>
                    {
                        ControlWindow controlWindow = new ControlWindow();
                    }));
            }
        }

        public RelayCommand ExportCommand
        {
            get
            {
                return exportCommand ??
                    (exportCommand = new RelayCommand((o) =>
                    {
                        try
                        {
                            ExportPathWindow exportPathWindow = new ExportPathWindow();
                            if (exportPathWindow.ShowDialog() == true)
                            {
                                model.dataHandler.Export(exportPathWindow.ExportPath);
                            }
                        }
                        catch(DirectoryNotFoundException e)
                        {
                            MessageBox.Show(e.Message);
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
                            if (SelectedDataRecord == null) return;
                            model.dataHandler.DeleteRecord(SelectedDataRecord);
                            DataRecords.Remove(SelectedDataRecord);
                        }
                        catch (SecurityException e)
                        {
                            MessageBox.Show(e.Message);
                        }                       
                    }));
            }
        }
        private void updateView()
        {
            this.Balance = model.dataHandler.Balance;
            CheckBalance();
        }

        private void CheckBalance()
        {
            if (this.Balance < 0)
            {
                MessageBox.Show("Отрицательный баланс");
            }
            else if (this.Balance == 0)
            {
                MessageBox.Show("Нулевой баланс");
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
