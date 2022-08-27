using CenterApp.Core.Models;

namespace CenterAppWeb.ViewModel
{
    public class ExamScheduleVM
    {
        public IEnumerable<Exam> Exams { get; set; }

        public Exam Exam { get; set; }
        public IEnumerable<Grade> Grades { get; set; }

        public Grade Grade { get; set; }
        public string SearchByExam { get; set; }
        public string SearchBySubject { get; set; }
        public DateTime SearchByDate { get; set; }
        public string SearchByGrade_Name { get; set; }

        public int? SearchByGrade_Point { get; set; }
    }
}
