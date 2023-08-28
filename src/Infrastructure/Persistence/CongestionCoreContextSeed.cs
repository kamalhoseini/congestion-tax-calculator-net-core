using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Persistence
{

    public static class CongestionCoreContextSeed
    {
        public static async Task SeedDatabase(this IServiceProvider service)
        {
            using (var scope = service.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<CongestionContext>();

                var cityId = await SeedCityAsync(context);
                await SeedTaxExemptVehicle(context, cityId);
                await SeedExemptDates(context, cityId);
                await SeedTaxAmount(context, cityId);


            }
        }
        static async Task<int> SeedCityAsync(CongestionContext context)
        {
            City city = new City("Gothenburg", "Seek", 60);
           
            context.Cities.Add(city);
            await context.SaveChangesAsync();

            return city.Id;
        }

        static async Task SeedTaxExemptVehicle(CongestionContext context, int cityId)
        {
            var taxExemptVehicles = new List<TaxExemptVehicle>
            {
                new TaxExemptVehicle("Emergency vehicles", cityId),
                new TaxExemptVehicle("Busses", cityId),
                new TaxExemptVehicle("Diplomat vehicles", cityId),
                new TaxExemptVehicle("Motorcycles", cityId),
                new TaxExemptVehicle("Military vehicles", cityId),
                new TaxExemptVehicle("Foreign vehicles", cityId),
            };

            context.AddRange(taxExemptVehicles);
            await context.SaveChangesAsync();
        }

        static async Task SeedExemptDates(CongestionContext context, int cityId)
        {
            var publicHolidays = new List<DateTime> {
               new DateTime(2013, 4, 27),
               new DateTime(2013, 4, 28),
               new DateTime(2013, 4, 30),
               new DateTime(2013, 5, 1),
               new DateTime(2013, 5, 8),
               new DateTime(2013, 5, 9),
               new DateTime(2013, 6, 19),
               new DateTime(2013, 6, 20),
               new DateTime(2013, 6, 21),
               new DateTime(2013, 12, 22),
               new DateTime(2013, 12, 23),
               new DateTime(2013, 12, 24),
               new DateTime(2013, 12, 25),
               new DateTime(2013, 12, 26),
               new DateTime(2013, 12, 31)
            };

            var freeMonth = new List<int> { 7 };
            var freeDaysOfWeek = new List<DayOfWeek> { DayOfWeek.Saturday, DayOfWeek.Sunday };

            ExemptDate exemptDate = new ExemptDate(cityId, publicHolidays, freeMonth, freeDaysOfWeek);

            context.Add(exemptDate);
            await context.SaveChangesAsync();
        }

        static async Task SeedTaxAmount(CongestionContext context, int cityId)
        {
            var taxAmounts = new List<TaxAmount>
            {
               new TaxAmount (new TimeSpan(6, 0, 0), new TimeSpan(6, 29, 0), 8,cityId ),
               new TaxAmount (new TimeSpan(6, 30, 0), new TimeSpan(6, 59, 0), 13 ,cityId),
               new TaxAmount (new TimeSpan(7, 0, 0), new TimeSpan(7, 59, 0), 18, cityId ),
               new TaxAmount (new TimeSpan(8, 0, 0), new TimeSpan(8, 29, 0), 13, cityId ),
               new TaxAmount (new TimeSpan(8, 30, 0), new TimeSpan(14, 59, 0), 8, cityId ),
               new TaxAmount (new TimeSpan(15, 0, 0), new TimeSpan(15, 29, 0), 13, cityId ),
               new TaxAmount (new TimeSpan(15, 30, 0), new TimeSpan(16, 59, 0), 18, cityId ),
               new TaxAmount (new TimeSpan(17, 00, 0), new TimeSpan(17, 59, 0), 13, cityId ),
               new TaxAmount (new TimeSpan(18, 00, 0), new TimeSpan(18, 29, 0), 8, cityId ),
               new TaxAmount (new TimeSpan(18, 30, 0), new TimeSpan(05, 59, 0), 0, cityId )
            };

            context.AddRange(taxAmounts);
            await context.SaveChangesAsync();
        }

    }
}