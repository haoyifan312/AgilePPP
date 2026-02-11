using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    internal class PaydayTransaction : Transaction
    {
        private Dictionary<int, PayCheck> _paychecks;
        public DateOnly Date {  get; set; }

        public PaydayTransaction(DateOnly date)
        {
            Date = date;
            _paychecks = new Dictionary<int, PayCheck>();
        }

        public void Execute()
        {
            foreach (var kvp in PayrollDatabase.GetInstance().Employees)
            {
                int empId = kvp.Key;
                var employee = kvp.Value;
                if (!employee.IsPayDay(Date))
                    continue;

                var pc = new PayCheck(Date);
                _paychecks.Add(empId, pc);
                employee.Payday(pc);
            }
        }

        public PayCheck? GetPaycheck(int empId)
        {
            if (!_paychecks.ContainsKey(empId))
                return null;

            return _paychecks[empId];
        }
    }
}
