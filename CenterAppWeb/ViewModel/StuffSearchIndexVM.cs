using CenterApp.Core.Models;

namespace CenterAppWeb.ViewModel
{
    public class StuffSearchIndexVM
    {
        public ICollection<Stuff> Stuff { get; set; }
        public int? SearchByID { get; set; }
        public string SearchByName { get; set; }
        public string SearchByPhone { get; set; }
    }
}
