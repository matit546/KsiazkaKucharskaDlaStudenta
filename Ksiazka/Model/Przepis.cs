using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ksiazka.Model
{
    public class Przepis
    {

        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Nazwa Wymagana!")]
        [Display(Name = "Nazwa Przepisu:")]
        public string Nazwa { get; set; }
        [Required(ErrorMessage = "Opis Wymagany!")]
        [Display(Name = "Opis:")]
        public string Opis { get; set; }
        [Required(ErrorMessage = "Skladniki Wymagane!")]
        [Display(Name = "Składniki:")]
        public string Skladniki { get; set; }
        [Display(Name = "Czas w minutach:")]
        [Required(ErrorMessage = "Czas wymagany!")]
        public int Czas { get; set; }
        [Display(Name = "Trudność:")]
        public string Trudnosc { get; set; }
        [Display(Name = "Kategoria:")]
        public string Kategoria_1 { get; set; }
        [Display(Name = "Kategoria2:")]
        public string Kategoria_2 { get; set; }
        [Display(Name = "Kategoria3:")]
        public string Kategoria_3 { get; set; }
        [Display(Name = "Zdjecie:")]
        public string Zdjecie { get; set; }
        [Display(Name = "Autor:")]
        public string Autor { get; set; }
        [Display(Name = "Data:")]
        public string Data_Dodania { get; set; }

    }
}
