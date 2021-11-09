using System.Collections.Generic;
using System.Threading.Tasks;
using Gwp.DataAccess.Entities;

namespace Gwp.DataAccess.Repositories
{
    public interface IGrossWrittenPremiumRepository
    {
        public Task<IEnumerable<GrossWrittenPremium>> GetAllAsync();
    }
}