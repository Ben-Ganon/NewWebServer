using Microsoft.AspNetCore.Mvc;

namespace WebAppServer1.Controllers
{
    public class ReactController : Controller
    {
        public IActionResult Index()
        {
            return File("index.html", "text/html");
        }
    }
}
