using System.Collections.Generic;
using ds_agent_oriented_simulation.Entities.Vehicles;

namespace ds_agent_oriented_simulation.Entities
{
    public class OneLaneRoad
    {
        private LinkedList<OneLane> kolonka;
        public double length { get; private set; }
        public OneLaneRoad(int dlzka)
        {
            this.kolonka = new LinkedList<OneLane>();
            this.length = dlzka;
        }

        public double AddCar(Vehicle car, double simTime)
        {
            double casCesty = (length/(car.Speed/60.0));
            double casPrichodu = casCesty + simTime;
            if (kolonka.Count == 0)
            {
                OneLane newLane = new OneLane(car, casPrichodu);
                kolonka.AddLast(newLane);
                return casCesty;
            }
            else
            {   
                if (casPrichodu <= kolonka.Last.Value.time)
                {
                    kolonka.Last.Value.AddCar(car);
                    return -1;
                }
                else
                {
                    OneLane newLane = new OneLane(car, casPrichodu);
                    kolonka.AddLast(newLane);
                    return casCesty;
                }
            }
            
        }

        public OneLane GetFirstLane()
        {
            OneLane toRet = kolonka.First.Value;
            kolonka.RemoveFirst();
            return toRet;
        }
    }
}
