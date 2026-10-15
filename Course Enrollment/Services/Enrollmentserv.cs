
using Course_Enrollment.Models;
using Cserv.Services;
using Enmod.Models;
using Lmod.Models;
using Lserv.Services;
using System;
using System.Collections.Generic;

namespace Enserv.Services
{
    public class FeeCalculator
    {
        // Without discounts
        public (decimal BaseFee, decimal DiscountAmount, decimal FinalFee)
            CalculateFee(decimal baseFee, bool isOnline)
        {
            if (baseFee < 0)
            {
                throw new ArgumentException("Invalid base fee");
            }

            decimal discount = 0m;
            decimal surcharge = isOnline ? 200m : 800m;
            decimal finalFee = baseFee + surcharge;

            return (baseFee, discount, finalFee);
        }

        // With early-bird and group discounts
        public (decimal BaseFee, decimal DiscountAmount, decimal FinalFee)
            CalculateFee(decimal baseFee, bool isOnline,
                         bool isEarlyBird, int groupCount)
        {
            if (baseFee < 0)
            {
                throw new ArgumentException("Invalid base fee");
            }

            if (groupCount < 1)
            {
                throw new ArgumentException("Group count must be at least 1");
            }

            decimal discount = 0m;

            if (isEarlyBird)
            {
                discount += baseFee * 0.10m;
            }

            if (groupCount >= 3)
            {
                discount += baseFee * 0.05m;
            }

            decimal surcharge = isOnline ? 200m : 800m;
            decimal finalFee = baseFee - discount + surcharge;

            return (baseFee, discount, finalFee);
        }
    }

    public class EnrollmentReg
    {
        private List<Enrollment> EnrollmentRec = new List<Enrollment>();

        private CourseServ courseService;
        private LearnerReg learnerService;
        private FeeCalculator feeCalculator;

        private int nextEnrollmentId = 1;
        public List<Enrollment> GetEnrollments()
        {
            return EnrollmentRec;
        }
        public EnrollmentReg(
            CourseServ courseService,
            LearnerReg learnerService,
            FeeCalculator feeCalculator)
        {
            this.courseService = courseService;
            this.learnerService = learnerService;
            this.feeCalculator = feeCalculator;
        }

        // Register a learner for a course
        public void RegisterEnrollment()
        {
            Console.WriteLine("Welcome to Enrollment Process");

            Console.Write("Enter Learner ID: ");
            string? learnerId = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(learnerId))
            {
                Console.WriteLine("Learner ID cannot be empty.");
                return;
            }

            // Check whether learner exists
            if (!learnerService.LearnerExists(learnerId))
            {
                Console.WriteLine("Learner not found. Register the learner first.");
                return;
            }

            Console.Write("Enter Course Code: ");
            string? courseCode = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(courseCode))
            {
                Console.WriteLine("Course Code cannot be empty.");
                return;
            }

            // Find the selected course
            Courses? selectedCourse = null;

            foreach (Courses course in courseService.Precourse)
            {
                if (string.Equals(
                    course.CourseCode,
                    courseCode,
                    StringComparison.OrdinalIgnoreCase))
                {
                    selectedCourse = course;
                    break;
                }
            }

            if (selectedCourse == null)
            {
                Console.WriteLine("Course not found.");
                return;
            }

            // Check duplicate active enrollment
            foreach (var i in EnrollmentRec)
            {
                if (i.LearnerId == learnerId && string.Equals(i.CourseCode,selectedCourse.CourseCode,StringComparison.OrdinalIgnoreCase) &&i.Status == EnrollmentStatus.Active)
                {
                    Console.WriteLine(
                        "Learner is already actively enrolled in this course.");
                    return;
                }
            }

            // Check available seats
            if (selectedCourse.EnrolledCount >= selectedCourse.TotalSeats)
            {
                Console.WriteLine("Course is full. No seats available.");
                return;
            }

            // Get session information
            Console.Write("Enter course start date (yyyy-MM-dd): ");

            if (!DateTime.TryParse(
                Console.ReadLine(), out DateTime startDate))
            {
                Console.WriteLine("Invalid start date.");
                return;
            }

            Console.Write("Enter group count: ");

            if (!int.TryParse(Console.ReadLine(), out int groupCount) ||
                groupCount < 1)
            {
                Console.WriteLine("Invalid group count.");
                return;
            }

            Console.Write("Online enrollment? (yes/no): ");
            string? mode = Console.ReadLine();

            bool isOnline;

            if (string.Equals(mode, "yes",
                StringComparison.OrdinalIgnoreCase))
            {
                isOnline = true;
            }
            else if (string.Equals(mode, "no",
                StringComparison.OrdinalIgnoreCase))
            {
                isOnline = false;
            }
            else
            {
                Console.WriteLine("Enter yes or no.");
                return;
            }

            // Early-bird discount applies when enrollment
            // is at least 14 days before the course starts
            bool isEarlyBird =
                (startDate.Date - DateTime.Now.Date).Days >= 14;

            // Calculate the fee using the course's actual fee
            var fee = feeCalculator.CalculateFee(
                selectedCourse.Fee,
                isOnline,
                isEarlyBird,
                groupCount);

            // Create enrollment only after all checks succeed
            Enrollment enrollmentRecord = new Enrollment(
                nextEnrollmentId,
                learnerId,
                selectedCourse.CourseCode!,
                DateTime.Now,
                fee.BaseFee,
                fee.DiscountAmount,
                fee.FinalFee,
                EnrollmentStatus.Active);

            EnrollmentRec.Add(enrollmentRecord);
            nextEnrollmentId++;

            // Update the shared course count
            selectedCourse.EnrolledCount++;

            Console.WriteLine("\nEnrollment Successful!");
            Console.WriteLine("Enrollment ID: " +
                enrollmentRecord.EnrollmentId);
            Console.WriteLine("Course: " + selectedCourse.Title);
            Console.WriteLine("Base Fee: " + fee.BaseFee);
            Console.WriteLine("Discount: " + fee.DiscountAmount);
            Console.WriteLine("Final Fee: " + fee.FinalFee);
            Console.WriteLine("Status: " + enrollmentRecord.Status);
        }

        // Search enrollment by enrollment ID
        public void Searchbycc()
        {
            Console.Write("Enter Enrollment ID to search: ");

            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid Enrollment ID.");
                return;
            }

            foreach (Enrollment enrollment in EnrollmentRec)
            {
                if (enrollment.EnrollmentId == id)
                {
                    DisplayEnrollment(enrollment);
                    return;
                }
            }

            Console.WriteLine("Enrollment not found.");
        }

        // Display all enrollment records
        public void Display()
        {
            if (EnrollmentRec.Count == 0)
            {
                Console.WriteLine("No enrollments found.");
                return;
            }

            foreach (Enrollment enrollment in EnrollmentRec)
            {
                DisplayEnrollment(enrollment);
                Console.WriteLine("-------------------------");
            }
        }

        // Display one enrollment
        private void DisplayEnrollment(Enrollment enrollment)
        {
            Console.WriteLine("Enrollment ID: " +
                enrollment.EnrollmentId);
            Console.WriteLine("Learner ID: " + enrollment.LearnerId);
            Console.WriteLine("Course Code: " + enrollment.CourseCode);
            Console.WriteLine("Enrollment Date: " +
                enrollment.EnrollmentDate);
            Console.WriteLine("Base Fee: " + enrollment.BaseFee);
            Console.WriteLine("Discount: " + enrollment.DiscountAmount);
            Console.WriteLine("Final Fee: " + enrollment.FinalFee);
            Console.WriteLine("Status: " + enrollment.Status);
        }

        // Search enrollments by learner ID
        public void Searchbyl()
        {
            Console.Write("Enter Learner ID to search: ");
            string? learnerId = Console.ReadLine();

            bool found = false;

            foreach (Enrollment enrollment in EnrollmentRec)
            {
                if (enrollment.LearnerId == learnerId)
                {
                    DisplayEnrollment(enrollment);
                    Console.WriteLine("-------------------------");
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("No enrollments found for this learner.");
            }
        }

        // Check whether a learner is already enrolled
        public void Alreadyenrolled()
        {
            Console.Write("Enter Learner ID: ");
            string? id = Console.ReadLine();

            Console.Write("Enter Course Code: ");
            string? code = Console.ReadLine();

            foreach (Enrollment e in EnrollmentRec)
            {
                if (e.LearnerId == id && e.CourseCode == code &&
                    e.Status == EnrollmentStatus.Active)
                {
                    Console.WriteLine("Learner is already enrolled.");
                    return;
                }
            }

            Console.WriteLine("No active enrollment found.");
        }

        // Display available seats
        public void availseats()
        {
            courseService.DisplayAvailableSeats();
        }

        // Cancel enrollment
        public void CancelEnrollment()
        {
            Console.Write("Enter Enrollment ID: ");

            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID.");
                return;
            }

            foreach (Enrollment e in EnrollmentRec)
            {
                if (e.EnrollmentId == id)
                {
                    if (e.Status != EnrollmentStatus.Active)
                    {
                        Console.WriteLine("Enrollment already cancelled or completed.");
                        return;
                    }

                    foreach (Courses c in courseService.Precourse)
                    {
                        if (c.CourseCode == e.CourseCode)
                        {
                            e.Status = EnrollmentStatus.Cancelled;
                            c.EnrolledCount--;

                            Console.WriteLine("Enrollment cancelled successfully.");
                            Console.WriteLine("Available seats: " +
                                (c.TotalSeats - c.EnrolledCount));
                            return;
                        }
                    }

                    Console.WriteLine("Course not found.");
                    return;
                }
            }

            Console.WriteLine("Enrollment not found.");
        }
    }
}
