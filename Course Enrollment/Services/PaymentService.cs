
using System;
using System.Collections.Generic;
using Enmod.Models;

namespace Cserv.Services
{
    // Implicit interface implementation
    public class OnlinePaymentProcessor : IPaymentProcessor
    {
        public bool ProcessPayment(decimal amount, bool simulateSuccess)
        {
            Console.WriteLine("Processing online payment...");
            return simulateSuccess;
        }
    }

    // Explicit interface implementation
    public class ClassroomPaymentProcessor : IPaymentProcessor
    {
        bool IPaymentProcessor.ProcessPayment(
            decimal amount, bool simulateSuccess)
        {
            Console.WriteLine("Processing classroom payment...");
            return simulateSuccess;
        }
    }

    public class PaymentService
    {
        public List<Payment> PaymentHistory = new List<Payment>();

        private int nextPaymentId = 1;

        public bool MakePayment(
            Enrollment enrollment,
            decimal amount,
            bool simulateSuccess,
            IPaymentProcessor processor)
        {
            if (enrollment.Status == EnrollmentStatus.Cancelled)
            {
                Console.WriteLine("Payment rejected. Enrollment is cancelled.");
                return false;
            }

            if (amount < 0)
            {
                Console.WriteLine("Invalid payment amount.");
                return false;
            }

            bool success = processor.ProcessPayment(
                amount, simulateSuccess);

            PaymentStatus paymentStatus;

            if (success)
            {
                paymentStatus = PaymentStatus.Successful;
                enrollment.PaymentStatus = PaymentStatus.Successful;
                Console.WriteLine("Payment successful.");
            }
            else
            {
                paymentStatus = PaymentStatus.Failed;
                Console.WriteLine("Payment failed.");
            }

            Payment payment = new Payment(
                nextPaymentId,
                enrollment.EnrollmentId,
                amount,
                DateTime.Now,
                paymentStatus
            );

            PaymentHistory.Add(payment);
            nextPaymentId++;

            return success;
        }

        public void DisplayPaymentHistory()
        {
            foreach (Payment payment in PaymentHistory)
            {
                Console.WriteLine("--------------------");
                Console.WriteLine("Payment ID: " + payment.PaymentId);
                Console.WriteLine("Enrollment ID: " + payment.EnrollmentId);
                Console.WriteLine("Amount: " + payment.Amount);
                Console.WriteLine("Payment Date: " + payment.PaymentDate);
                Console.WriteLine("Status: " + payment.Status);
            }
        }
    }
}
