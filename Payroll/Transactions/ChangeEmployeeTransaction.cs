using Payroll.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll.Transactions
{
    internal abstract class ChangeEmployeeTransaction: Transaction
    {
        protected int empId;

        public ChangeEmployeeTransaction(int  empId)
        {
            this.empId = empId;
        }

        public void Execute()
        {
            Employee? e = PayrollDatabase.GetInstance().GetEmployee(empId);
            if (null == e)
                throw new Exception($"Fail to get employee with id: {empId}");
            Change(e);
        }

        protected abstract void Change(Employee e);
    }
}
