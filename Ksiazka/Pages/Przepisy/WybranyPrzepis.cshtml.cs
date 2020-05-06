using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ksiazka.Data;
using Ksiazka.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Ksiazka.Pages.Przepisy
{
    public class WybranyPrzepisModel : PageModel
    {
        private ApplicationDbContext _baza;

        public WybranyPrzepisModel(ApplicationDbContext baza)
        {
            _baza = baza;
        }
        [BindProperty]
        public Uzytkownik Uzytkownik { get; set; }
        public string schowaj { get; set; }
        [BindProperty]
        public Komentarze DodajKomentarz { get; set; }
        public List<Komentarze> Komentarz { get; set; }
        [BindProperty]
        public Przepis Przepis { get; set; }
        public async Task OnGetAsync(int id)
        {
         
            if(id==0)
            {
                id = numer.numerprzepis;
            }

            Przepis = _baza.Przepis.Find(id);
            Komentarz = await _baza.Komentarze.Where(x => x.Id_Przepisu.ToString().Contains(id.ToString())).ToListAsync();
            numer.numerprzepis = id;
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

            return RedirectToPage("Przepisy");
        }
        public async Task<IActionResult> OnPostKomentarz()
        {
            if (User.Identity.Name == null)
            {
                DodajKomentarz.Autor = "Gość";
                DodajKomentarz.Awatar = "default.jpg";
            }
            else
            {

                Uzytkownik = _baza.Uzytkownik.Where(u => u.UserName.ToLower().Contains(User.Identity.Name.ToLower())).First();
                DodajKomentarz.Awatar = Uzytkownik.Awatar;
                DodajKomentarz.Autor = User.Identity.Name;

            }
            DodajKomentarz.Id_Przepisu = numer.numerprzepis;
            DodajKomentarz.Data= DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString();
            _baza.Komentarze.Add(DodajKomentarz);
            await _baza.SaveChangesAsync();
            return RedirectToPage("WybranyPrzepis");
        }

        public async Task<IActionResult> OnPostUsunKomentarz(int id)
        {
            var komentarz = await _baza.Komentarze.FindAsync(id);
            if (komentarz == null)
            {
                return NotFound();
            }
            _baza.Komentarze.Remove(komentarz);
            await _baza.SaveChangesAsync();

            return RedirectToPage("WybranyPrzepis");
        }

        public async Task<IActionResult> OnPostEdytujKomentarz(int id)
        {
            var komentarz = await _baza.Komentarze.FindAsync(id);
            if (komentarz == null)
            {
                return NotFound();
            }
            komentarz.Komentarz = DodajKomentarz.Komentarz;
            komentarz.Data = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString();
            await _baza.SaveChangesAsync();
            return RedirectToPage("WybranyPrzepis");
        }
    }
}