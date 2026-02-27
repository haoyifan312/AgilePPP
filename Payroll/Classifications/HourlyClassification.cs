using Payroll.Transactions;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Payroll.Classifications
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
            var payDay = pc.Payday;
            foreach (var kvp in _timeCards)
            {
                var date = kvp.Key;
                if (!pc.IsInPayPeriod(date))
                    continue;   //only last week

                double eachEffectiveHour = kvp.Value.Hours;
                if (IsNonWorkDay(date))
                    eachEffectiveHour *= 1.5;
                else
                    eachEffectiveHour = CalculateEffectiveHoursIncludeOverLimit(overtimeLimit, eachEffectiveHour);
                totalEffectiveHours += eachEffectiveHour;
            }
            _timeCards.Clear();
            return totalEffectiveHours * HourlyRate;
        }

        private bool IsNonWorkDay(DateOnly date)
        {
            return date.DayOfWeek == DayOfWeek.Sunday || date.DayOfWeek == DayOfWeek.Saturday;
        }

        private static double CalculateEffectiveHoursIncludeOverLimit(double overtimeLimit, double eachEffectiveHour)
        {
            if (eachEffectiveHour > overtimeLimit)
                eachEffectiveHour = overtimeLimit + 1.5 * (eachEffectiveHour - overtimeLimit);
            return eachEffectiveHour;
        }

        private bool IsInPayPeriod(DateOnly payDay, DateOnly timeCardDate)
        {
            int dateDiff = payDay.DayNumber - timeCardDate.DayNumber;
            return dateDiff < 7 && dateDiff >= 0;
        }
    }
}
