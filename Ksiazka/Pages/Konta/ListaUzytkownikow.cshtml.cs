using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ksiazka.Data;
using Ksiazka.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Ksiazka.Pages.Konta
{
    public class ListaUzytkownikowModel : PageModel
    {

        private readonly ApplicationDbContext _baza;
        public ListaUzytkownikowModel(ApplicationDbContext baza)
        {
            _baza = baza;
        }

        [BindProperty]
        public List<Uzytkownik> Uzytkownicy { get; set; }
        public async Task<IActionResult> OnGet()
        {
            Uzytkownicy = await _baza.Uzytkownik.ToListAsync();
            return Page();
        }
    }
}