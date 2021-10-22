using Microsoft.AspNetCore.Mvc;
using BakeryMarketing.Models;
using System.Threading.Tasks;
using BakeryMarketing.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace BakeryMarketing.Controllers
{
  public class AccountController : Controller
  {
    private readonly BakeryMarketingContext _db;
    private RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AccountController (RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, BakeryMarketingContext db)
    {
      _roleManager = roleManager;
      _userManager = userManager;
      _signInManager = signInManager;
      _db = db;
    }

    public async Task<IActionResult> Index()
    {
      if(_roleManager.Roles.ToList().Count == 0)
      {
        IdentityResult chefResult = await _roleManager.CreateAsync(new IdentityRole("Chef"));
        IdentityResult managerResult = await _roleManager.CreateAsync(new IdentityRole("Manager"));
      }
      return View();
    }

    public IActionResult Register()
    {
      List<IdentityRole> roles = _roleManager.Roles.ToList();
      ViewBag.Id = new SelectList(_roleManager.Roles, "Id", "Name");
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Register (RegisterViewModel model, string Id)
    {
      var user = new ApplicationUser { UserName = model.Email };
      IdentityResult result = await _userManager.CreateAsync(user, model.Password);
      if (result.Succeeded)
      {
        var selectedRole = await _roleManager.FindByIdAsync(Id);
        IdentityResult roleAddResult = await _userManager.AddToRoleAsync(user, selectedRole.Name);
        return RedirectToAction("Index");
      }
      else
      {
          return View();
      }
    }

    public ActionResult Login()
    {
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Login(LoginViewModel model)
    {
      Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: true, lockoutOnFailure: false);
      if (result.Succeeded)
      {
        return RedirectToAction("Index");
      }
      else
      {
        return View();
      }
    }

    [HttpPost]
    public async Task<ActionResult> LogOff()
    {
      await _signInManager.SignOutAsync();
      return RedirectToAction("Index");
    }
  }
}