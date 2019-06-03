using System.Web.Mvc;
using ThreadManager;
using ThreadManager.Interfaces;

namespace PutCoinSimulator.Controllers
{
    public class HomeController : Controller
    {
        private readonly IManager manager;

        public HomeController()
        {
            manager = new Manager();
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}