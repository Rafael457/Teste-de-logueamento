using bibliotecalogteste.Models;
using Microsoft.EntityFrameworkCore;

namespace bibliotecalogteste.Context
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Pessoa> Pessoas { get; set; }
    }
}
