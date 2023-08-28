using Domain.Common;

namespace Domain.Entities
{
    public class Car : IVehicle
    {
        public string Name { get; set; } = "Car";
        public string GetVehicleType()
        {
            return Name;
        }
    }
}