using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Ksiazka.Data;
using Ksiazka.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ksiazka.Pages.Przepisy
{
    public class DodawaniePrzepisuModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IHostingEnvironment _environment;
        private readonly ApplicationDbContext _baza;
        public DodawaniePrzepisuModel(ApplicationDbContext baza, IHostingEnvironment environment, UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _baza = baza;
            _environment = environment;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [BindProperty]
        public Przepis Przepis { get; set; }

        [BindProperty, Required(ErrorMessage = "Trudnośc wymagana!")]
        public string TrudnoscPrzepisu { get; set; }
        public string[] Trudnosci = new[] { "Łatwy", "Średni","Trudny"};

        //Trudnosc_PPrxepis    TrudnoscPrzepisu

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToPage("/Index");
            }
            return Page();
        }

        [BindProperty]
        public IFormFile Zdjecie { get; set; }
        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
         
            Przepis.Data_Dodania = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString();
            Przepis.Autor = User.Identity.Name;
            if (Przepis.Kategoria_2 == null) { Przepis.Kategoria_2 = "brak"; }
            if (Przepis.Kategoria_3 == null) { Przepis.Kategoria_3 = "brak"; }
            Przepis.Trudnosc = TrudnoscPrzepisu;
            //Directory.CreateDirectory("C:/Users/matit/Desktop/projektrazorpages/KsiazkaKucharska/KsiazkaKucharska/zdjecia/");
            //string filename = Path.GetFileNameWithoutExtension(Przepis.Zdjecie);
            //string extension = Path.GetExtension(Przepis.Zdjecie);
            //filename = filename + DateTime.Now.ToString("yymmssff") + extension;
            //   Przepis.Zdjecie = "C:/Users/matit/Desktop/projektrazorpages/KsiazkaKucharska/KsiazkaKucharska/zdjecia/" + filename;
            //string pathfile = Path.Combine("C:/Users/matit/Desktop/projektrazorpages/KsiazkaKucharska/KsiazkaKucharska/zdjecia/", filename);
            //using (var item = new FileStream(pathfile, FileMode.Create))
            //{
            //    item.CopyTo(item);
            //}
            if (Zdjecie != null)
            {
                var file = Path.Combine(Environment.CurrentDirectory, "wwwroot/zdjecia", Zdjecie.FileName);
                using (var sciezka = new FileStream(file, FileMode.Create))
                {
                    await Zdjecie.CopyToAsync(sciezka);

                }
                Przepis.Zdjecie = Zdjecie.FileName;
            }
            else { Przepis.Zdjecie = "tlo.jpg"; }

            _baza.Przepis.Add(Przepis);
            await _baza.SaveChangesAsync();
            return RedirectToPage("Przepisy");
        }
    }
}