using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll.Transaction
{
    internal class DeleteEmployeeTransaction: Transaction
    {
        private int _empId;

        public DeleteEmployeeTransaction(int empId):
            base()
        {
            _empId = empId;
        }

        public void Execute()
        {
            PayrollDatabase.GetInstance().DeleteEmployee(_empId);
        }
    }
}
