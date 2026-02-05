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
        public void TestSalesReceiptTransaction()
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

        [Fact]
        public void TestAddServiceCharge()
        {
            int empId = 7;
            var t = new AddHourlyEmployee(empId, "Bill", "Home", 15.25);
            t.Execute();
            Employee? e = PayrollDatabase.GetInstance().GetEmployee(empId);
            Assert.NotNull(e);

            var ua = new UnionAffiliation(12.5);
            e.ItsAffiliation = ua;

            int memberId = 86;
            PayrollDatabase.GetInstance().AddUnionMember(memberId, e);
            ServiceChargeTransaction sct = new ServiceChargeTransaction(memberId, 20011101, 12.95);
            sct.Execute();

            ServiceCharge? sc = ua.GetServiceCharge(20011101);
            Assert.NotNull(sc);
            Assert.Equal(12.95, sc.Amount);

        }

        [Fact]
        public void TestChangeNameTransaction()
        {
            int empId = 8;
            var ahe = new AddHourlyEmployee(empId, "Bill", "Home", 15.25);
            ahe.Execute();

            var cnt = new ChangeNameTransaction(empId, "Bob");
            cnt.Execute();

            Employee? e = PayrollDatabase.GetInstance().GetEmployee(empId);
            Assert.NotNull(e);
            Assert.Equal("Bob", e.Name);
        }

        [Fact]
        public void TestChangeAddressTransaction()
        {
            int empId = 9;
            var ahe = new AddSalariedEmployee(empId, "Bill", "Home", 15.25);
            ahe.Execute();

            var cnt = new ChangeAddressTransaction(empId, "Street");
            cnt.Execute();

            Employee? e = PayrollDatabase.GetInstance().GetEmployee(empId);
            Assert.NotNull(e);
            Assert.Equal("Street", e.Address);
        }

        [Fact]
        public void TestChangeHourlyTransaction()
        {
            int empId = 10;
            var act = new AddCommissionedEmployee(empId, "Lance", "home", 2500, 3.2);
            act.Execute();

            var cht = new ChangeHourlyTransaction(empId, 27.25);
            cht.Execute();

            Employee? e = PayrollDatabase.GetInstance().GetEmployee(empId);
            Assert.NotNull(e);

            Classification? c = e.ItsClassification;
            Assert.NotNull(c);
            var hc = (HourlyClassification)c;
            Assert.NotNull(hc);
            Assert.Equal(27.25, hc.HourlyRate);

            Schedule? s = e.ItsSchedule;
            Assert.NotNull(s);
            var ws = (WeeklySchedule)s;
            Assert.NotNull(ws);
        }

        [Fact]
        public void TestChangeSalariedTransaction()
        {
            int empId = 11;
            var act = new AddCommissionedEmployee(empId, "Lance", "home", 2500, 3.2);
            act.Execute();

            var cst = new ChangeSalariedTransaction(empId, 2725);
            cst.Execute();

            Employee? e = PayrollDatabase.GetInstance().GetEmployee(empId);
            Assert.NotNull(e);

            Classification? c = e.ItsClassification;
            Assert.NotNull(c);
            var sc = (SalariedClassification)c;
            Assert.NotNull(sc);
            Assert.Equal(2725, sc.Salary);

            Schedule? s = e.ItsSchedule;
            Assert.NotNull(s);
            var ms = (MonthlySchedule)s;
            Assert.NotNull(ms);
        }

        [Fact]
        public void TestChangeCommissionedTransaction()
        {
            int empId = 12;
            var act = new AddSalariedEmployee(empId, "Lance", "home", 2500);
            act.Execute();

            var cst = new ChangeCommissionedTransaction(empId, 2725, 1.2);
            cst.Execute();

            Employee? e = PayrollDatabase.GetInstance().GetEmployee(empId);
            Assert.NotNull(e);

            Classification? c = e.ItsClassification;
            Assert.NotNull(c);
            var cc = (CommissionedClassification)c;
            Assert.NotNull(cc);
            Assert.Equal(2725, cc.Salary);
            Assert.Equal(1.2, cc.CommissionRate);

            Schedule? s = e.ItsSchedule;
            Assert.NotNull(s);
            var bws = (BiweeklySchedule)s;
            Assert.NotNull(bws);
        }
    }
}
