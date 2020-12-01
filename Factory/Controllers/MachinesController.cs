using Factory.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace Factory.Controllers
{
  public class MachinesController : Controller
  {
    private FactoryContext _db;
    public MachinesController(FactoryContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Machine> machines = _db.Machines.ToList();
      return View(machines);
    }

    public ActionResult Details(int id)
    {
      Machine machine = _db.Machines.Include(m => m.Engineers).ThenInclude(mc => mc.Engineer).FirstOrDefault(m => m.MachineId == id);
      return View(machine);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Machine machine)
    {
      _db.Machines.Add(machine);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Add(int id)
    {
      //i need to add a lambda (and a paramter to the actionresult method) to first or default to fix the bug
      Machine machine = _db.Machines.Include(m => m.Engineers).ThenInclude(me => me.Engineer).FirstOrDefault(x => x.MachineId == id);
      List<MachineEngineer> engineers = machine.Engineers.ToList();
      List<Engineer> filtered = _db.Engineers.Where(m => !(engineers.Any(x => m.EngineerId == x.EngineerId))).ToList();
      bool isNotEmpty = filtered.Count > 0;
      ViewBag.EngineerId = new SelectList(filtered, "EngineerId", "Name");
      return View(isNotEmpty);
    }

    [HttpPost]
    public ActionResult Add(int id, int engineerid)
    {
      _db.MachineEngineers.Add(new MachineEngineer() { MachineId = id, EngineerId = engineerid });
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = id });
    }

    public ActionResult Remove(int id)
    {
      MachineEngineer me = _db.MachineEngineers.Include(x => x.Machine).Include(x => x.Engineer).FirstOrDefault(x => x.MachineEngineerId == id);
      return View(me);
    }

    [HttpPost, ActionName("Remove")]
    public ActionResult RemoveEngineer(int id)
    {
      MachineEngineer me = _db.MachineEngineers.FirstOrDefault(x => x.MachineEngineerId == id);
      int engineerid = me.EngineerId;
      _db.MachineEngineers.Remove(me);
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = engineerid });
    }

    public ActionResult Edit(int id)
    {
      Machine machine = _db.Machines.FirstOrDefault(m => m.MachineId == id);
      return View(machine);
    }

    [HttpPost]
    public ActionResult Edit(Machine machine)
    {
      _db.Entry(machine).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = machine.MachineId });
    }

    public ActionResult Delete(int id)
    {
      Machine machine = _db.Machines.FirstOrDefault(m => m.MachineId == id);
      return View(machine);
    }

    [HttpPost]
    public ActionResult Delete(Machine machine)
    {
      _db.Machines.Remove(machine);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}