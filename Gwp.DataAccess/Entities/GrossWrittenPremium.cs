namespace Gwp.DataAccess.Entities
{
    public class GrossWrittenPremium
    {
        public Country Country { get; set; }
        public LineOfBusiness LineOfBusiness { get; set; }
        public decimal Amount { get; set; }
        public int Year { get; set; }
    }
}