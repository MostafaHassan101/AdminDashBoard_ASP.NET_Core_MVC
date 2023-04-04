using AdminDashboard.Models;
using Context;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace AdminDashboard.Controllers
{

    //[Authorize(Roles = "Admin")]
    //[Authorize]
    public class UserController : Controller
    {
        private readonly DContext context;
        UserManager<User> usermanager;
        SignInManager<User> SignInManager;
        //RoleManager<IdentityRole> RoleManager;
        public UserController(DContext _context, UserManager<User> _usermanager, SignInManager<User> signInManager)
        {
            usermanager= _usermanager;
           context = _context;
            SignInManager = signInManager;
            
        }

        // GET: UserController/Create Account

        public ActionResult Create()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SignUp()
        {
           
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(UserCreateModel model)
        {
            if (ModelState.IsValid == false)
            {
                return View();
            }
            else
            {
                User user = new User()
                {
                   
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    UserName = model.UserName,
                };
                IdentityResult result = await usermanager.CreateAsync(user, model.Password);

                if (result.Succeeded == false)
                {
                    foreach (var er in result.Errors.ToList())
                    {
                        ModelState.AddModelError("", er.Description);
                    }
                    return View();
                    
                }
                else
                {
                    IdentityResult result2=await usermanager.AddToRoleAsync(user,"Admin");

                    if (result2.Succeeded == false)
                    {
                        foreach (var er in result.Errors.ToList())
                        {
                            ModelState.AddModelError("", er.Description);
                        }
                        return View();

                    }
                    // return RedirectToAction("UsersDetails", "User");
                    return Ok();
                }
            }
        }








        // GET: UserController/Details/5
        //   [Authorize]
        // [Authorize(Roles = "Admin")]
        //[Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task< ActionResult> UsersDetails()
        {
             List<User> userss = new List<User>();
            var users = context.Users.ToList();
            foreach (var user in users)
            {

                if (await usermanager.IsInRoleAsync( user, "Admin"))
                {
                    userss.Add(user);
                }
            }

            //    List<User> userssx =  usermanager.GetUsersInRoleAsync("customer").Result.ToList();

            ViewBag.users = userss;
            return View();
        }




        // UserController/Login

        [AllowAnonymous]
        [HttpGet]
        public IActionResult SignIn()
        {
           // ViewBag.ReturnUrl = ReturnUrl;
            ViewBag.Title = "Sign In";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(LoginModel model)
        {
            if (ModelState.IsValid == false)
            {
                return View();
            }
            else
            {
                var result =await SignInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);

                if (result.Succeeded == false)
                {
                    ModelState.AddModelError("", "Invalid username or password");
                    return View();

                }
                else
                {
                    return RedirectToAction("Index", "Home");

                    //if (!string.IsNullOrEmpty(model.ReturnUrl))
                    //{
                    //    return LocalRedirect(model.ReturnUrl);
                    //}
                    //else
                    //{
                    //    return RedirectToAction("Index", "Home");
                    //}
                }
            }
                
        }




        // UserController/ Log out
        [HttpGet]
        public  async Task<IActionResult> SignOut()
        {
            await SignInManager.SignOutAsync();

            return RedirectToAction("SignIn");

        }



    

        //  UserController/Edit
        public ActionResult Edit(int id)
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }





        // UserController/Delete
        public async Task<ActionResult> Delete(long id)
        {
            var user = await usermanager.FindByIdAsync(id.ToString());
            if(user != null)
            {
               IdentityResult result= await usermanager.DeleteAsync(user);
              //  context.SaveChanges();
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(UsersDetails));
                   // return View("UsersDetails");
                }else
                {
                    foreach(var er in result.Errors)
                    {
                        ModelState.AddModelError("", er.Description);
                    }
                }
            }

            return RedirectToAction(nameof(UsersDetails));
        }






        // All Reviews & All Users

        [HttpGet]
        public IActionResult AllReviews()
        {
            var ProductReviews = context.ProductReviews.Include("Product").ToList();
            ViewBag.ProductReviews = ProductReviews;

            return View(ProductReviews);
        }

        [HttpGet]
        public IActionResult AllUsers()
        {
            var Users = context.Users.ToList();
            ViewBag.Users = Users;

            return View(Users);
        }
    }
}
