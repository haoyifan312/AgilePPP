using System;
using System.Collections.Generic;
using System.Text;

using Payroll;

namespace Payroll
{
    internal class HourlyClassification : Classification
    {
        private Dictionary<DateOnly, TimeCard> _timeCards;

        public double HourlyRate { get; set; }

        public HourlyClassification(double hourlyRate)
        {
            HourlyRate = hourlyRate;
            _timeCards = new Dictionary<DateOnly, TimeCard>();
        }

        public void AddTimeCard(TimeCard tc)
        {
            _timeCards.Add(tc.Date, tc);
        }

        public TimeCard GetTimeCard(DateOnly date)
        {
            return _timeCards[date]; 
        }

        double Classification.CalculatePay(PayCheck pc)
        {
            double totalEffectiveHours = 0.0;
            double overtimeLimit = 8.0;
            foreach(var kvp in _timeCards)
            {
                double eachEffectiveHour = kvp.Value.Hours;
                if (eachEffectiveHour > overtimeLimit)
                    eachEffectiveHour = overtimeLimit + 1.5 * (eachEffectiveHour - overtimeLimit);
                totalEffectiveHours += eachEffectiveHour;

            }
            _timeCards.Clear();
            return totalEffectiveHours * HourlyRate;
        }
    }
}
