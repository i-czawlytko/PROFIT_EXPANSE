using System;
using System.Collections.Generic;

namespace DataAccess.Interfaces
{
    public interface IDataHandler
    {
        event Action DataChanged;
        int Balance { get;}
        int AddRecord(DataRecord rec);
        void DeleteRecord(DataRecord rec);
        void Export(string path);
        List<DataRecord> GetDataRecords();
    }
}