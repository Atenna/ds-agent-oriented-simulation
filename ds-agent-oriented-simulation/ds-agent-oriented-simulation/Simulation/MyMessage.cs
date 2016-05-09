using ds_agent_oriented_simulation.Entities.Vehicles;
using OSPABA;

namespace ds_agent_oriented_simulation.Simulation
{
    public class MyMessage : MessageForm
    {

        public Vehicle Car { get; set; }
        public int Variant { get; set; }

        public string Name { get; set; }

        public MyMessage(OSPABA.Simulation sim) :
            base(sim)
        {
        }

        public MyMessage(MyMessage original) :
            base(original)
        {
            // copy() is called in superclass
        }

        public MyMessage(OSPABA.Simulation sim, Vehicle car) :
            base(sim)
        {
            this.Car = car;
        }

        public MyMessage(OSPABA.Simulation sim, int variant) :
            base(sim)
        {
            this.Variant = variant;
        }

        public MyMessage(OSPABA.Simulation sim, string name) :
            base(sim)
        {
            this.Name = name;
        }

        override public MessageForm CreateCopy()
        {
            return new MyMessage(this);
        }

        override protected void Copy(MessageForm message)
        {
            base.Copy(message);
            MyMessage original = (MyMessage)message;
            // Copy attributes
        }
    }
}
