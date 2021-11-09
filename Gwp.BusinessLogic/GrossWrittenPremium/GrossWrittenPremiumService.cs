using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gwp.DataAccess.Repositories;

namespace Gwp.BusinessLogic.GrossWrittenPremium
{
    public class GrossWrittenPremiumService : IGrossWrittenPremiumService
    {
        private readonly IGrossWrittenPremiumRepository _repository;

        public GrossWrittenPremiumService(IGrossWrittenPremiumRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<IEnumerable<AverageByLineOfBusiness>> GetAverageAsync(IEnumerable<int> lobIds, int countryId, int yearFrom, int yearTo)
        {
            var filtered = (await _repository.GetAllAsync().ConfigureAwait(false))
                                    .Where(gwp => lobIds.Contains(gwp.LineOfBusiness.Id) &&
                                                  gwp.Country.Id == countryId &&
                                                  gwp.Year >= yearFrom &&
                                                  gwp.Year <= yearTo);
           return filtered.GroupBy(x => x.LineOfBusiness.Name).Select(x => new AverageByLineOfBusiness
            {
                LineOfBusiness = x.Key,
                Average = x.Average(avg => avg.Amount)
            });
        }
    }
}
