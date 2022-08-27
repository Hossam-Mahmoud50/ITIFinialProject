using CenterApp.Core.Models;

using Microsoft.AspNetCore.Mvc.Rendering;

namespace CenterAppWeb.ViewModel
{
    public class StuffFileVM
    {
        public Stuff Stuff { get; set; }
        public IFormFile? file { get; set; }
    }
}
