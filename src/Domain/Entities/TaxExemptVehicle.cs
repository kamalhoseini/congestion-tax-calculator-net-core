using Domain.Common;

namespace Domain.Entities
{
    public class TaxExemptVehicle : IVehicle
    {
        public int Id { get; set; }
        public string VehicleName { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }

        public TaxExemptVehicle(string vehicleName, int cityId)
        {
            VehicleName = vehicleName;
            CityId = cityId;
        }

        public string GetVehicleType()
        {
            return VehicleName;
        }
    }
}
