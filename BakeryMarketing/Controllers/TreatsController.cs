using Microsoft.AspNetCore.Mvc;
using BakeryMarketing.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace BakeryMarketing.Controllers
{
  public class TreatsController : Controller
  {
    private readonly BakeryMarketingContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public TreatsController(UserManager<ApplicationUser> userManager, BakeryMarketingContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public ActionResult Index()
    {
      IEnumerable<Treat> sorted = _db.Treats.OrderBy(treat => treat.Name);
      return View(sorted);
    }

    public ActionResult Details(int id)
    {
      var thisTreat = _db.Treats
        .Include(Treat => Treat.JoinEntities)
        .ThenInclude(join => join.Flavor)
        .FirstOrDefault(treat => treat.TreatId == id);
      return View(thisTreat);
    }
    public ActionResult Create()
    {
      ViewBag.FlavorId = new SelectList(_db.Flavors, "FlavorId", "Name");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Treat treat, int FlavorId, string FlavorName)
    {
      _db.Treats.Add(treat);
      _db.SaveChanges();
      if (FlavorId != 0)
      {
        _db.TreatFlavor.Add(new TreatFlavor() { FlavorId = FlavorId, TreatId = treat.TreatId});
      }
      if (FlavorName != null)
      {
        Flavor newFlavor = new Flavor() { Name = FlavorName };
        _db.Flavors.Add(newFlavor);
        _db.SaveChanges();
        _db.TreatFlavor.Add(new TreatFlavor() { FlavorId = newFlavor.FlavorId, TreatId = treat.TreatId});
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    public ActionResult Edit(int id)
    {
      var thisTreat = _db.Treats.FirstOrDefault(treat => treat.TreatId == id);
      ViewBag.FlavorId = new SelectList(_db.Flavors, "FlavorId", "Name");
      return View(thisTreat);
    }

    [HttpPost]
    public ActionResult Edit(Treat treat, int FlavorId, string FlavorName)
    {
      if (FlavorId != 0)
      {
        _db.TreatFlavor.Add(new TreatFlavor() {FlavorId = FlavorId, TreatId = treat.TreatId});
      }
      if (FlavorName != null)
      {
        Flavor newFlavor = new Flavor() { Name = FlavorName };
        _db.Flavors.Add(newFlavor);
        _db.SaveChanges();
        _db.TreatFlavor.Add(new TreatFlavor() { FlavorId = newFlavor.FlavorId, TreatId = treat.TreatId});
      }
      _db.Entry(treat).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisTreat = _db.Treats.FirstOrDefault(treat => treat.TreatId == id);
      return View(thisTreat);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed (int id)
    {
      var thisTreat = _db.Treats.FirstOrDefault(treat => treat.TreatId == id);
      _db.Treats.Remove(thisTreat);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteFlavor(int joinId)
    {
      var joinEntry = _db.TreatFlavor.FirstOrDefault(entry => entry.TreatFlavorId == joinId);
      _db.TreatFlavor.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    
  }
}
