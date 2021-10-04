using DataAccess.Interfaces;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DataAccess.Handlers
{
    public class ExcelExporter : IExporter
    {
        public void ExportDataRecords(List<DataRecord> records, string folderPath)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            if (!Directory.Exists(folderPath)) throw new DirectoryNotFoundException("Указанная директория отсутствует!");
            
            string path = Path.Combine(folderPath, "records.xlsx");

            FileInfo f = new FileInfo(path);
            if (f.Exists) f.Delete();
            using (ExcelPackage ep = new ExcelPackage(f))
            {
                ExcelWorksheet sch = ep.Workbook.Worksheets.Add("Schedule");
                sch.Cells[1, 1].Value = "ID"; ;
                sch.Cells[1, 2].Value = "Сумма";
                sch.Cells[1, 3].Value = "Категория";
                sch.Cells[1, 4].Value = "Получатель";
                sch.Cells[1, 5].Value = "Операция";

                int counter = 2;
                foreach (var r in records)
                {
                    sch.Cells[counter, 1].Value = r.Id;
                    sch.Cells[counter, 2].Value = r.Summ;
                    sch.Cells[counter, 3].Value = r.Category;
                    sch.Cells[counter, 4].Value = r.Recipient;
                    sch.Cells[counter, 5].Value = r.OperationName;
                    counter++;
                }
                ep.Save();
            }
        }
    }
}
