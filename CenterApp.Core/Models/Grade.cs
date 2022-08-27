using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenterApp.Core.Models
{
    public class Grade
    {
        public int? Grade_Id { get; set; }
        public string Grade_Name { get; set; }
        public int? Grade_Point { get; set; }
        public int? Percentage_From { get; set; }
        public int? Percentage_Upto { get; set; }
        public int? Exam_Id { get; set; }
        public int? Student_Id { get; set; }
        public virtual Exam? Exams { get; set; }
        public Student? student { get; set; }


    }
}
