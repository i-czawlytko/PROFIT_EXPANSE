using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
using System.Text;
using DataAccess.Interfaces;
using DataAccess.Repositories;

namespace DataAccess.Handlers
{
    public abstract class BaseDataHandler : IDataHandler
    {
        public int Balance { get { return _balance; } }        
        public event Action DataChanged;
        private int _balance;
        private IExporter exporter;
        private IDataRepository repo;
        public const string securityErrorMessage = "Ошибка доступа (у пользрвателя недостаточно прав)";

        public BaseDataHandler(IDataRepository repository, IExporter exp)
        {
            repo = repository;
            exporter = exp;
            _balance = repo.GetBalance();
        }
        public virtual List<DataRecord> GetDataRecords()
        {
            return repo.GetDataRecords();
        }
        public virtual int AddRecord(DataRecord rec)
        {
            int record_id = repo.AddDataRecord(rec);
            DataChange();
            return record_id;
        }
        public virtual void DeleteRecord(DataRecord rec)
        {
            repo.DeleteDataRecord(rec);
            DataChange();
        }
        public virtual void Export(string path)
        {
            var data = repo.GetDataRecords();
            exporter.ExportDataRecords(data, path);
        }

        private void DataChange()
        {
            _balance = repo.GetBalance();
            DataChanged?.Invoke();
        }

    }

    public class UserDataHandler : BaseDataHandler
    {
        public UserDataHandler(IDataRepository repository, IExporter exp) : base(repository, exp) {}
        public override void DeleteRecord(DataRecord rec)
        {
            throw new SecurityException(securityErrorMessage);
        }
    }

    public class AdminDataHandler : BaseDataHandler
    {
        public AdminDataHandler(IDataRepository repository, IExporter exp) : base(repository, exp) { }
    }
}
