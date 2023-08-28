using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly CongestionContext _context;
        public CityRepository(CongestionContext context)
        {
            _context = context;
        }

        public async Task<City> GetCityAsync(string cityName)
        => await _context.Cities
                         .AsNoTracking()
                         .SingleOrDefaultAsync(c => c.Name.ToLower() == cityName.ToLower());
    }
}
