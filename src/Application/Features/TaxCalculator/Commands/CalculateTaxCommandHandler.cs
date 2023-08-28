using Application.Common.Interfaces;
using Domain.Entities;
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
            var vehicle = new Car { Name = request.Vehicle };

           return await _taxCalculatorService.GetTax(vehicle, request.Dates, request.CityName);
        }
    }
}