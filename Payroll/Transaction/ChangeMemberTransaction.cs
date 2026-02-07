using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    internal class ChangeMemberTransaction : ChangeAffiliationTransaction
    {
        public int MemberId { get; set; }
        public double Dues { get; set; }

        public ChangeMemberTransaction(int empId, int memberId, double dues):
            base(empId)
        {
            MemberId = memberId;
            Dues = dues;
        }

        protected override Affiliation GetAffiliation()
        {
            return new UnionAffiliation(Dues, MemberId);
        }

        protected override void RecordMembership(Employee e)
        {
            PayrollDatabase.GetInstance().AddUnionMember(MemberId, e);
        }
    }
}
