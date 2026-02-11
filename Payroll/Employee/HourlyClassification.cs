using System;
using System.Collections.Generic;
using System.Text;

using Payroll;

namespace Payroll
{
    internal class HourlyClassification : Classification
    {
        private Dictionary<int, TimeCard> _timeCards;

        public double HourlyRate { get; set; }

        public HourlyClassification(double hourlyRate)
        {
            HourlyRate = hourlyRate;
            _timeCards = new Dictionary<int, TimeCard>();
        }

        public void AddTimeCard(TimeCard tc)
        {
            _timeCards.Add(tc.Date, tc);
        }

        public TimeCard GetTimeCard(int date)
        {
            return _timeCards[date]; 
        }

        double Classification.CalculatePay(PayCheck pc)
        {
            throw new NotImplementedException();
        }
    }
}
