using System.Collections.Generic;
using System.Web.Mvc;
using Models;
using PutCoinSimulator.ViewModels.Home;
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
            var model = new HomeIndexViewModel();
            model.RefreshPartialsMilisecondsInterval = 2000; //2s
            return View(model);
        }

        public ActionResult Blockchain()
        {
            IEnumerable<Block> model = null;
            return View("_BlockchainPartial", model);
        }

        public ActionResult CommisionedTransactions()
        {
            IEnumerable<Transaction> model = null;
            return View("_CommisionedTransactionsPartial", model);
        }

        public ActionResult UnreleasedTransactions()
        {
            IEnumerable<Transaction> model = null;
            return View("_UnreleasedTransactionsPartial", model);
        }

        public ActionResult Pockets()
        {
            IEnumerable<PocketViewModel> model = null;
            return View("_PocketsPartial", model);
        }
    }
}