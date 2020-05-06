using Ksiazka.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ksiazka.Model.widok
{
    public class listaPrzepisow
    {
        public List<Przepis> Przepisy { get; set; }
        public Paging Paging { get; set; }
    }
}
