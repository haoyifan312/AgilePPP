using System;
using System.Collections.Generic;
using System.Text;
using Payroll.Database;

namespace Payroll.Transactions
{
    internal class DeleteEmployeeTransaction: Transaction
    {
        private int _empId;

        public DeleteEmployeeTransaction(int empId)
        {
            _empId = empId;
        }

        public void Execute()
        {
            PayrollDatabase.GetInstance().DeleteEmployee(_empId);
        }
    }
}
