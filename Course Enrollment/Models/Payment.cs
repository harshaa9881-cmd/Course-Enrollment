
using System;

namespace Enmod.Models
{
    public enum PaymentStatus
    {
        Pending,
        Successful,
        Failed
    }

    public class Payment
    {
        public int PaymentId { get; set; }
        public int EnrollmentId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public PaymentStatus Status { get; set; }

        public Payment(int paymentId, int enrollmentId,
                       decimal amount, DateTime paymentDate,
                       PaymentStatus status)
        {
            PaymentId = paymentId;
            EnrollmentId = enrollmentId;
            Amount = amount;
            PaymentDate = paymentDate;
            Status = status;
        }
    }

    public interface IPaymentProcessor
    {
        bool ProcessPayment(decimal amount, bool simulateSuccess);
    }
}
