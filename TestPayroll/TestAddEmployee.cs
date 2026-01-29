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

            Employee? e = PayrollDatabase.GetInstance().GetEmployee(empId);
            Assert.NotNull(e);
            Assert.Equal("Bob", e.Name);

            Assert.True(e.ItsClassification is SalariedClassification);

            SalariedClassification sc = (SalariedClassification) e.ItsClassification;
            Assert.Equal(1000.0, sc.Salary);

            Assert.True(e.ItsSchedule is MonthlySchedule);
            Assert.True(e.ItsPaymentMethod is HoldMethod);
        }

        [Fact]
        public void TestAddHourlyEmployee()
        {
            int empId = 2;
            var t = new AddHourlyEmployee(empId, "John", "Smith Street", 80.0);            
            t.Execute();

            Employee? e = PayrollDatabase.GetInstance().GetEmployee(empId);
            Assert.NotNull(e);
            Assert.Equal("John", e.Name);

            Assert.True(e.ItsClassification is HourlyClassification);

            HourlyClassification sc = (HourlyClassification)e.ItsClassification;
            Assert.Equal(80.0, sc.HourlyRate);

            Assert.True(e.ItsSchedule is WeeklySchedule);
            Assert.True(e.ItsPaymentMethod is HoldMethod);
        }

        [Fact]
        public void TestAddCommissionedEmployee()
        {
            int empId = 3;
            var t = new AddCommissionedEmployee(empId, "Will", "1st ave", 800.0, 0.02);
            t.Execute();

            Employee? e = PayrollDatabase.GetInstance().GetEmployee(empId);
            Assert.NotNull(e);
            Assert.Equal("Will", e.Name);

            Assert.True(e.ItsClassification is CommissionedClassification);

            CommissionedClassification sc = (CommissionedClassification)e.ItsClassification;
            Assert.Equal(800.0, sc.Salary);
            Assert.Equal(0.02, sc.CommissionRate);

            Assert.True(e.ItsSchedule is BiweeklySchedule);
            Assert.True(e.ItsPaymentMethod is HoldMethod);
        }
    }
}
