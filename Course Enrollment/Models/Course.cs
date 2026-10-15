using System;
using System.Collections.Generic;
using System.Text;

namespace Course_Enrollment.Models
{
    public class Courses
    {
        public string? CourseCode { get; set; }
        public string? Title { get; set; }
        public decimal Fee  { get; set; }
        public int DurationDays { get; set; }
        public int TotalSeats { get; set; }
        public int EnrolledCount { get; set; }

        public Courses(string code, string title, decimal fee,
                          int duration, int seats, int enrolled)
        {
            CourseCode = code;
            Title = title;
            Fee = fee;
            DurationDays = duration;
            TotalSeats = seats;
            EnrolledCount = enrolled;
        }
    }
}
    
