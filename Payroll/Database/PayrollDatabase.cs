using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Payroll.Database
{
    internal class PayrollDatabase
    {
        static private PayrollDatabase _instance = new PayrollDatabase();
        private Dictionary<int, Employee> _employees;
        private Dictionary<int, Employee> _unionMembers;
        public ReadOnlyDictionary<int, Employee> Employees => new(_employees);

        private PayrollDatabase()
        {
            _employees = new Dictionary<int, Employee>();
            _unionMembers = new Dictionary<int, Employee>();
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

        public void AddUnionMember(int memberId, Employee employee)
        {
            _unionMembers.Add(memberId, employee);
        }

        public Employee? GetUnionMember(int memberId)
        {
            return _unionMembers[memberId];
        }

        public void DeleteUnionMember(int memberId)
        {
            _unionMembers.Remove(memberId);
        }
    }
}
