using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface ITaxAmountRepository
    {
        Task<List<TaxAmount>> GetTaxAmountsAsync(int cityId);
    }
}
