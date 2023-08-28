using Domain.Common;
using System;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface ITaxCalculatorService
    {
        Task<decimal> GetTax(IVehicle vehicle, DateTime[] dates, string cityName);
    }
}
