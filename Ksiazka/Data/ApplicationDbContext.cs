using System;
using System.Collections.Generic;
using System.Text;
using Ksiazka.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ksiazka.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Przepis> Przepis { get; set; }
        public DbSet<Uzytkownik> Uzytkownik { get; set; }
        public DbSet<Komentarze> Komentarze { get; set; }
    }
}
