using ApiProject.Lesson.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace ApiProject.Lesson.Persistence.Configuration
{
    
    public class DatabaseCxt : DbContext
    {
        public readonly string _connectionString;

        public DatabaseCxt(DbContextOptions<DatabaseCxt> opts, IOptions<AppSettings> setting) : base(opts)
        {
            _connectionString = setting.Value.ConnectionString;
        }
        public DatabaseCxt()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        public DbSet<Corso> Corso { get; set; }
        public DbSet<Studente> Studente { get; set; }
    }
    public class Studente
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime DatePublished { get; set; }
        public virtual Corso? Corso { get; set; }
    }
    public class Corso
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime DatePublished { get; set; }
        public virtual List<Studente>? Students { get; set; }
    }
}
