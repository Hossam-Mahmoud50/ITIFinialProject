using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenterApp.Core.Models
{
    public class StudentAttend
    {
        public int Attend_Id { get; set; }
        public int? Student_Id { get; set; }
        public Student? Student { get; set; }
        public int? Stage_Id { get; set; }
        public Stage? Stage { get; set; }
        public DateTime AttendDate { get; set; }
        public bool IsAttend { get; set; }

    }
}
