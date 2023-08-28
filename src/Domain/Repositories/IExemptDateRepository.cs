using Domain.Entities;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IExemptDateRepository
    {
        Task<ExemptDate> GetExemptDateAsync(int cityId);
    }
}
