using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    internal class CommissionedClassification: Classification
    {
        private Dictionary<int, SalesReceipt> _receipts;

        public double Salary { set; get; }
        public double CommissionRate { set; get; }

        public CommissionedClassification(double salary, double commissionRate)
        {
            Salary = salary;
            CommissionRate = commissionRate;
            _receipts = new Dictionary<int, SalesReceipt>();
        }

        public void AddSalesReceipt(SalesReceipt salesReceipt)
        {
            _receipts.Add(salesReceipt.Date, salesReceipt);
        }

        public SalesReceipt GetSalesReceipt(int date)
        {
            return _receipts[date]; 
        }

        double Classification.CalculatePay(PayCheck pc)
        {
            return 0.0;
        }
    }
}
