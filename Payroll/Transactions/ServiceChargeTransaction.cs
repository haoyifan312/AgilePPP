using Payroll.Database;
using Payroll.Affiliations;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Payroll.Transactions
{
    internal class ServiceCharge
    {
        public DateOnly Date { get; set; }
        public double Amount { get; set; }
        public ServiceCharge(DateOnly date, double amount)
        {
            Date = date;
            Amount = amount;
        }
    }


    internal class ServiceChargeTransaction : Transaction
    {
        public DateOnly Date { get; set; }
        public double Amount { get; set; }
        public int MemberID { get; set; }

        public ServiceChargeTransaction(int memberID, DateOnly date, double amount)
        {
            Date = date;
            Amount = amount;
            MemberID = memberID;
        }

        public void Execute()
        {
            Employee? e = PayrollDatabase.GetInstance().GetUnionMember(MemberID);
            if (e == null)
                throw new Exception($"No union member exist for {MemberID}");

            var ua = (UnionAffiliation)e.ItsAffiliation;
            if (ua != null)
            {
                var sc = new ServiceCharge(Date, Amount);
                ua.AddServiceCharge(sc);
            }
            else
            {
                throw new Exception($"Employee {e.Name} is not in Union Affiliation");
            }
        }
    }
}
