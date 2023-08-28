using System;

namespace Domain.Entities
{
    public class TaxAmount
    {
        public int Id { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public decimal Amount { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }

        public TaxAmount(TimeSpan startTime, TimeSpan endTime, decimal amount, int cityId)
        {
            StartTime = startTime;
            EndTime = endTime;
            Amount = amount;
            CityId = cityId;
        }
    }
}
