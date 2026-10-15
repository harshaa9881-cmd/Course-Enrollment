
namespace Course_Enrollment.Models
{
    public record CourseSummary(
        string CourseCode,
        string Title,
        decimal Fee,
        int TotalSeats,
        int EnrolledCount
    );
}
