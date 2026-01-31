using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
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
