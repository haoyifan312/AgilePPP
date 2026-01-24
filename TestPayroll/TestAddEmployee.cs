using Payroll;
using Payroll.Transaction;

namespace TestPayroll
{
    public class TestAddEmployee
    {
        [Fact]
        public void TestAddSalariedEmployee()
        {
            int empId = 1;
            var t = new AddSalariedEmployee(empId, "Bob", "Home", 1000.0);
            t.Execute();

            Employee e = PayrollDatabase.GetInstance().GetEmployee(empId);
            Assert.Equal("Bob", e.Name);

            Assert.True(e.ItsClassification is SalariedClassification);

            SalariedClassification sc = (SalariedClassification) e.ItsClassification;
            Assert.Equal(1000.0, sc.Salary);

            Assert.True(e.ItsSchedule is MonthlySchedule);
            Assert.True(e.ItsPaymentMethod is HoldMethod);
        }
    }
}
