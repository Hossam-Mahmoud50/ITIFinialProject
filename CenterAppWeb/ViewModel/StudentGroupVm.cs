using CenterApp.Core.Models;

namespace CenterAppWeb.ViewModel
{
    public class StudentGroupVm
    {
        public int Matrial_Id { get; set; }  
        public int Teacher_Id { get; set; }
        public List<Teacher> Teachers { get; set; }
        public int Group_Id { get; set; }
    }
}
