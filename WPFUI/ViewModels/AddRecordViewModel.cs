using DataAccess;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace WPFUI
{
    public class AddRecordViewModel
    {
        public DataRecord DataRecord { get; private set; }
        public List<Op> OperationList { get; private set; }

        public AddRecordViewModel()
        {
            DataRecord = new DataRecord { Operation = OperationType.Expanse };

            OperationList = new List<Op>
            {
                new Op {OperationType = OperationType.Expanse, OperationName = "Расход"},
                new Op {OperationType = OperationType.Profit, OperationName = "Доход"}
            };

        }
    }
    public class Op
    {
        public OperationType OperationType { get; set; }
        public string OperationName { get; set; }
    }
}
