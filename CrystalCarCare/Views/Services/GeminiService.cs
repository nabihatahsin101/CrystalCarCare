using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using Newtonsoft.Json;

namespace CrystalCarCare.Services
{
    public class GeminiService
    {
        private readonly string apiKey;
        private readonly string endpoint = "https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash:generateContent";

        public GeminiService()
        {
            apiKey = WebConfigurationManager.AppSettings["GeminiApiKey"];
        }

        public async Task<string> AskGeminiAsync(string prompt)
        {
            using (var client = new HttpClient())
            {
                var request = new
                {
                    contents = new[]
                    {
                        new {
                            parts = new[] {
                                new { text = prompt }
                            }
                        }
                    }
                };

                var json = JsonConvert.SerializeObject(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync($"{endpoint}?key={apiKey}", content);
                var responseString = await response.Content.ReadAsStringAsync();

                dynamic result = JsonConvert.DeserializeObject(responseString);

                try
                {
                    return result.candidates[0].content.parts[0].text.ToString();
                }
                catch
                {
                    return "Sorry, I could not get a response.";
                }
            }
        }
    }
}
