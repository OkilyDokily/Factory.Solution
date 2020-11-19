using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Factory.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace Factory.Controllers
{
  public class EngineersController : Controller
  {
    private FactoryContext _db;
    public EngineersController(FactoryContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Engineer> engineers = _db.Engineers.ToList();
      return View(engineers);
    }

    public ActionResult Details(int id)
    {
      Engineer engineer = _db.Engineers.Include(e => e.Machines).ThenInclude(mc => mc.Machine).FirstOrDefault(e => e.EngineerId == id);
      return View(engineer);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Engineer engineer)
    {
      _db.Engineers.Add(engineer);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Add(int id)
    {
      ViewBag.MachineId = new SelectList(_db.Machines, "MachineId", "Name");
      return View();
    }

    [HttpPost]
    public ActionResult Add(int id, int machineid)
    {
        _db.MachineEngineers.Add
    }
  }
}