using CenterApp.Core.Models;

namespace CenterAppWeb.ViewModel
{
    public class AttendVM
    {

        public Student oneStudent { get; set; }
        public int Stage_Id { get; set; }
        public DateOnly AttendDate { get; set; }
        public bool IsAttend { get; set; }
    }
}
