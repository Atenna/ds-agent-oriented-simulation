using System.Collections.Generic;
using ds_agent_oriented_simulation.Entities.Vehicles;

namespace ds_agent_oriented_simulation.Entities
{
    public class OneLane
    {
        public LinkedList<Vehicle> cars { get; private set; }
        public double time { get; private set; }

        public OneLane(Vehicle firstCar, double time)
        {
            cars = new LinkedList<Vehicle>();
            cars.AddLast(firstCar);
            this.time = time;
        }

        public void AddCar(Vehicle newCar)
        {
            cars.AddLast(newCar);
        }
    }
}
