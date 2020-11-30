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
      Engineer engineer = _db.Engineers.Include(e => e.Machines).ThenInclude(me => me.Machine).FirstOrDefault();
      List<MachineEngineer> machines = engineer.Machines.ToList();
      List<Machine> filtered = _db.Machines.Where(m => !(machines.Any(x => m.MachineId == x.MachineId))).ToList();
      bool isNotEmpty = filtered.Count > 0;
      ViewBag.MachineId = new SelectList(filtered, "MachineId", "Name");
      return View(isNotEmpty);
    }

    [HttpPost]
    public ActionResult Add(int id, int machineid)
    {
      _db.MachineEngineers.Add(new MachineEngineer() { EngineerId = id, MachineId = machineid });
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = id });
    }

    public ActionResult Remove(int id)
    {
      MachineEngineer me = _db.MachineEngineers.Include(x => x.Machine).Include(x => x.Engineer).FirstOrDefault(x => x.MachineEngineerId == id);
      return View(me);
    }

    [HttpPost, ActionName("Remove")]
    public ActionResult RemoveMachine(int id)
    {
      MachineEngineer me = _db.MachineEngineers.FirstOrDefault(x => x.MachineEngineerId == id);
      int engineerid = me.EngineerId;
      _db.MachineEngineers.Remove(me);
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = engineerid });
    }

    public ActionResult Edit(int id)
    {
      Engineer engineer = _db.Engineers.FirstOrDefault(e => e.EngineerId == id);
      return View(engineer);
    }

    [HttpPost]
    public ActionResult Edit(Engineer engineer)
    {
      _db.Entry(engineer).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = engineer.EngineerId });
    }

    public ActionResult Delete(int id)
    {
      Engineer engineer = _db.Engineers.FirstOrDefault(m => m.EngineerId == id);
      return View(engineer);
    }


    [HttpPost]
    public ActionResult Delete(Engineer engineer)
    {
      _db.Engineers.Remove(engineer);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}