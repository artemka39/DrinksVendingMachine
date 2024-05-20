using DrinksVendingMachine.Common.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinksVendingMachine.Backend
{
    public class DrinksVendingMachineDbContext : DbContext
    {
        public DbSet<Coin> Coins { get; set; }
        public DbSet<Drink> Drinks { get; set; }
        public DrinksVendingMachineDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var oneRuble =
                new Coin
                {
                    Id = 1,
                    Denomination = 1,
                    Acceptance = true,
                    Count = 0
                };
            var twoRubles =
                new Coin
                {
                    Id = 2,
                    Denomination = 2,
                    Acceptance = true,
                    Count = 0
                };
            var fiveRubles =
                new Coin
                {
                    Id = 3,
                    Denomination = 5,
                    Acceptance = true,
                    Count = 0
                };
            var tenRubles =
                new Coin
                {
                    Id = 4,
                    Denomination = 10,
                    Acceptance = true,
                    Count = 0
                };
            modelBuilder.Entity<Coin>().HasData(
                oneRuble,
                twoRubles,
                fiveRubles,
                tenRubles
                );
        }
    }
}
