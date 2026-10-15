using System;
using System.Collections.Generic;
using System.Text;

namespace Enmod.Models
{
    
        public enum EnrollmentStatus
        {
            Active,
            Cancelled,
            Completed
        }

        public class Enrollment
        {
            public int EnrollmentId { get; set; }
            public string LearnerId { get; set; }
            public string CourseCode { get; set; }
            public DateTime EnrollmentDate { get; set; }
            public decimal BaseFee { get; set; }
            public decimal DiscountAmount { get; set; }
            public decimal FinalFee { get; set; }
            public EnrollmentStatus Status { get; set; }

            public Enrollment(int id,string learnerId,string courseCode,DateTime date,decimal baseFee,decimal discountAmount,decimal finalFee,EnrollmentStatus status)
            {
                EnrollmentId = id;
                LearnerId = learnerId;
                CourseCode = courseCode;
                EnrollmentDate = date;
                BaseFee = baseFee;
                DiscountAmount = discountAmount;
                FinalFee = finalFee;
                Status = status;
            }
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;
    }
}
