using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ksiazka.Model
{
    public class Komentarze
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Puste pole!")]
        public string Komentarz { get; set; }
        public int Id_Przepisu { get; set; }
        public string Autor { get; set; }
        public string Data { get; set; }
        public string Awatar { get; set; }
    }
}
