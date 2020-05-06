using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Ksiazka.Data;
using Ksiazka.Model;
using Ksiazka.Utility;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ksiazka.Pages.Przepisy
{
    public class EdycjaModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private IHostingEnvironment _environment;
        private ApplicationDbContext _baza;

        public EdycjaModel(ApplicationDbContext baza, IHostingEnvironment environment, UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _baza = baza;
            _environment = environment;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [BindProperty]
        public string Trudnosc { get; set; }
        public string[] Trudnosci = new[] { "Łatwy", "Średni", "Trudny" };
        [BindProperty]
        public Przepis Przepis { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
           
            Przepis = await _baza.Przepis.FindAsync(id);
            if (Przepis == null)
            {
                return RedirectToPage("/Index");
            }
          
            return Page();
        }
        public IFormFile Zdjecie { get; set; }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var Przepis_z_bazy = await _baza.Przepis.FindAsync(Przepis.Id);
                Przepis_z_bazy.Nazwa = Przepis.Nazwa;
                Przepis_z_bazy.Opis = Przepis.Opis;
                Przepis_z_bazy.Skladniki = Przepis.Skladniki;
                Przepis_z_bazy.Kategoria_1 = Przepis.Kategoria_1;
                Przepis_z_bazy.Kategoria_2 = Przepis.Kategoria_2;
                Przepis_z_bazy.Kategoria_3 = Przepis.Kategoria_3;
                Przepis_z_bazy.Czas = Przepis.Czas;
                Przepis_z_bazy.Trudnosc = Przepis.Trudnosc;

                if (Zdjecie != null)
                {
                    var file = Path.Combine(Environment.CurrentDirectory, "wwwroot/zdjecia", Zdjecie.FileName);
                    using (var sciezka = new FileStream(file, FileMode.Create))
                    {
                        await Zdjecie.CopyToAsync(sciezka);

                    }
                    Przepis_z_bazy.Zdjecie = Zdjecie.FileName;
                }
                else { Przepis.Zdjecie = "tlo.jpg"; }
                await _baza.SaveChangesAsync();

                return RedirectToPage("Przepisy");
            }
            return Page();
        }
    }
}