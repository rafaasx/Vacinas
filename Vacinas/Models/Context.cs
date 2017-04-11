namespace Vacinas.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class Context : DbContext
    {
        public Context() : base("Vacinas") { }
        public DbSet<Regiao> Regiao { get; set; }
        public DbSet<Estoque> Estoque { get; set; }
        public DbSet<Pacote> Pacote { get; set; }
    }}
