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
  public class FlavorsController : Controller
  {
    private readonly BakeryMarketingContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public FlavorsController(UserManager<ApplicationUser> userManager, BakeryMarketingContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public ActionResult Index()
    {
      IEnumerable<Flavor> sorted = _db.Flavors.OrderBy(flavor => flavor.Name);
      return View(sorted);
    }

    public ActionResult Details(int id)
    {
      var thisFlavor = _db.Flavors
        .Include(flavor => flavor.JoinEntities)
        .ThenInclude(join => join.Treat)
        .FirstOrDefault(flavor => flavor.FlavorId == id);
      return View(thisFlavor);
    }
    public ActionResult Create()
    {
      ViewBag.TreatId = new SelectList(_db.Treats, "TreatId", "Name");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Flavor flavor, int TreatId, string TreatName)
    {
      _db.Flavors.Add(flavor);
      _db.SaveChanges();
      if (TreatId != 0)
      {
        _db.TreatFlavor.Add(new TreatFlavor() { TreatId = TreatId, FlavorId = flavor.FlavorId});
      }
      if (TreatName != null)
      {
        Treat newTreat = new Treat() { Name = TreatName };
        _db.Treats.Add(newTreat);
        _db.SaveChanges();
        _db.TreatFlavor.Add(new TreatFlavor() { TreatId = newTreat.TreatId, FlavorId = flavor.FlavorId});
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    public ActionResult Edit(int id)
    {
      var thisFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
      ViewBag.TreatId = new SelectList(_db.Treats, "TreatId", "Name");
      return View(thisFlavor);
    }

    [HttpPost]
    public ActionResult Edit(Flavor flavor, int TreatId, string TreatName)
    {
      if (TreatId != 0)
      {
        _db.TreatFlavor.Add(new TreatFlavor() {TreatId = TreatId, FlavorId = flavor.FlavorId});
      }
      if (TreatName != null)
      {
        Treat newTreat = new Treat() { Name = TreatName };
        _db.Treats.Add(newTreat);
        _db.SaveChanges();
        _db.TreatFlavor.Add(new TreatFlavor() { TreatId = newTreat.TreatId, FlavorId = flavor.FlavorId});
      }
      _db.Entry(flavor).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
      return View(thisFlavor);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed (int id)
    {
      var thisFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
      _db.Flavors.Remove(thisFlavor);
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
