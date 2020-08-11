using System.Web.Mvc;
using WebApi1.Dapper;


namespace WebApi1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            UserDapper userDapper = new UserDapper();
            return View();
        }
    }
}
