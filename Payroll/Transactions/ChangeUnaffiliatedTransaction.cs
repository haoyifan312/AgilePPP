using Payroll.Database;
using Payroll.Affiliations;

using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll.Transactions
{
    internal class ChangeUnaffiliatedTransaction : ChangeAffiliationTransaction
    {
        public ChangeUnaffiliatedTransaction(int empId) : 
            base(empId)
        {
        }

        protected override Affiliation GetAffiliation()
        {
            return new NoAffiliation();
        }

        protected override void RecordMembership(Employee e)
        {
            var ua = (UnionAffiliation)e.ItsAffiliation;
            if (ua != null)
            {
                int memberId = ua.MemberId;
                PayrollDatabase.GetInstance().DeleteUnionMember(memberId);
            }
        }
    }
}
