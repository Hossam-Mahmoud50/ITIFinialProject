using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CenterAppWeb.Controllers
{
    public class ChattingController : Controller
    {
        // GET: ChattingController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ChattingController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        
    }
}
