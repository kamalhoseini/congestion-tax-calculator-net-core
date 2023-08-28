using Domain.Common;

namespace Domain.Entities
{
    public class Motorbike : IVehicle
    {

        public string Name { get; set; } = "Motorbike";
        public string GetVehicleType()
        {
            return Name;
        }
      
    }
}