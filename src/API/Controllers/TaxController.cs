using Application.Features.TaxCalculator.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/Tax")]
    public class TaxController : Controller
    {

        private readonly ISender _mediator;
        public TaxController(ISender mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("CalculateTax")]
        public async Task<ActionResult<decimal>> CalculateTax([FromBody] CalculateTaxCommand command)
            => await _mediator.Send(command);
    }
}