using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ksiazka.Data;
using Ksiazka.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Ksiazka.Pages.Przepisy
{
    public class PrzepisyModel : PageModel
    {
        private readonly ApplicationDbContext _baza;
        public static bool sprawdz { get; set; }
        public bool disbale2 { get; set; }

        public PrzepisyModel(ApplicationDbContext baza)
        {
            _baza = baza;
        }
        public List<Przepis> Przepisy { get; set; }

        public async Task OnGet()
        {

            if (sprawdz == true)
            {
                Przepisy = await _baza.Przepis.ToListAsync();
                int zmienna = 0;
                zmienna = Przepisy.Count;
                if (numer.liczba + 6 >= zmienna)
                {
                    Przepisy = await _baza.Przepis.ToListAsync();
                    numer.liczba = 0;
                    disbale2 = true;
                    sprawdz = false;
                }
                else
                {
                    Przepisy = Przepisy.Skip(numer.liczba - 6).Take(numer.liczba + 6).ToList<Przepis>();
                    sprawdz = false;
                }
            }
            else
            {
                numer.liczba = 0;
                Przepisy = await _baza.Przepis.ToListAsync();
                Przepisy = Przepisy.Skip(numer.liczba - 6).Take(numer.liczba + 6).ToList<Przepis>();
                disbale2 = false;
            }
        }

        public IActionResult OnPost()
        {
            numer.liczba = numer.liczba + 6;
            sprawdz = true;
            return RedirectToPage("Przepisy");
         
        }
    }
}