using Microsoft.AspNetCore.Mvc;
using Factory.Models;

namespace Factory.Controllers
{
  public class HomeController : Controller
  {
    private FactoryContext _db;
    public HomeController(FactoryContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      return View();
    }
  }    
}