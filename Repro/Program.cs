using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading;
using Microsoft.EntityFrameworkCore;

namespace Repro
{
    class Program
    {
        static void Main(string[] args)
        {
            CallDb();
            Thread.Sleep(TimeSpan.FromMinutes(10)); 
            CallDb();
        }

        private static void CallDb()
        {
            using var ctx = new TestContext();
            var result = ctx.Dummy.FromSqlRaw("select current_timestamp").First();
            Console.WriteLine(result.DateTime);
        }
    }
    
    public class TestContext : DbContext
    {
        public DbSet<Dummy> Dummy { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=TODO;Database=TODO;Username=TODO;Password=TODO;sslmode=Disable");
    }

    public class Dummy
    {
        [Key] [Column("current_timestamp")]
        public DateTime DateTime { get; set; }
    }

}