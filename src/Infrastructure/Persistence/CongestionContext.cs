using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Collections.Generic;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace Infrastructure.Persistence
{
    public class CongestionContext : DbContext
    {
        public CongestionContext(DbContextOptions<CongestionContext> options) : base(options)
        {
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<ExemptDate> ExemptDates { get; set; }
        public DbSet<TaxAmount> TaxAmounts { get; set; }
        public DbSet<TaxExemptVehicle> TaxExemptVehicles { get; set; }


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            var dateTimeListConverter = new ValueConverter<List<DateTime>, string>(v => string.Join(";", v),
                                        v => v.Split(";", StringSplitOptions.RemoveEmptyEntries) 
                                        .Select(s => DateTime.Parse(s)).ToList());
            
            var intListConverter = new ValueConverter<List<int>, string>(v => string.Join(";", v),
                                        v => v.Split(";", StringSplitOptions.RemoveEmptyEntries)
                                        .Select(s => int.Parse(s)).ToList());

            var dayOfWeekListConverter = new ValueConverter<List<DayOfWeek>, string>(v => string.Join(";", v),
                                             v => v.Split(";", StringSplitOptions.RemoveEmptyEntries) 
                                             .Select(s => Enum.Parse<DayOfWeek>(s)).ToList());

            modelBuilder.Entity<ExemptDate>()
                        .Property(e => e.PublicHolidays)
                        .HasConversion(dateTimeListConverter);
            
            modelBuilder.Entity<ExemptDate>()
                        .Property(e => e.FreeDaysOfWeek)
                        .HasConversion(dayOfWeekListConverter);
            
            modelBuilder.Entity<ExemptDate>()
                        .Property(e => e.FreeMonths)
                        .HasConversion(intListConverter);
        }
    }
}