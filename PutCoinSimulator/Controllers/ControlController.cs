using System.Web.Mvc;
using Common;
using PutCoinSimulator.ViewModels.Control;
using ThreadManager;
using ThreadManager.Interfaces;

namespace PutCoinSimulator.Controllers
{
    public class ControlController : Controller
    {
        private readonly IManager manager;

        public ControlController()
        {
            manager = GetManager.Manager;
        }

        public ActionResult Index(string message, bool isSuccess = true)
        {
            var model = new ControlIndexViewModel
            {
                NumbersOfClients = Settings.NumbersOfClients,
                AppStarted = Settings.AppStarted,
                Message = message,
                IsSuccess = isSuccess
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult NumbersOfClients(ControlIndexViewModel model)
        {
            string message;
            if (model.NumbersOfClients <= 0)
            {
                message = "Numbers of clients has to be > 0";
                return RedirectToAction(nameof(Index), new { message, isSuccess = false });
            }
            if (model.NumbersOfClients == Settings.NumbersOfClients)
            {
                message = "Numbers has the same value";
                return RedirectToAction(nameof(Index), new { message, isSuccess = false });
            }
            Settings.NumbersOfClients = model.NumbersOfClients;
            message = "Numbers of clients updated successfully";
            return RedirectToAction(nameof(Index), new { message });
        }

        [HttpPost]
        public ActionResult StartApp(ControlIndexViewModel model)
        {
            Settings.AppStarted = !model.AppStarted;
            if (Settings.AppStarted)
                manager.StartWorks();
            else
                manager.StopWorks();

            var message = $"App { (Settings.AppStarted ? "Started" : "Stopped") }";
            return RedirectToAction(nameof(Index), new { message });
        }
    }
}