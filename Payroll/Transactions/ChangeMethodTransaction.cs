using Payroll.Database;
using Payroll.Methods;
using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll.Transactions
{
    internal abstract class ChangeMethodTransaction : Transaction
    {
        protected int _empId;

        public ChangeMethodTransaction(int empId)
        {
            _empId = empId;
        }

        public void Execute()
        {
            Employee? e = PayrollDatabase.GetInstance().GetEmployee(_empId);
            if (null == e)
                throw new Exception($"Failed to get employee: {_empId}");
            
            PaymentMethod pm = GetMethod();
            e.ItsPaymentMethod = pm;
        }

        protected abstract PaymentMethod GetMethod();
    }
}
