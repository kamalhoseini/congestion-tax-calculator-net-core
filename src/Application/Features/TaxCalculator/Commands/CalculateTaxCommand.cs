using MediatR;
using System;

namespace Application.Features.TaxCalculator.Commands
{
    public class CalculateTaxCommand : IRequest<decimal> 
    {
        public CalculateTaxCommand()
        {
                
        }
        public string VehicleType { get; set; }
        public DateTime[] Dates { get; set; }
        public string CityName { get; set; }


    } 
}