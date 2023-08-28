using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class TaxExemptVehicleRepository: ITaxExemptVehicleRepository
    {
        private readonly CongestionContext _context;
        public TaxExemptVehicleRepository(CongestionContext context)
        {
            _context = context;
        }

        public async Task<List<TaxExemptVehicle>> GetTaxExemptVehiclesAsync(int cityId)
         => await _context.TaxExemptVehicles
                            .Where(c => c.CityId == cityId)
                            .AsNoTracking()
                            .ToListAsync();
    }
}
