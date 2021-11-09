using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gwp.BusinessLogic.GrossWrittenPremium
{
    public interface IGrossWrittenPremiumService
    {
        public Task<IEnumerable<AverageByLineOfBusiness>> GetAverageAsync(IEnumerable<int> lobIds, int countryId, int yearFrom, int yearTo);
    }
}