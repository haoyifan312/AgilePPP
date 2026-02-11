using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    internal class UnionAffiliation: Affiliation
    {
        public double Dues {  get; set; }
        public int MemberId { get; set; }

        private Dictionary<int, ServiceCharge> _serviceCharges;

        public UnionAffiliation(double dues, int memberId)
        {
            Dues = dues;
            MemberId = memberId;
            _serviceCharges = new Dictionary<int, ServiceCharge>();
        }

        public void AddServiceCharge(ServiceCharge serviceCharge)
        {
            _serviceCharges.Add(serviceCharge.Date, serviceCharge);
        }

        public ServiceCharge? GetServiceCharge(int date)
        {
            return _serviceCharges[date]; 
        }

        double Affiliation.CalculateDeductions(PayCheck pc)
        {
            return Dues;
        }
    }
}
