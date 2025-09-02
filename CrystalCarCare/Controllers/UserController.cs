using System.Web.Mvc;

namespace CrystalCarCare.Controllers
{
    public class UserController : Controller
    {
        // GET: /User/BookingStatus
        public ActionResult BookingStatus()
        {
            return View(); // Views/User/BookingStatus.cshtml
        }

        // You can add other user pages here (Profile, History, etc.)
    }
}
