using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenterApp.Core.Models
{
    public class Exam
    {
        public int? Exam_Id { get; set; }
        public string Exam_Title { get; set; }
        public DateTime Exam_Start_Date { get; set; }
        public int Time { get; set; }
        public int? Group_Id { get; set; }
        public virtual Group? Group { get; set; }
        public int? Matrial_Id { get; set; }
        public virtual Matrial? Matrials { get; set; }
        public ICollection<Grade>? Grades { get; set; }

    }
}
