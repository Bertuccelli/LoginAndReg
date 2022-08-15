using Microsoft.AspNetCore.Mvc;
using LoginAndReg.Models;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
namespace LoginAndReg.Controllers;

public class UserController : Controller
{
    private LoginAndRegContext db;
    public UserController(LoginAndRegContext context)
    {
        db = context;
    }
    private int? UUID
    {
        get
        {
            return HttpContext.Session.GetInt32("UUID");
        }
    }
    private bool loggedIn
    {
        get
        {
            return UUID != null;
        }
    }


    [HttpGet("/")]
    public IActionResult Index()
    {
        if(loggedIn)
        {
            return Success();
        }
        return View("Index");
    }


    [HttpPost("/register")]
    public IActionResult Register(User newUser)
    {
        if(ModelState.IsValid)
        {
            if(db.users.Any(User => User.email == newUser.email))
            {
                ModelState.AddModelError("email", "is taken");
            }
        }
        if(ModelState.IsValid == false)
        {
            return Index();
        }

        PasswordHasher<User> hasher = new PasswordHasher<User>();
        newUser.password = hasher.HashPassword(newUser, newUser.password);
        db.users.Add(newUser);
        db.SaveChanges();
        HttpContext.Session.SetInt32("UUID", newUser.id);
        return Success();
    }


    [HttpGet("/login")]
    public IActionResult Enter()
    {
        if(loggedIn)
        {
            return Success();
        }
        return View("Index");
    }

    [HttpPost("/loggingin")]
    public IActionResult Login(LoginUser loginUser)
    {
        if(ModelState.IsValid == false)
        {
            return Enter();
        }
        User? dbUser = db.users.FirstOrDefault(loggedUser => loggedUser.email == loginUser.LoginEmail);
        if (dbUser == null)
        {
            ModelState.AddModelError("LoginEmail", "not found");
            return Enter();
        }

        PasswordHasher<LoginUser> hasher = new PasswordHasher<LoginUser>();
        PasswordVerificationResult pwCompare = hasher.VerifyHashedPassword(loginUser, dbUser.password, loginUser.LoginPassword);

        if(pwCompare == 0)
        {
            ModelState.AddModelError("LoginPassword", "isn't correct");
            return Enter();
        }
        HttpContext.Session.SetInt32("UUID", dbUser.id);
        return Success();
    }


    [HttpGet("/success")]
    public IActionResult Success()
    {
        if(!loggedIn)
        {
            return Enter();
        }
        return View("Success");
    }


    [HttpPost("/logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return Enter();
    }
}