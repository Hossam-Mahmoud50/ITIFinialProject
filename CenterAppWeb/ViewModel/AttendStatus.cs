using CenterApp.Core.Models;

namespace CenterAppWeb.ViewModel
{
    public class AttendStatus
    {
        public int Attend_Id { get; set; }
        public bool IsAttend { get; set; }
        public string Stage_Name { get; set; }
        public DateTime AttendDate { get; set; }
        public Student oneStudent { get; set; }
    }
}
