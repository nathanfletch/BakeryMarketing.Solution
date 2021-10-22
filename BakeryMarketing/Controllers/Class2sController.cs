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
  [Authorize]
  public class Class1sController : Controller
  {
    private readonly BakeryMarketingContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public Class1sController(UserManager<ApplicationUser> userManager, BakeryMarketingContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    public async Task<ActionResult> Index()
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      var userClass1s = _db.Class1s.Where(entry => entry.User.Id == currentUser.Id).ToList();
      return View(userClass1s);
    }

    public ActionResult Details(int id)
    {
      var thisClass1 = _db.Class1s
        .Include(Class1 => Class1.JoinEntities)
        .ThenInclude(join => join.Class2)
        .FirstOrDefault(Class1 => Class1.Class1Id == id);
      return View(thisClass1);
    }

    // public ActionResult Complete(int id)
    // {
    //   var thisClass1 = _db.Class1s.FirstOrDefault(Class1 => Class1.Class1Id == id);
    //   return View(thisClass1);
    // }

    // [HttpPost, ActionName("Complete")]
    // public ActionResult CompleteConfirm(int id, Class1 Class1, bool Complete)
    // {
    //   if (Complete != true)
    //   {
    //     var thisClass1 = _db.Class1s.FirstOrDefault(Class1 => Class1.Class1Id == id);
    //     thisClass1.Complete = true;
    //     _db.Entry(thisClass1).State = EntityState.Modified;
    //     _db.SaveChanges();
    //   }
    //   return RedirectToAction("Index");
    // }

    public ActionResult AddClass2(int id)
    {
      var thisClass1 = _db.Class1s.FirstOrDefault(Class1 => Class1.Class1Id == id);
      ViewBag.Class2Id = new SelectList(_db.Class2s, "Class2Id", "Name");
      return View(thisClass1);
    }

    [HttpPost]
    public ActionResult AddClass2(Class1 Class1, int Class2Id)
    {
      if (Class2Id != 0)
      {
        _db.Class2Class1.Add(new Class2Class1() {Class2Id = Class2Id, Class1Id = Class1.Class1Id});
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Edit(int id)
    {
      var thisClass1 = _db.Class1s.FirstOrDefault(Class1 => Class1.Class1Id == id);
      ViewBag.Class2Id = new SelectList(_db.Class2s, "Class2Id", "Name");
      return View(thisClass1);
    }

    [HttpPost]
    public ActionResult Edit(Class1 Class1, int Class2Id)
    {
      if (Class2Id != 0)
      {
        _db.Class2Class1.Add(new Class2Class1() {Class2Id = Class2Id, Class1Id = Class1.Class1Id});
      }
      _db.Entry(Class1).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisClass1 = _db.Class1s.FirstOrDefault(Class1 => Class1.Class1Id == id);
      return View(thisClass1);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed (int id)
    {
      var thisClass1 = _db.Class1s.FirstOrDefault(Class1 => Class1.Class1Id == id);
      _db.Class1s.Remove(thisClass1);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteClass2(int joinId)
    {
      var joinEntry = _db.Class2Class1.FirstOrDefault(entry => entry.Class2Class1Id == joinId);
      _db.Class2Class1.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Create()
    {
        ViewBag.Class2Id = new SelectList(_db.Class2s, "Class2Id", "Name");
        return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Class1 Class1, int Class2Id)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      Class1.User = currentUser;
      _db.Class1s.Add(Class1);
      _db.SaveChanges();
      if (Class2Id != 0)
      {
        _db.Class2Class1.Add(new Class2Class1() { Class2Id = Class2Id, Class1Id = Class1.Class1Id});
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}
