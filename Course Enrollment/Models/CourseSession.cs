
using System;

namespace Course_Enrollment.Models
{
    public abstract class CourseSession
    {
        public string CourseCode { get; set; }
        public DateTime StartDate { get; set; }
        public int DurationDays { get; set; }

        public CourseSession(string code, DateTime startDate, int duration)
        {
            CourseCode = code;
            StartDate = startDate;
            DurationDays = duration;
        }

        public abstract decimal CalculateSessionCharge();
    }

    public class OnlineSession : CourseSession
    {
        public OnlineSession(string code, DateTime startDate, int duration)
            : base(code, startDate, duration)
        {
        }

        public override decimal CalculateSessionCharge()
        {
            return 200m;
        }
    }

    public class ClassroomSession : CourseSession
    {
        public ClassroomSession(string code, DateTime startDate, int duration): base(code, startDate, duration)
        {
        }

        public override decimal CalculateSessionCharge()
        {
            return 800m;
        }
    }
}
