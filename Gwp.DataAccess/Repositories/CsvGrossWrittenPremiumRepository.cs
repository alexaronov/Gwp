using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using Gwp.DataAccess.Entities;
using Gwp.DataAccess.Raw;

namespace Gwp.DataAccess.Repositories
{
    public class CsvGrossWrittenPremiumRepository : IGrossWrittenPremiumRepository
    {
        private static readonly IEnumerable<GrossWrittenPremium> _enitites;

        static CsvGrossWrittenPremiumRepository()
        {
            var directory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            using var gwpStreamReader = new StreamReader(File.Open(Path.Combine(directory, "gwp.csv"), FileMode.Open, FileAccess.Read));
            using var gwpCsvReader = new CsvReader(gwpStreamReader, CultureInfo.InvariantCulture);
            var gwps = gwpCsvReader.GetRecords<GwpRaw>().ToList();

            using var lobStreamReader = new StreamReader(File.Open(Path.Combine(directory, "lob.csv"), FileMode.Open, FileAccess.Read));
            using var lobCsvReader = new CsvReader(lobStreamReader, CultureInfo.InvariantCulture);
            var lobs = lobCsvReader.GetRecords<LobRaw>().ToDictionary(x=>x.Id, key => key);

            using var countryStreamReader = new StreamReader(File.Open(Path.Combine(directory, "countries.csv"), FileMode.Open, FileAccess.Read));
            using var countryCsvReader = new CsvReader(countryStreamReader, CultureInfo.InvariantCulture);
            var countries = countryCsvReader.GetRecords<CountryRaw>().ToDictionary(x => x.Id, key => key);

            _enitites = gwps.Select(s => new GrossWrittenPremium()
            {
                Amount = s.Amount,
                Year = s.Year,
                LineOfBusiness = new LineOfBusiness() {Id = s.LOB, Name = lobs[s.LOB].Name},
                Country = new Country() { Id = s.Country, Name = countries[s.Country].Name}
            }).ToArray();
        }

        public async Task<IEnumerable<GrossWrittenPremium>> GetAllAsync()
        {
            return _enitites;
        }
    }
}