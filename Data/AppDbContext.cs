using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTextParser.Data
{
    public class AppDbContext : DbContext
    {
        //On my local pc was only postgres
        public static bool UsedPostgres = false;

        public AppDbContext() : base()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            if(UsedPostgres)
                optionsBuilder.UseNpgsql("User ID = postgres;Password=airframetan;Host=localhost;Port=5432;" +
                    "Database=test-text-parser;Integrated Security=true;Pooling=true; Include Error Detail=True");
            else
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=applicationdb;Trusted_Connection=True;");
        }

        public DbSet<Word> Words { get; set; }
    }
}
