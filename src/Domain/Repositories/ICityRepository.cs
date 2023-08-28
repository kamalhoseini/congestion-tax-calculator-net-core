using Domain.Entities;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface ICityRepository
    {
        Task<City> GetCityAsync(string cityName);
    }
}
