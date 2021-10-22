using Microsoft.AspNetCore.Mvc;

namespace BakeryMarketing.Controllers
{
  public class HomeController : Controller
  {
    [HttpGet("/")]
    public ActionResult Index() { 
      return View(); 
    }
  }
}