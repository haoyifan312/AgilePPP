using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    struct TimeCard
    {
        public DateOnly Date;
        public double Hours;

        public TimeCard(DateOnly date, double hours)
        {
            Date = date;
            Hours = hours;
        }
    }

    internal class TimeCardTransaction: Transaction
    {
        public DateOnly Date { get; set; }
        public double Hours { get; set; }
        public int EmpId { get; set; }

        public TimeCardTransaction(DateOnly date, double hours, int empId)
        {
            Date = date;
            Hours = hours;
            EmpId = empId;
        }

        public void Execute()
        {
            Employee? e = PayrollDatabase.GetInstance().GetEmployee(EmpId);
            if (e == null)
            {
                throw new Exception($"No employee {EmpId}");
            }

            var tc = new TimeCard(Date, Hours);
            Classification? t = e.ItsClassification;
            if (t == null)
            {
                throw new Exception($"{e.Name} does not have payment classification setup");
            }

            var tct = (HourlyClassification)t;
            if (tct == null)
            {
                throw new Exception($"{e.Name} is not hourly paid");
            }

            tct.AddTimeCard(tc);
        }
    }
}
