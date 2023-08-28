using Application.Common.Interfaces;
using Domain.Common;
using Domain.Entities;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Infrastructure.Services
{
    public class TaxCalculatorService: ITaxCalculatorService
    {
        private readonly ICityRepository _cityRepository;
        private readonly ITaxExemptVehicleRepository _taxExemptVehicleRepository;
        private readonly IExemptDateRepository _exemptDateRepository;
        private readonly ITaxAmountRepository _taxAmountRepository;

        public TaxCalculatorService(ICityRepository cityRepository, ITaxExemptVehicleRepository taxExemptVehicleRepository, IExemptDateRepository exemptDateRepository, ITaxAmountRepository taxAmountRepository)
        {
            _cityRepository = cityRepository;
            _taxExemptVehicleRepository = taxExemptVehicleRepository;
            _exemptDateRepository = exemptDateRepository;
            _taxAmountRepository = taxAmountRepository;
        }

        /**
         * Calculate the total toll fee for one day
         *
         * @param vehicle - the vehicle
         * @param dates   - date and time of all passes on one day
         * @return - the total congestion tax for that day
        */

        public async Task<decimal> GetTax(IVehicle vehicle, DateTime[] dates, string cityName)
        {
            var (cityId, maximumAmountPerDay) = await GetCity(cityName);
            if (cityId == 0)
                return 0;

            var isExemptVehicle = await IsExemptVehicle(cityId, vehicle);
            if (isExemptVehicle)
                return 0;

            var amount = await CalculateTax(cityId, maximumAmountPerDay, dates);

            return amount;
        }

        private async Task<(int cityId, decimal maximumAmountPerDay)> GetCity(string cityName)
        {
            var city = await _cityRepository.GetCityAsync(cityName);

            if (city is null)
                return (0, 0);

            return (city.Id, city.MaximumAmountPerDay);
        }

        private async Task<bool> IsExemptVehicle(int cityId, IVehicle vehicle)
        {
            bool result = false;

            var taxExemptVehicles = await _taxExemptVehicleRepository.GetTaxExemptVehiclesAsync(cityId);

            var isExemptVehicle = taxExemptVehicles.Any(c => c.VehicleName == vehicle.GetVehicleType());
            if (isExemptVehicle)
                result = true;

            return result;
        }

        private async Task<decimal> CalculateTax(int cityId, decimal maximumAmountPerDay, DateTime[] dates)
        {
            var exemptDate = await _exemptDateRepository.GetExemptDateAsync(cityId);
            var taxAmounts = await _taxAmountRepository.GetTaxAmountsAsync(cityId);

            var groupedDates = SeprateDates(dates);

            decimal total = 0;

            foreach (var group in groupedDates)
            {
                var times = group.TimesOfDay;
                if (times is { Count: 0 })
                    continue;

                var isInFreeDate = TaxIsInFreeDate(exemptDate, group.Date);
                if (isInFreeDate)
                    continue;

                decimal amount = 0;
                for (int i = 0; i < times.Count; i++)
                {
                    if (amount >= maximumAmountPerDay)
                    {
                        amount = maximumAmountPerDay;
                        break;
                    }

                    TimeSpan time = times[i];

                    amount += taxAmounts.FirstOrDefault(c => c.StartTime <= time && c.EndTime >= time)?.Amount ?? 0;
                }

                total += amount;
            }

            return total;

        }

        private bool TaxIsInFreeDate(ExemptDate exemptDate, DateTime date)
        {
            bool isInFreeMonth = exemptDate.IsInFreeMonth(date);
            if (isInFreeMonth)
                return true;

            bool isFreeDaysOfWeek = exemptDate.IsFreeDaysOfWeek(date);
            if (isFreeDaysOfWeek)
                return true;

            bool isDayBeforePublicHoliday = exemptDate.IsDayBeforePublicHoliday(date);
            if (isDayBeforePublicHoliday)
                return true;

            return false;
        }

        private List<(DateTime Date, List<TimeSpan> TimesOfDay)> SeprateDates(DateTime[] dates)
        {
            var groupedDates = dates.GroupBy(c => c.Date)
                                     .Select(c => (
                                         Date: c.Key,
                                         TimesOfDay: c.Select(c => c.TimeOfDay).OrderBy(c => c).ToList()
                                     ))
                                     .ToList();

            return groupedDates;
        }


    }
}