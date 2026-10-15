
using System;
using Cserv.Services;
using Lserv.Services;
using Enserv.Services;
using Enmod.Models;
using Course_Enrollment.Models;

namespace Course_Enrollment
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CourseServ courseService = new CourseServ();
            LearnerReg learnerService = new LearnerReg();
            FeeCalculator feeCalculator = new FeeCalculator();

            EnrollmentReg enrollmentService =
                new EnrollmentReg(courseService, learnerService, feeCalculator);

            PaymentService paymentService = new PaymentService();
            ReportService reportService = new ReportService();

            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\n===== COURSE ENROLLMENT SYSTEM =====");
                Console.WriteLine("1. List all courses");
                Console.WriteLine("2. Search courses");
                Console.WriteLine("3. Sort courses by fee");
                Console.WriteLine("4. Register a learner");
                Console.WriteLine("5. List learners");
                Console.WriteLine("6. Search for a learner");
                Console.WriteLine("7. Enroll in a course");
                Console.WriteLine("8. View enrollment records");
                Console.WriteLine("9. Cancel an enrollment");
                Console.WriteLine("10. Display a fee breakdown");
                Console.WriteLine("11. Process a simulated payment");
                Console.WriteLine("12. View payment history");
                Console.WriteLine("13. Display reports");
                Console.WriteLine("14. Exit");
                Console.Write("Enter your choice: ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid choice.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        courseService.DisplayCourses();
                        break;

                    case 2:
                        Console.WriteLine("1. Search by course code");
                        Console.WriteLine("2. Search by course title");
                        Console.Write("Enter choice: ");

                        if (!int.TryParse(Console.ReadLine(), out int searchChoice))
                        {
                            Console.WriteLine("Invalid choice.");
                            break;
                        }

                        switch (searchChoice)
                        {
                            case 1:
                                courseService.SearchByCode();
                                break;

                            case 2:
                                courseService.SearchByTitle();
                                break;

                            default:
                                Console.WriteLine("Invalid search choice.");
                                break;
                        }
                        break;

                    case 3:
                        courseService.SortByFee();
                        break;

                    case 4:
                        learnerService.Registration();
                        break;

                    case 5:
                        learnerService.Display();
                        break;

                    case 6:
                        learnerService.Search();
                        break;

                    case 7:
                        enrollmentService.RegisterEnrollment();
                        break;

                    case 8:
                        enrollmentService.Display();
                        break;

                    case 9:
                        enrollmentService.CancelEnrollment();
                        break;

                    case 10:
                        Console.Write("Enter base fee: ");
                        if (!decimal.TryParse(Console.ReadLine(), out decimal baseFee))
                        {
                            Console.WriteLine("Invalid fee.");
                            break;
                        }

                        Console.WriteLine("1. Online session");
                        Console.WriteLine("2. Classroom session");
                        Console.Write("Enter session type: ");

                        if (!int.TryParse(Console.ReadLine(), out int sessionChoice))
                        {
                            Console.WriteLine("Invalid session type.");
                            break;
                        }

                        bool isOnline = sessionChoice switch
                        {
                            1 => true,
                            2 => false,
                            _ => false
                        };

                        if (sessionChoice != 1 && sessionChoice != 2)
                        {
                            Console.WriteLine("Invalid session type.");
                            break;
                        }

                        var feeSummary = feeCalculator.CalculateFee(baseFee, isOnline);

                        Console.WriteLine("Base Fee: " + feeSummary.Item1);
                        Console.WriteLine("Session Surcharge: " + feeSummary.Item2);
                        Console.WriteLine("Final Fee: " + feeSummary.Item3);
                        break;

                    case 11:
                        Console.Write("Enter enrollment ID: ");

                        if (!int.TryParse(Console.ReadLine(), out int enrollmentId))
                        {
                            Console.WriteLine("Invalid enrollment ID.");
                            break;
                        }

                        Enrollment selectedEnrollment = null;

                        foreach (Enrollment enrollment in enrollmentService.GetEnrollments())
                        {
                            if (enrollment.EnrollmentId == enrollmentId)
                            {
                                selectedEnrollment = enrollment;
                                break;
                            }
                        }

                        if (selectedEnrollment == null)
                        {
                            Console.WriteLine("Enrollment not found.");
                            break;
                        }

                        Console.WriteLine("1. Online payment");
                        Console.WriteLine("2. Classroom payment");
                        Console.Write("Choose payment type: ");

                        if (!int.TryParse(Console.ReadLine(), out int paymentChoice))
                        {
                            Console.WriteLine("Invalid payment type.");
                            break;
                        }

                        IPaymentProcessor processor = new OnlinePaymentProcessor();

                        switch (paymentChoice)
                        {
                            case 1:
                                processor = new OnlinePaymentProcessor();
                                break;

                            case 2:
                                processor = new ClassroomPaymentProcessor();
                                break;

                            default:
                                Console.WriteLine("Invalid payment type.");
                                break;
                        }

                        Console.Write("Simulate successful payment? (yes/no): ");
                        bool simulateSuccess =
                            Console.ReadLine()?.Trim().ToLower() == "yes";

                        paymentService.MakePayment(selectedEnrollment,selectedEnrollment.FinalFee,simulateSuccess,processor);
                        break;

                    case 12:
                        paymentService.DisplayPaymentHistory();
                        break;

                    case 13:
                        reportService.CourseOccupancy(courseService.Precourse);

                        reportService.ExpectedFees(enrollmentService.GetEnrollments());

                        reportService.PaymentSummary(paymentService.PaymentHistory);

                        reportService.CourseEnrollmentSummary(courseService.Precourse,enrollmentService.GetEnrollments());
                        break;

                    case 14:
                        exit = true;
                        Console.WriteLine("Thank you for using the system!");
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Select 1 to 14.");
                        break;
                }
            }
        }
    }
}
