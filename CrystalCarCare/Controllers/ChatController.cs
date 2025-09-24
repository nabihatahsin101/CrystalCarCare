using CrystalCarCare.Services;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CrystalCarCare.Controllers
{
    public class ChatController : Controller
    {
        private readonly GeminiService _geminiService = new GeminiService();

        [HttpPost]
        public async Task<ActionResult> Ask(string userMessage)
        {
            if (string.IsNullOrWhiteSpace(userMessage))
                return Json(new { response = "Please type a message." }, JsonRequestBehavior.AllowGet);

            var reply = await _geminiService.AskGeminiAsync(userMessage);
            return Json(new { response = reply }, JsonRequestBehavior.AllowGet);
        }
    }
}
