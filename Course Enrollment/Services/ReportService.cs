
using System;
using System.Collections.Generic;
using Course_Enrollment.Models;
using Enmod.Models;

namespace Cserv.Services
{
    public class ReportService
    {
        // Report A: Course Occupancy
        public void CourseOccupancy(Courses[] courses)
        {
            foreach (Courses course in courses)
            {
                int availableSeats = course.TotalSeats - course.EnrolledCount;
                double percentage = 0;

                if (course.TotalSeats > 0)
                {
                    percentage = (double)course.EnrolledCount
                                 / course.TotalSeats * 100;
                }

                Console.WriteLine("\nCourse: " + course.CourseCode);
                Console.WriteLine("Total Seats: " + course.TotalSeats);
                Console.WriteLine("Occupied Seats: " + course.EnrolledCount);
                Console.WriteLine("Available Seats: " + availableSeats);
                Console.WriteLine("Occupancy: " + percentage + "%");
            }
        }

        // Report B: Expected Fees
        public void ExpectedFees(List<Enrollment> enrollments)
        {
            decimal total = 0;

            foreach (Enrollment enrollment in enrollments)
            {
                if (enrollment.Status == EnrollmentStatus.Active)
                {
                    total += enrollment.FinalFee;
                }
            }

            Console.WriteLine("Expected Fees: " + total);
        }

        // Report C: Payment Summary
        public void PaymentSummary(List<Payment> payments)
        {
            int successful = 0;
            int failed = 0;
            decimal collected = 0;

            foreach (Payment payment in payments)
            {
                if (payment.Status == PaymentStatus.Successful)
                {
                    successful++;
                    collected += payment.Amount;
                }
                else if (payment.Status == PaymentStatus.Failed)
                {
                    failed++;
                }
            }

            Console.WriteLine("Successful Attempts: " + successful);
            Console.WriteLine("Failed Attempts: " + failed);
            Console.WriteLine("Total Collected: " + collected);
        }

        // Report D: Course Enrollment Summary
        public void CourseEnrollmentSummary(
            Courses[] courses, List<Enrollment> enrollments)
        {
            foreach (Courses course in courses)
            {
                int count = 0;

                foreach (Enrollment enrollment in enrollments)
                {
                    if (enrollment.CourseCode == course.CourseCode &&
                        enrollment.Status == EnrollmentStatus.Active)
                    {
                        count++;
                    }
                }

                Console.WriteLine("\nCourse: " + course.CourseCode);
                Console.WriteLine("Active Enrollments: " + count);

                if (course.TotalSeats - course.EnrolledCount <= 0)
                {
                    Console.WriteLine("Course is full.");
                }
            }
        }
    }
}
