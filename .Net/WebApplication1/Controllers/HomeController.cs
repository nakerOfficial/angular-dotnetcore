using Microsoft.AspNetCore.Mvc;
using System.Xml;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private IHttpClientFactory _clientFactory;

        public HomeController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult GetRatesFromBank()
        {
            XmlTextReader reader = new XmlTextReader("https://www.bnr.ro/nbrfxrates.xml");
            XmlReaderParser readerHelper = new XmlReaderParser(reader);

            var elements = readerHelper.Elements;

            var result = elements
                .Where(a => a.Name == "Rate")
                .Select(a => new {
                    Name = a.Attributes.First(a => a.Key == "currency").Value,
                    Value = a.Text
                })
                .ToList();

            return Ok(result);
        }
    }
}
