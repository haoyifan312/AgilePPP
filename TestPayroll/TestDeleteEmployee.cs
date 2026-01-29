using Payroll;
using System;
using System.Collections.Generic;
using System.Text;
using Payroll.Transaction;

namespace TestPayroll
{
    public class TestDeleteEmployee
    {
        [Fact]
        public void TestDeleteEmployeeSimple()
        {
            int empId = 4;
            var add = new AddCommissionedEmployee(empId, "Lance", "Home", 2500, 3.2);
            add.Execute();

            {
                Employee? e = PayrollDatabase.GetInstance().GetEmployee(empId);
                Assert.NotNull(e);
            }

            var del = new DeleteEmployeeTransaction(empId);
            del.Execute();
            {
                Employee? e = PayrollDatabase.GetInstance().GetEmployee(empId);
                Assert.Null(e);
            }
        }
    }
}
