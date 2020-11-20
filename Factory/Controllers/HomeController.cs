using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Factory.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
      List<Engineer> es = _db.Engineers.Include(e => e.Machines).ThenInclude(me => me.Machine).ToList();

      List<Machine> ms = _db.Machines.Include(m => m.Engineers).ThenInclude(me => me.Engineer).ToList();

      return View((es,ms));
    }
  }
}