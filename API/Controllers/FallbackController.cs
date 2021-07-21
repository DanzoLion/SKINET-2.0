using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers 
{
    public class FallbackController : Controller            // index.html is provided as a view so we derive from Controller from our API server
    {
        public IActionResult Index()                        // we named this in our Startup.cs class
        {
            return PhysicalFile(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "index.html"), "text/HTML");
        }
    }
}