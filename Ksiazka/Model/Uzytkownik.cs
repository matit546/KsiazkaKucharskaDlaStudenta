using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ksiazka.Model
{
    public class Uzytkownik: IdentityUser
    {
        public string Nazwa_uzytkownika { get; set; }
        public string Data_zalozenia { get; set; }
        public string Awatar { get; set; }
    }
}
