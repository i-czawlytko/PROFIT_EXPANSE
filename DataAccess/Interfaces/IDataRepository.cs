using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Interfaces
{
    public interface IDataRepository
    {
        public List<DataRecord> GetDataRecords();
        public int AddDataRecord(DataRecord rec);
        public void DeleteDataRecord(DataRecord rec);

        public int GetBalance();
    }
}
