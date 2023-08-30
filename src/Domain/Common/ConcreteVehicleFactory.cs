using System;

namespace Domain.Common
{

    public class ConcreteVehicleFactory : VehicleFactory
    {
        public override IVehicle GetVehicle(string Vehicle)
        {
            switch (Vehicle)
            {
                case "Car":
                    return new Car();
                case "Motorbike":
                    return new Motorbike();
                default:
                    throw new ApplicationException(string.Format("Vehicle '{0}' cannot be find", Vehicle));
            }
        }
    }
}
