using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class TaxAmountRepository : ITaxAmountRepository
    {
        private readonly CongestionContext _context;
        public TaxAmountRepository(CongestionContext context)
        {
            _context = context;
        }

        public async Task<List<TaxAmount>> GetTaxAmountsAsync(int cityId)
        => await _context.TaxAmounts
                         .Where(c => c.CityId == cityId)
                         .AsNoTracking()
                         .ToListAsync();  
    }
}
