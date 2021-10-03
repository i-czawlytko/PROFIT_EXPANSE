using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Interfaces
{
    public interface IExporter
    {
        public void ExportDataRecords(List<DataRecord> records, string path);

    }
}
