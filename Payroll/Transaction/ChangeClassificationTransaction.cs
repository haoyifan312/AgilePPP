using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    internal abstract class ChangeClassificationTransaction : ChangeEmployeeTransaction
    {
        public ChangeClassificationTransaction(int empId):
            base(empId) 
        {
        }

        protected override void Change(Employee e)
        {
            Classification c = GetClassification();
            Schedule s = GetSchedule();
            e.ItsClassification = c;
            e.ItsSchedule = s;
        }

        protected abstract Classification GetClassification();
        protected abstract Schedule GetSchedule();
    }
}
