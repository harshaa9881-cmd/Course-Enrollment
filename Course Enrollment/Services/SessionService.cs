
using System;
using Course_Enrollment.Models;

namespace Cserv.Services
{
    public class SessionService
    {
        public void DisplaySessionCharge(string courseCode,DateTime startDate,int duration, bool isOnline)
        {
            CourseSession session;

            if (isOnline)
            {
                session = new OnlineSession(courseCode, startDate, duration);
            }
            else
            {
                session = new ClassroomSession(courseCode, startDate, duration);
            }

            decimal charge = session.CalculateSessionCharge();

            Console.WriteLine("Course Code: " + session.CourseCode);
            Console.WriteLine("Start Date: " + session.StartDate.ToShortDateString());
            Console.WriteLine("Duration: " + session.DurationDays + " days");
            Console.WriteLine("Session Charge: " + charge);
        }
    }
}
