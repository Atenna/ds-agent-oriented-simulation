﻿using System.Collections.Generic;
using ds_agent_oriented_simulation.Agents;
using ds_agent_oriented_simulation.Entities;
using ds_agent_oriented_simulation.Entities.Vehicles;
using ds_agent_oriented_simulation.Settings;
using ds_agent_oriented_simulation.Simulation;
using OSPABA;

namespace ds_agent_oriented_simulation.ContinualAssistant
{
    //meta! id="107"
    public class ProcessPresunNaStavbu : Process
    {
        private OneLaneRoad cesta;
        public LinkedList<Vehicle> CarsOnWay;

        public ProcessPresunNaStavbu(int id, OSPABA.Simulation mySim, CommonAgent myAgent) :
            base(id, mySim, myAgent)
        {
            cesta = new OneLaneRoad(Constants.AbLength);
            CarsOnWay = new LinkedList<Vehicle>();
        }

        override public void PrepareReplication()
        {
            base.PrepareReplication();
            // Setup component for the next replication
            cesta = new OneLaneRoad(Constants.AbLength);
            CarsOnWay = new LinkedList<Vehicle>();
        }

        //meta! sender="AgentDopravy", id="108", type="Start"
        public void ProcessStart(MessageForm message)
        {
            Vehicle naVylozenie = ((MyMessage)message).Car;
            naVylozenie.Activity = "Going";
            naVylozenie.Position = "From Depo do Building";
            CarsOnWay.AddLast(naVylozenie);

            double holdTime = cesta.AddCar(naVylozenie, MySim.CurrentTime);
            if (holdTime != -1)
            {
                message.Code = Mc.PrejazdUkonceny;
                Hold(holdTime, message);
            }
        }

        //meta! userInfo="Process messages defined in code", id="0"
        public void ProcessDefault(MessageForm message)
        {
            switch (message.Code)
            {
                case Mc.PrejazdUkonceny:
                    ProcessPrejazdUkonceny(message);
                    break;
            }
        }

        private void ProcessPrejazdUkonceny(MessageForm message)
        {
            ((MyMessage)message).cars = cesta.GetFirstLane().cars;
            CarsOnWay.RemoveFirst();
            AssistantFinished(message);
        }

        //meta! userInfo="Generated code: do not modify", tag="begin"
        override public void ProcessMessage(MessageForm message)
        {
            switch (message.Code)
            {
                case Mc.Start:
                    ProcessStart(message);
                    break;

                default:
                    ProcessDefault(message);
                    break;
            }
        }
        //meta! tag="end"
        public new AgentDopravy MyAgent
        {
            get
            {
                return (AgentDopravy)base.MyAgent;
            }
        }
    }
}
