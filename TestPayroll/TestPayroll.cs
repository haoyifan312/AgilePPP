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

            var tct = new TimeCardTransaction(new DateOnly(2001, 10, 31), 8.0, empId);
            tct.Execute();

            Employee? e = PayrollDatabase.GetInstance().GetEmployee(empId);
            Assert.NotNull(e);

            Classification? pc = e.ItsClassification;
            Assert.NotNull(pc);
            var hpc = (HourlyClassification)pc;
            Assert.NotNull(hpc);

            TimeCard tc = hpc.GetTimeCard(new DateOnly(2001, 10, 31));
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
            int memberId = 86;
            var ua = new UnionAffiliation(12.5, memberId);
            e.ItsAffiliation = ua;

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

        [Fact]
        public void TestChangeDirectTransaction()
        {
            int empId = 13;
            var act = new AddSalariedEmployee(empId, "Lance", "home", 2500);
            act.Execute();

            var cst = new ChangeDirectTransaction(empId, "BoA", 12345);
            cst.Execute();

            Employee? e = PayrollDatabase.GetInstance().GetEmployee(empId);
            Assert.NotNull(e);

            PaymentMethod? pm = e.ItsPaymentMethod;
            Assert.NotNull(pm);
            var dm = (DirectMethod)pm;
            Assert.NotNull(dm);
            Assert.Equal("BoA", dm.Bank);
            Assert.Equal(12345, dm.Account);
        }

        [Fact]
        public void TestChangeMailTransaction()
        {
            int empId = 14;
            var act = new AddSalariedEmployee(empId, "Lance", "home", 2500);
            act.Execute();

            var cst = new ChangeMailTransaction(empId, "home");
            cst.Execute();

            Employee? e = PayrollDatabase.GetInstance().GetEmployee(empId);
            Assert.NotNull(e);

            PaymentMethod? pm = e.ItsPaymentMethod;
            Assert.NotNull(pm);
            var mm = (MailMethod)pm;
            Assert.NotNull(mm);
            Assert.Equal("home", mm.Address);
        }

        [Fact]
        public void TestChangeHoldTransaction()
        {
            int empId = 15;
            var act = new AddSalariedEmployee(empId, "Lance", "home", 2500);
            act.Execute();

            var cst = new ChangeHoldTransaction(empId, "home");
            cst.Execute();

            Employee? e = PayrollDatabase.GetInstance().GetEmployee(empId);
            Assert.NotNull(e);

            PaymentMethod? pm = e.ItsPaymentMethod;
            Assert.NotNull(pm);
            var hm = (HoldMethod)pm;
            Assert.NotNull(hm);
            Assert.Equal("home", hm.Address);
        }

        [Fact]
        public void TestChangeMemberTransaction()
        {
            int empId = 16;
            int memberId = 7734;
            var ahe = new AddHourlyEmployee(empId, "Bill", "Home", 15.25);
            ahe.Execute();

            var cmt = new ChangeMemberTransaction(empId, memberId, 99.42);
            cmt.Execute();

            Employee? e = PayrollDatabase.GetInstance().GetEmployee(empId);
            Assert.NotNull(e);

            Affiliation af = e.ItsAffiliation;
            Assert.NotNull(af);
            UnionAffiliation ua = (UnionAffiliation)af;
            Assert.NotNull(ua);
            Assert.Equal(99.42, ua.Dues);
            Assert.Equal(memberId, ua.MemberId);

            Employee? m = PayrollDatabase.GetInstance().GetUnionMember(memberId);
            Assert.NotNull(m);
            Assert.Equal(m, e);

            var cut = new ChangeUnaffiliatedTransaction(empId);
            cut.Execute();
            Employee? e2 = PayrollDatabase.GetInstance().GetEmployee(empId);
            Assert.NotNull(e2);

            Affiliation af2 = e2.ItsAffiliation;
            Assert.NotNull(af2);
            NoAffiliation na = (NoAffiliation)af2;
            Assert.NotNull(ua);

            Assert.Throws<System.Collections.Generic.KeyNotFoundException> (() => PayrollDatabase.GetInstance().GetUnionMember(memberId));
        }

        [Fact]
        public void TestPaySingleSalariedEmployee()
        {
            int empId = 17;
            var ase = new AddSalariedEmployee(empId, "Bob", "Home", 1000.0);
            ase.Execute();

            var payDate = new DateOnly(2001, 11, 30);
            var pt = new PaydayTransaction(payDate);
            pt.Execute();

            PayCheck? pc = pt.GetPaycheck(empId);
            Assert.NotNull(pc);
            Assert.Equal(1000.0, pc.GrossPay);
            Assert.Equal(1000.0, pc.NetPay);
            Assert.Equal(0.0, pc.Deductions);
        }

        [Fact]
        public void TestPaySingleSalariedEmployeeOnWrongDate()
        {
            int empId = 18;
            var ase = new AddSalariedEmployee(empId, "Bob", "Home", 1000.0);
            ase.Execute();

            var payDate = new DateOnly(2001, 11, 29);
            var pt = new PaydayTransaction(payDate);
            pt.Execute();

            PayCheck? pc = pt.GetPaycheck(empId);
            Assert.Null(pc);
        }

        [Fact]
        public void TestPaySingleHourlyEmployeeNoTimeCards()
        {
            int empId = 19;
            var ahe = new AddHourlyEmployee(empId, "Bill", "Home", 15.25);
            ahe.Execute();

            DateOnly date = new DateOnly(2001, 11, 9);  // Friday
            var pt = new PaydayTransaction(date);
            pt.Execute();

            ValidateHourlyPaycheck(pt, empId, date, 0.0);
        }

        void ValidateHourlyPaycheck(PaydayTransaction pt, int empId, DateOnly date, double pay)
        {
            PayCheck? pc = pt.GetPaycheck(empId);
            Assert.NotNull(pc);
            Assert.Equal(pay, pc.GrossPay);
            Assert.Equal(0.0, pc.Deductions);
            Assert.Equal(pay, pc.NetPay);
        }

        [Fact]
        public void TestPaySingleHourlyEmployeeOneTimeCard()
        {
            int empId = 20;
            var t = new AddHourlyEmployee(empId, "Bill", "Home", 15.25);
            t.Execute();

            DateOnly date = new DateOnly(2001, 11, 9); //Friday

            var tct = new TimeCardTransaction(date, 2.0, empId);
            tct.Execute();

            var pt = new PaydayTransaction(date);
            pt.Execute();
            ValidateHourlyPaycheck(pt, empId, date, 30.5);
        }

        [Fact]
        public void TestPaySingleHourlyEmployeeOvertimeOneTimeCard()
        {
            int empId = 21;
            var t = new AddHourlyEmployee(empId, "Bill", "Home", 15.25);
            t.Execute();

            DateOnly date = new DateOnly(2001, 11, 9);  //Friday
            var tct = new TimeCardTransaction(date, 9.0, empId);
            tct.Execute();

            var pt = new PaydayTransaction(date);
            pt.Execute();
            ValidateHourlyPaycheck(pt, empId, date, (8 + 1.5) * 15.25);
        }

        [Fact]
        public void TestPaySingleHourlyEmployeeOnWrongDate()
        {
            int empId = 22;
            var t = new AddHourlyEmployee(empId, "Bill", "Home", 15.25);
            t.Execute();

            DateOnly date = new DateOnly(2001, 11, 8);  //Thursday
            var tct = new TimeCardTransaction(date, 9.0, empId);
            tct.Execute();
            var pt = new PaydayTransaction(date);
            pt.Execute();

            PayCheck? pc = pt.GetPaycheck(empId);
            Assert.Null(pc);
        }

    }
}
