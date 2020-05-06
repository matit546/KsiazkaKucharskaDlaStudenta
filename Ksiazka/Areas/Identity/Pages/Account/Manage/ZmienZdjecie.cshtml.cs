using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Ksiazka.Data;
using Ksiazka.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ksiazka.Areas.Identity.Pages.Account.Manage
{
    public class ZmienZdjecieModel : PageModel
    {



        [BindProperty]
        public Uzytkownik Uzytkownik { get; set; }
        private readonly IHostingEnvironment _environment;
        private readonly ApplicationDbContext _baza;
        public ZmienZdjecieModel(ApplicationDbContext baza, IHostingEnvironment environment)
        {
            _baza = baza;
            _environment = environment;
        }
        [BindProperty]
        public IFormFile Zdjecie { get; set; }

        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPost()
        {

            Uzytkownik = _baza.Uzytkownik.Where(u => u.UserName.ToLower().Contains(User.Identity.Name.ToLower())).First();

            if (Zdjecie != null)
            {
                var file = Path.Combine(Environment.CurrentDirectory, "wwwroot/zdjecia", Zdjecie.FileName);
                using (var sciezka = new FileStream(file, FileMode.Create))
                {
                    await Zdjecie.CopyToAsync(sciezka);

                }
                Uzytkownik.Awatar = Zdjecie.FileName;
            }

            await _baza.SaveChangesAsync();
            return RedirectToPage("Index");
        }


    }
}