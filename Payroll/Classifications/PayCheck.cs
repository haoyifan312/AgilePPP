using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll.Classifications
{
    internal class PayCheck
    {
        public DateOnly PayBeginDay;
        public DateOnly Payday;

        public double GrossPay {  get; set; }

        public string Field { get; set; } = "";
        public double Deductions { get; set; }
        public double NetPay { get {
                return GrossPay - Deductions;
            } }


        public PayCheck(DateOnly payBeginDate, DateOnly payday)
        {
            PayBeginDay = payBeginDate;
            Payday = payday;
        }

        internal bool IsInPayPeriod(DateOnly date)
        {
            return date <= Payday && date >= PayBeginDay;
        }
    }
}
