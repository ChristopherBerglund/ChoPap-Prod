using ChoPap.Features;
using ChoPap.Features.Helper;
using ChoPap.Features.StockHandler;
using ChoPap.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ChoPap.Data
{
    public class Program
    {
        public class ChopapContext : DbContext
        {
            public DbSet<Account> Accounts { get; set; }
            public DbSet<Stock> Stocks { get; set; }
            public DbSet<BoughtStocks> BoughtStocks { get; set; }
            public DbSet<Temp> Temps { get; set; }
            //public DbSet<lockedStock> LockedStocks { get; set; }
            public DbSet<Updated> updates { get; set; }
            public DbSet<SaldoTable> SaldoTables { get; set; }
            //public DbSet<MonthlySaldo> MonthlySaldos { get; set; }
            public DbSet<SoldStocks> SoldStocks { get; set; }
            public DbSet<CountryConfig> CountryConfig { get; set; }


            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                //DEV-MODE
                optionsBuilder.UseSqlServer(Global.sqlConnectionStringTest);
                //PRODUCTION
                //optionsBuilder.UseSqlServer(Global.sqlConntectionStringProduction);
            }
        }







    }
}
