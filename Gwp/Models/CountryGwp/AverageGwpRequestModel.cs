using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Gwp.Models.CountryGwp
{
    public class AverageGwpRequestModel
    {
        /// <summary>
        /// Identifier of country (try 1/2/3)
        /// </summary>
        [Required(ErrorMessage = "Specify country")]
        public int Country { get; set; }

        /// <summary>
        /// Identifiers of lines of business (try 1/2/3)
        /// </summary>
        [Required(ErrorMessage = "Specify lines of business")]
        [MinLength(1, ErrorMessage = "Specify at least 1 line of business")]
        public IEnumerable<int> LinesOfBusiness { get; set; }

        public string GetStringKey()
        {
            return $"{Country}[{LinesOfBusinessStr}]";
        }

        private string LinesOfBusinessStr => LinesOfBusiness != null ? string.Join(',' ,LinesOfBusiness.OrderBy(o => o)) : "";
    }
}