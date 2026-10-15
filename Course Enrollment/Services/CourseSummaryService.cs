
using System;
using Course_Enrollment.Models;

namespace Cserv.Services
{
    public class CourseSummaryService
    {
        public CourseSummary CreateSummary(Courses course)
        {
            CourseSummary summary = new CourseSummary(course.CourseCode ?? "", course.Title ?? "",course.Fee,course.TotalSeats,course.EnrolledCount);

            return summary;
        }

        public void DisplaySummary(CourseSummary summary)
        {
            Console.WriteLine("Course Summary");
            Console.WriteLine("Course Code: " + summary.CourseCode);
            Console.WriteLine("Title: " + summary.Title);
            Console.WriteLine("Fee: " + summary.Fee);
            Console.WriteLine("Total Seats: " + summary.TotalSeats);
            Console.WriteLine("Enrolled Count: " + summary.EnrolledCount);
        }
    }
}
