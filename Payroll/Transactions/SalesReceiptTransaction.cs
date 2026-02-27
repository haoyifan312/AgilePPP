using Payroll.Classifications;
using Payroll.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace Payroll.Transactions
{
    internal struct SalesReceipt
    {
        public int Date;
        public double Amount;

        public SalesReceipt(int date, double amount) 
        {
            Date = date;
            Amount = amount;
        }
    }

    internal class SalesReceiptTransaction : Transaction
    {
        public int Date {  get; set; }
        public double Amount { get; set; }
        public int EmpId { get; set; }

        public SalesReceiptTransaction(int date, double amount, int empId)
        {
            Date = date;
            Amount = amount;
            EmpId = empId;
        }

        public void Execute()
        {
            Employee? e = PayrollDatabase.GetInstance().GetEmployee(EmpId);
            if (e == null)
                throw new Exception($"No employee with id {EmpId}");

            Classification? c = e.ItsClassification;
            if (c == null)
                throw new Exception($"{e.Name} does not have payment classification setup");

            var cc = (CommissionedClassification)c;
            if (cc == null)
                throw new Exception($"{e.Name} is not commision payed");

            var sr = new SalesReceipt(Date, Amount);
            cc.AddSalesReceipt(sr);
        }
    }
}
