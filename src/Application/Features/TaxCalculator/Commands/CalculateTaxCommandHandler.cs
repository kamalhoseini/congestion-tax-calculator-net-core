using Application.Common.Interfaces;
using Domain.Common;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.TaxCalculator.Commands
{
    public class CalculateTaxCommandHandler : IRequestHandler<CalculateTaxCommand, decimal>
    {
        private readonly ITaxCalculatorService _taxCalculatorService;

        public CalculateTaxCommandHandler(ITaxCalculatorService taxCalculatorService)
        {
            _taxCalculatorService = taxCalculatorService;
        }

        public async Task<decimal> Handle(CalculateTaxCommand request, CancellationToken cancellationToken)
        {
            VehicleFactory factory = new ConcreteVehicleFactory();

            IVehicle vehicle = factory.GetVehicle(request.VehicleType);

            return await _taxCalculatorService.GetTax(vehicle, request.Dates, request.CityName);
        }
    }
}