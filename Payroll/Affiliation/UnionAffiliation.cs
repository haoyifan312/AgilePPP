using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    internal class UnionAffiliation: Affiliation
    {
        public double Dues {  get; set; }

        private Dictionary<int, ServiceCharge> _serviceCharges;

        public UnionAffiliation(double dues) 
        {
            Dues = dues;
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
    }
}
