using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    internal class PayCheck
    {
        private DateOnly _payday;
        
        public double GrossPay {  get; set; }

        public string Field { get; set; } = "";
        public double Deductions { get; set; }
        public double NetPay { get {
                return GrossPay - Deductions;
            } }


        public PayCheck(DateOnly payday)
        {
            _payday = payday;
        }
    }
}
