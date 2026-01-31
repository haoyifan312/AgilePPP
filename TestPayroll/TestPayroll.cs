using Payroll;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestPayroll
{
    public class TestPayroll
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

            SalariedClassification sc = (SalariedClassification)e.ItsClassification;
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

        [Fact]
        public void TestTimeCardTransactionImpl()
        {
            int empId = 5;
            var t = new AddHourlyEmployee(empId, "Bill", "Home", 15.25);
            t.Execute();

            var tct = new TimeCardTransaction(20011031, 8.0, empId);
            tct.Execute();

            Employee? e = PayrollDatabase.GetInstance().GetEmployee(empId);
            Assert.NotNull(e);

            Classification? pc = e.ItsClassification;
            Assert.NotNull(pc);
            var hpc = (HourlyClassification)pc;
            Assert.NotNull(hpc);

            TimeCard tc = hpc.GetTimeCard(20011031);
            Assert.Equal(8.0, tc.Hours);
        }

        [Fact]
        public void TestSalesReceiptTransactionImpl()
        {
            int empId = 6;
            var t = new AddCommissionedEmployee(empId, "Carl", "Home", 3000, 10);
            t.Execute();

            var srt = new SalesReceiptTransaction(20260130, 120, empId);
            srt.Execute();

            Employee? e = PayrollDatabase.GetInstance().GetEmployee(empId);
            Assert.NotNull(e);

            Classification? c = e.ItsClassification;
            Assert.NotNull(c);

            var cc = (CommissionedClassification)c;
            Assert.NotNull(cc);

            SalesReceipt sr = cc.GetSalesReceipt(20260130);

            Assert.Equal(120, sr.Amount);
        }
    }
}
