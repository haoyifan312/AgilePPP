using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll
{
    internal class PayrollDatabase
    {
        static private PayrollDatabase _instance = new PayrollDatabase();
        private Dictionary<int, Employee> _employees;

        private PayrollDatabase()
        {
            _employees = new Dictionary<int, Employee>();
        }

        public static PayrollDatabase GetInstance()
        {
            return _instance;
        }

        public Employee? GetEmployee(int empId)
        {
            if (_employees.ContainsKey(empId))
            {
                return _employees[empId];
            }
            return null;
        }

        public void AddEmployee(int empId, Employee employee)
        {
            _employees.Add(empId, employee);
        }

        public void DeleteEmployee(int empId)
        {
            _employees.Remove(empId);
        }
    }
}
