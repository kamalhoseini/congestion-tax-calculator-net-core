using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface ITaxExemptVehicleRepository
    {
        Task<List<TaxExemptVehicle>> GetTaxExemptVehiclesAsync(int cityId);
    }
}
