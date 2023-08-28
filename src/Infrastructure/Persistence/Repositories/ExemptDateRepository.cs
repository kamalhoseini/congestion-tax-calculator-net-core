using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class ExemptDateRepository: IExemptDateRepository
    {
        private readonly CongestionContext _context;
        public ExemptDateRepository(CongestionContext context)
        {
            _context = context;
        }

        public async Task<ExemptDate> GetExemptDateAsync(int cityId)
         => await _context.ExemptDates
                          .SingleOrDefaultAsync(c => c.CityId == cityId);

    }
}
