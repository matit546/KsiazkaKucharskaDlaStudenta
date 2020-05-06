using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Ksiazka.Data;
using Ksiazka.Model;
using Ksiazka.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Ksiazka.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;

        public RegisterModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext db
            )

        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _db = db;
            _roleManager = roleManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required( ErrorMessage ="Wpisz Email")]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Wpisz Has³o")]
            [StringLength(100, ErrorMessage = "{0} musi miec minimalnie {2} i maksymalnie {1} znaków.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Haslo")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Potwierdz Haslo")]
            [Compare("Password", ErrorMessage = "Wprowadz takie same haslo")]
            public string ConfirmPassword { get; set; }
            [Required(ErrorMessage = "Podaj nazwê u¿ytkownika")]
            [Display(Name = "Nazwa u¿ytkownika")]
            public string Nazwa_uzytkownika { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new Uzytkownik
                {
                    UserName = Input.Nazwa_uzytkownika,
                    Email = Input.Email,
                    Nazwa_uzytkownika = Input.Nazwa_uzytkownika,
                    Data_zalozenia= DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString(),
                    Awatar="Default.jpg"

            };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    if (!await _roleManager.RoleExistsAsync(Role.Administrator))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(Role.Administrator));
                    }
                    if (!await _roleManager.RoleExistsAsync(Role.Uzytkownik))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(Role.Uzytkownik));
                    }
                    await _userManager.AddToRoleAsync(user, Role.Uzytkownik);
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
