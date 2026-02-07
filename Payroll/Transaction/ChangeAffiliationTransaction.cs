using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    internal abstract class ChangeAffiliationTransaction : Transaction
    {
        protected int _empId;

        public ChangeAffiliationTransaction(int empId)
        {
            _empId = empId;
        }

        public void Execute()
        {
            Employee? e = PayrollDatabase.GetInstance().GetEmployee(_empId);
            if (null == e)
                throw new Exception($"Failed to get employee :{_empId}");

            RecordMembership(e);

            Affiliation a = GetAffiliation();
            e.ItsAffiliation = a;
        }

        protected abstract void RecordMembership(Employee e);

        protected abstract Affiliation GetAffiliation();
    }
}
