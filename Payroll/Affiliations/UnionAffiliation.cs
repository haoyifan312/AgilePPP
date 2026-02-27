using System;
using System.Collections.Generic;
using System.Text;
using Payroll.Classifications;
using Payroll.Transactions;

namespace Payroll.Affiliations
{
    internal class UnionAffiliation: Affiliation
    {
        public double Dues {  get; set; }
        public int MemberId { get; set; }

        private Dictionary<DateOnly, ServiceCharge> _serviceCharges;

        public UnionAffiliation(double dues, int memberId)
        {
            Dues = dues;
            MemberId = memberId;
            _serviceCharges = new Dictionary<DateOnly, ServiceCharge>();
        }

        public void AddServiceCharge(ServiceCharge serviceCharge)
        {
            _serviceCharges.Add(serviceCharge.Date, serviceCharge);
        }

        public ServiceCharge? GetServiceCharge(DateOnly date)
        {
            return _serviceCharges[date]; 
        }

        double Affiliation.CalculateDeductions(PayCheck pc)
        {
            int numberFridays = GetFridaysInPeriod(pc.PayBeginDay, pc.Payday);
            return Dues * numberFridays + GetServiceChangeInPeriod(pc.PayBeginDay, pc.Payday);
        }

        private double GetServiceChangeInPeriod(DateOnly payBeginDay, DateOnly payday)
        {
            double total = 0.0;
            foreach (var kvp in _serviceCharges)
            {
                var date = kvp.Key;
                if (date >= payBeginDay && date <= payday)
                    total += kvp.Value.Amount;
            }
            return total;
        }

        private int GetFridaysInPeriod(DateOnly payBeginDay, DateOnly payday)
        {
            int numberFridays = 0;
            for (DateOnly d = payBeginDay; d <= payday; d = d.AddDays(1))
            {
                if (d.DayOfWeek == DayOfWeek.Friday)
                    numberFridays += 1;
            }
            return numberFridays;
        }
    }
}
