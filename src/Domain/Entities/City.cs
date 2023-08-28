namespace Domain.Entities
{
    public class City
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Currency { get; set; }
        public decimal MaximumAmountPerDay { get; set; }
        public City(string name, string currency, decimal maximumAmountPerDay)
        {
            Name = name;
            Currency = currency;
            MaximumAmountPerDay = maximumAmountPerDay;
        }
        public City()
        {
                
        }
    }
}