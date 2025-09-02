using System.Web.Mvc;

namespace CrystalCarCare.Controllers
{
    public class ServicesController : Controller
    {
        public ActionResult CarWash()  // Make sure the name matches the view
        {
            return View();
        }

        public ActionResult Mechanic()
        {
            return View();
        }

        public ActionResult Additional()
        {
            return View();
        }
    }
}
