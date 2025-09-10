using System.Web.Mvc;

namespace CrystalCarCare.Controllers
{
    [Authorize]
    public class ServicesController : Controller
    {
        public ActionResult CarWash()
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

        // ✅ New Rent action
        public ActionResult Rent()
        {
            return View();
        }
    }
}
