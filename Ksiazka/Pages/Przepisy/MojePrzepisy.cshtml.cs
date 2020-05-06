using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ksiazka.Data;
using Ksiazka.Model;
using Ksiazka.Model.widok;
using Ksiazka.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Ksiazka.Pages.Przepisy
{
    public class MojePrzepisyModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ApplicationDbContext _baza;

        public MojePrzepisyModel(ApplicationDbContext baza, UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _baza = baza;
        }
        [BindProperty]
        public Przepis Przepis { get; set; }
        [BindProperty]
        public listaPrzepisow listaPrzepisow { get; set; }
        public async Task<IActionResult> OnGetAsync(int strona=1)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToPage("/Index");
            }
            if (User.IsInRole(Role.Administrator))
            {
                listaPrzepisow = new listaPrzepisow()
                {
                    Przepisy = await _baza.Przepis.ToListAsync()
                };

                StringBuilder param = new StringBuilder();
                param.Append("/Przepisy/MojePrzepisy?strona=:");
                var count = listaPrzepisow.Przepisy.Count;


                listaPrzepisow.Paging = new Paging
                {
                    CurrentPage = strona,
                    Itemsperpage = Role.liczba,
                    TotalItems = count,
                    UrlParam = param.ToString()
                };

                listaPrzepisow.Przepisy = listaPrzepisow.Przepisy.OrderBy(x => x.Nazwa).Skip((strona - 1) *Role.liczba).Take(Role.liczba).ToList();
            }
            else
            {
                listaPrzepisow = new listaPrzepisow() {
                Przepisy = await _baza.Przepis.Where(u => u.Autor.ToLower().Contains(User.Identity.Name.ToLower())).ToListAsync()
            };
                StringBuilder param = new StringBuilder();
                param.Append("/Przepisy/MojePrzepisy?strona=:");
                var count = listaPrzepisow.Przepisy.Count;

                listaPrzepisow.Paging = new Paging
                {
                    CurrentPage = strona,
                    Itemsperpage = Role.liczba,
                    TotalItems = count,
                    UrlParam = param.ToString()
                };
                listaPrzepisow.Przepisy = listaPrzepisow.Przepisy.OrderBy(x => x.Nazwa).Skip((strona - 1) * Role.liczba).Take(Role.liczba).ToList();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostUsun(int id)
        {
            var Przepis = await _baza.Przepis.FindAsync(id);
            if (Przepis == null)
            {
                return NotFound();
            }
            _baza.Przepis.Remove(Przepis);
            await _baza.SaveChangesAsync();

            return RedirectToPage("MojePrzepisy");
        }
      
    }
}