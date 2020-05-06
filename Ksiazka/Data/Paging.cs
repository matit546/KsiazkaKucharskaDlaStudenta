using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ksiazka.Data
{
    public class Paging
    {
        public int TotalItems { get; set; }
        public int Itemsperpage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPage => (int)Math.Ceiling((decimal)TotalItems / Itemsperpage);
        public string UrlParam { get; set; }
    }
}
