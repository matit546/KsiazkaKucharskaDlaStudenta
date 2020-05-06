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
    public class SzukaniePrzepisuModel : PageModel
    {

        private readonly ApplicationDbContext _baza;
        public SzukaniePrzepisuModel(ApplicationDbContext baza)
        {
            _baza = baza;
        }

        public List<Przepis> Przepisy { get; set; }
        public List<Przepis> Kategorie1 { get; set; }
        public List<Przepis> Kategorie2 { get; set; }
        public List<Przepis> Kategorie3 { get; set; }

        public async Task OnGet(string Wyszukiwarka = null)
        {
            if(Wyszukiwarka==null)
            {
                Wyszukiwarka = "tegonapewnoniema";
            }

            StringBuilder param = new StringBuilder();
            if (Wyszukiwarka != null)
            {
                param.Append(Wyszukiwarka);
            }
                Przepisy = await _baza.Przepis.Where(u => u.Nazwa.ToLower().Contains(Wyszukiwarka.ToLower())).ToListAsync();
                Kategorie1 = await _baza.Przepis.Where(u => u.Kategoria_1.ToLower().Contains(Wyszukiwarka.ToLower())).ToListAsync();
                Kategorie2 = await _baza.Przepis.Where(u => u.Kategoria_2.ToLower().Contains(Wyszukiwarka.ToLower())).ToListAsync();
                Kategorie3 = await _baza.Przepis.Where(u => u.Kategoria_3.ToLower().Contains(Wyszukiwarka.ToLower())).ToListAsync();

                Przepisy.AddRange(Kategorie1);
                Przepisy.AddRange(Kategorie2);
                Przepisy.AddRange(Kategorie3);
                Przepisy = Przepisy.GroupBy(x => x.Id).Select(x => x.First()).ToList();
               
            
        }


    }
}