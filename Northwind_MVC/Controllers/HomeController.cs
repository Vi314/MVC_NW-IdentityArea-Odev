using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Northwind_MVC.Models;
using Northwind_MVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind_MVC.Controllers
{
    public class HomeController : Controller
    {
        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signInManager;

        public HomeController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                var loginAcc = await _userManager.FindByNameAsync(model.Username);
                if (loginAcc != null)
                {
                    var didSignin = await _signInManager.PasswordSignInAsync(model.Username, model.Password,false,false);
                    if (didSignin.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return View(model);
                    }
                }
                else
                {
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                IdentityUser newUser = new IdentityUser
                {
                    UserName = model.Username, 
                    Email = model.Email
                };

                var isCreated = await _userManager.CreateAsync(newUser, model.PasswordRepeat);

                if (isCreated.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
