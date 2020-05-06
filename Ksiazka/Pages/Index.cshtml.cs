using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ksiazka.Data;
using Ksiazka.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ksiazka.Pages
{
    public class IndexModel : PageModel
    {
        private ApplicationDbContext _baza;
        public IndexModel(ApplicationDbContext baza)
        {
            _baza = baza;
        }
        public Uzytkownik Uzytkownik { get; set; }

        public void OnGet()
        {
            if (User.Identity.Name == null)
            {
                statyczna.Zdjecie = "default.jpg";
            }
            else
            {
                Uzytkownik = _baza.Uzytkownik.Where(u => u.UserName.ToLower().Contains(User.Identity.Name.ToLower())).First();
                statyczna.Zdjecie = Uzytkownik.Awatar;
            }
        }
    }
}
