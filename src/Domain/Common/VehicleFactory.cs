namespace Domain.Common
{
    public abstract class VehicleFactory
    {
        public abstract IVehicle GetVehicle(string vehicle);

    }
}
