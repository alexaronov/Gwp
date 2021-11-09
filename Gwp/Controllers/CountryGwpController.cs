using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gwp.BusinessLogic.GrossWrittenPremium;
using Gwp.Models.CountryGwp;
using Gwp.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Gwp.Controllers
{
    [ApiController]
    [Route("server/api/gwp")]
    public class CountryGwpController : ControllerBase
    {
        private readonly IMemoryCache _cache;
        private readonly IGrossWrittenPremiumService _grossWrittenPremiumService;

        public CountryGwpController(IMemoryCache cache, IGrossWrittenPremiumService grossWrittenPremiumService)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _grossWrittenPremiumService = grossWrittenPremiumService ?? throw new ArgumentNullException(nameof(grossWrittenPremiumService));
        }

        /// <summary>
        /// Returns an average gross written premium (GWP) over 2008-2015 period for the selected lines of business
        /// </summary>
        /// <returns></returns>
        [HttpPost("avg")]
        public async Task<Dictionary<string,decimal>> Average(AverageGwpRequestModel model)
        {
            if (_cache.TryGetValue(model.GetStringKey(), out var value))
            {
                if (value != null)
                {
                    return (Dictionary<string, decimal>) value;
                }
            }

            var averages = await _grossWrittenPremiumService.GetAverageAsync(model.LinesOfBusiness, model.Country, 2008, 2015);
            var result = averages.ToDictionary(key => key.LineOfBusiness.ToCamelCase(), value => value.Average);
            _cache.Set(model.GetStringKey(), result, DateTimeOffset.Now.AddMinutes(5));

            return result;
        }
    }
}