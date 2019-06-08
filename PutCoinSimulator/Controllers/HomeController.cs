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
            manager = GetManager.Manager;
        }

        public ActionResult Index()
        {
            var model = new HomeIndexViewModel {RefreshPartialsMilisecondsInterval = 2000}; //2s
            return View(model);
        }

        public ActionResult Blockchain()
        {
            var model = manager.VisualisingWorker.GetBlockchain;
            return View("_BlockchainPartial", model);
        }

        public ActionResult CommisionedTransactions()
        {
            var model = manager.VisualisingWorker.GetCommisionedTransactions;
            return View("_CommisionedTransactionsPartial", model);
        }

        public ActionResult UnreleasedTransactions()
        {
            var model = manager.VisualisingWorker.GetUnreleasedTransactions;
            return View("_UnreleasedTransactionsPartial", model);
        }

        public ActionResult Pockets()
        {
            var model = manager.VisualisingWorker.GetPockets;
            return View("_PocketsPartial", model);
        }
    }
}