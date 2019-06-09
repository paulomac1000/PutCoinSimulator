using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Models;
using PutCoinSimulator.ViewModels.Home;
using Repository;
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

        public ActionResult RejectedTransactions()
        {
            var model = manager.VisualisingWorker.GetRejectedTransactions;
            return View("_RejectedTransactionsPartial", model);
        }

        public ActionResult Pockets()
        {
            var model = manager.VisualisingWorker.GetPockets?.Select(p => new PocketViewModel
            {
                OwnerName = p.OwnerName,
                PrivateKey = p.PrivateKey,
                PublicKey = p.PublicKey,
                AccountBalance = Helpers.GetAccountBalanceByOwnerName(p.OwnerName)
            });

            return View("_PocketsPartial", model);
        }
    }
}