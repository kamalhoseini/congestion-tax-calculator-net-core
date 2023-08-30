using Domain.Common;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Persistence;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Infrastructure.UnitTests
{
    public class TaxCalculatorServiceTest
    {
        private readonly Mock<ICityRepository> _cityRepository;
        private readonly Mock<ITaxExemptVehicleRepository> _taxExemptVehicleRepository;
        private readonly Mock<IExemptDateRepository> _exemptDateRepository;
        private readonly Mock<ITaxAmountRepository> _taxAmountRepository;
        public TaxCalculatorServiceTest()
        {
            _cityRepository = new Mock<ICityRepository>();
            _taxExemptVehicleRepository = new Mock<ITaxExemptVehicleRepository>();
            _exemptDateRepository = new Mock<IExemptDateRepository>();
            _taxAmountRepository = new Mock<ITaxAmountRepository>();
        }

        [Theory]
        [MemberData(nameof(FakeData))]
        public async Task CalculateDiscount_ShouldbeReturnValidResult((string vehicleType, DateTime[] dates, string cityName, decimal validResult) data)
        {
            // Arrange

            VehicleFactory factory = new ConcreteVehicleFactory();

            IVehicle vehicle = factory.GetVehicle(data.vehicleType);

            var cityId = FakeCity().Id;

            _cityRepository.Setup(c => c.GetCityAsync(It.IsAny<string>()))
                           .ReturnsAsync(FakeCity());

            _taxExemptVehicleRepository.Setup(c => c.GetTaxExemptVehiclesAsync(cityId))
                                  .ReturnsAsync(FakeTaxExemptVehicle(cityId));

            _exemptDateRepository.Setup(c => c.GetExemptDateAsync(cityId))
                                  .ReturnsAsync(FakeExemptDate(cityId));

            _taxAmountRepository.Setup(c => c.GetTaxAmountsAsync(cityId))
                                  .ReturnsAsync(FakeTaxAmount(cityId));

            var service = new TaxCalculatorService(_cityRepository.Object,
                                                   _taxExemptVehicleRepository.Object,
                                                   _exemptDateRepository.Object,
                                                   _taxAmountRepository.Object);

            // Act
            var result = await service.GetTax(vehicle, data.dates, data.cityName);

            // Assert
            Assert.Equal(data.validResult, result);
        }


        public City FakeCity()
        {
            return new City()
            {
                Id = 1,
                Name = "Gothenburg",
                Currency = "Seek",
                MaximumAmountPerDay = 60
            };
        }

        public List<TaxExemptVehicle> FakeTaxExemptVehicle(int cityId)
        {
            return new List<TaxExemptVehicle>
            {
                new TaxExemptVehicle("Emergency vehicles", cityId),
                new TaxExemptVehicle("Busses", cityId),
                new TaxExemptVehicle("Diplomat vehicles", cityId),
                new TaxExemptVehicle("Motorcycles", cityId),
                new TaxExemptVehicle("Military vehicles", cityId),
                new TaxExemptVehicle("Foreign vehicles", cityId),
            };
        }

        public ExemptDate FakeExemptDate(int cityId)
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

            return new ExemptDate(cityId, publicHolidays, freeMonth, freeDaysOfWeek);
        }

        public List<TaxAmount> FakeTaxAmount(int cityId)
        {
            return new List<TaxAmount>
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
        }

        public static IEnumerable<object[]> FakeData()
        {
            return new List<object[]>
                {
                         new object[] {
                             (
                                 "Car",
                                 new DateTime[] {
                                      new DateTime(2013, 01, 14, 21, 00, 00),
                                      new DateTime(2013, 01, 15, 21, 00, 00),
                                      new DateTime(2013, 02, 07, 06, 23, 27),
                                      new DateTime(2013, 02, 07, 15, 27, 00),
                                      new DateTime(2013, 02, 08, 06, 27, 00),
                                      new DateTime(2013, 02, 08, 06, 20, 27),
                                      new DateTime(2013, 02, 08, 14, 35, 00),
                                      new DateTime(2013, 02, 08, 15, 29, 00),
                                      new DateTime(2013, 02, 08, 15, 47, 00),
                                      new DateTime(2013, 02, 08, 16, 01, 00),
                                      new DateTime(2013, 02, 08, 16, 48, 00),
                                      new DateTime(2013, 02, 08, 17, 49, 00),
                                      new DateTime(2013, 02, 08, 18, 29, 00),
                                      new DateTime(2013, 02, 08, 18, 35, 00),
                                      new DateTime(2013, 03, 26, 14, 25, 00),
                                      new DateTime(2013, 03, 28, 14, 07, 27)
                                 },
                                 "Gothenburg",
                                 (decimal)97
                             )
                         }
                };
        }
    }
}
