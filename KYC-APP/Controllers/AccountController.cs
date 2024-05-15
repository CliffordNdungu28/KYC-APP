using KYC_APP.Static;
using KYC_APP.Data.ViewModels;
using KYC_APP.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KYC_APP.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager; 

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signinManager)
        {
            _userManager = userManager;
            _signInManager = signinManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View(new RegisterVM());
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginvm)
        {
            if (!ModelState.IsValid) return View(loginvm);
            var user = await _userManager.FindByEmailAsync(loginvm.emailaddress);

            if (user != null)
            {
                var passwordcheck = await _userManager.CheckPasswordAsync(user, loginvm.Password);
                if (passwordcheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginvm.Password, false, false);
                    if (result.Succeeded)
                    {

                        var roles = await _userManager.GetRolesAsync(user);

                        // Check user role and redirect accordingly
                        if (roles.Contains(UserRoles.Admin))
                        {
                            return RedirectToAction("DocumentTypeManager", "Portal");
                        }
                        else if (roles.Contains(UserRoles.User))
                        {
                            return RedirectToAction("DocumentManager", "Portal");
                        }
                        else if (roles.Contains(UserRoles.SP))
                        {
                            return RedirectToAction("DatabaseManager", "Portal");
                        }
                    }
                    if (result.IsNotAllowed)
                    {
                        ModelState.AddModelError("", "Not allowed to login");
                    }

                }
                TempData["Error"] = "Wrong credentials. Please try again";
                return View(loginvm);
            }
            TempData["Error"] = "Wrong credentials. Please try again";
            return View(loginvm);


        }

        public IActionResult Login() => View(new LoginVM());



        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registervm)

        {
            if (!ModelState.IsValid) return View(registervm);

            var user = await _userManager.FindByEmailAsync(registervm.Email);
           
                if (user != null)
                {
                    TempData["Error"] = "This email address is already in use.";
                    return View(registervm);
                }

                if (registervm.Institution != null)
                {
                    var newUser = new ApplicationUser()
                    {
                        Email = registervm.Email,
                        FullName = registervm.FullName,
                        Firstname = registervm.Firstname,
                        Lastname = registervm.Lastname,
                        Institution = registervm.Institution,
                        Category = registervm.Category,
                        //Industry = registervm.Industry
                        PhoneNumber = registervm.PhoneNumber,
                        UserName = registervm.Email,


                      


                    };
                    var newuserresponse = await _userManager.CreateAsync(newUser, registervm.Password);
                    if (newuserresponse.Succeeded)
                    {
                        //await _accountRepository.GenerateEmailConfirmationTokenAsync(newUser);
                        await _userManager.AddToRoleAsync(newUser, UserRoles.SP);
                    };

                }
                else
                {
                    var newUser = new ApplicationUser()
                    {
                        Email = registervm.Email,
                        FullName = registervm.FullName,
                        Firstname = registervm.Firstname,
                        Lastname = registervm.Lastname,
                        PhoneNumber = registervm.PhoneNumber,
                        UserName = registervm.Email,

                        //datacenterlocation = registervm.SelectedLocation.ToString(),


                    };
                    var newuserresponse = await _userManager.CreateAsync(newUser, registervm.Password);
                    if (newuserresponse.Succeeded)
                    {
                        //await _accountRepository.GenerateEmailConfirmationTokenAsync(newUser);
                        await _userManager.AddToRoleAsync(newUser, UserRoles.User);
                    };
                }

                //return RedirectToAction("ConfirmEmail", new { email = registervm.Email });
                return RedirectToAction("ConfirmEmail");


            
        }

        //[HttpPost("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(/*EmailConfirmModel model*/)
        {
            //var user = await _accountRepository.GetUserByEmailAsync(model.Email);
            //if (user != null)
            //{
            //    if (user.EmailConfirmed)
            //    {
            //        model.isConfirmed = true;
            //        return View(model);

            //    }
            //    await _accountRepository.GenerateEmailConfirmationTokenAsync(user);
            //    model.EmailSent = true;
            //    ModelState.Clear();
            //}
            //else
            //{
            //    ModelState.AddModelError("", "Something went wrong.");
            //}


            //return View(model);

            return View();

        }
    }
}
