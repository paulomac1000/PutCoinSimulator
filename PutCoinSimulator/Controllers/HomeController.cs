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

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}