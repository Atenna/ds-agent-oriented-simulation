using System;
using System.CodeDom;
using ds_agent_oriented_simulation.Agents;
using ds_agent_oriented_simulation.Entities.Vehicles;
using ds_agent_oriented_simulation.Settings;
using ds_agent_oriented_simulation.Simulation;
using OSPABA;

namespace ds_agent_oriented_simulation.Managers
{
    //meta! id="17"
    public class ManagerSkladky : Manager
    {
        private MyMessage requestCopyMessage;
        public ManagerSkladky(int id, OSPABA.Simulation mySim, Agent myAgent) :
            base(id, mySim, myAgent)
        {
            Init();
        }

        override public void PrepareReplication()
        {
            base.PrepareReplication();
            // Setup component for the next replication

            if (PetriNet != null)
            {
                PetriNet.Clear();
            }
        }

        //meta! sender="AgentDopravy", id="36", type="Request"
        public void ProcessNalozAuto(MessageForm message)
        {
            Vehicle naNalozenie = ((MyMessage)message).Car;
            requestCopyMessage = (MyMessage)((MyMessage)message).CreateCopy();
            requestCopyMessage.Car = naNalozenie;

            // TO=DO - KOLKO SA BUDE NAKLADAT NA AUTO ak bude na skladke menej materialu? Pocka na dovoz????

            if (MyAgent.NakladacAIsWorking && MyAgent.NakladacBIsWorking)
            {
                lock (naNalozenie)
                {
                    MyAgent.AutaSkladkaQueue.AddLast(naNalozenie);
                }
            }
            else
            {
                if (MyAgent.NakladacAIsWorking)
                {
                    message.Addressee = ((AgentSkladky)MyAgent).procesNakladacB;
                    MyAgent.NakladacBIsWorking = true;
                    StartContinualAssistant(message);
                }
                else
                {
                    message.Addressee = ((AgentSkladky)MyAgent).procesNakladacA;
                    MyAgent.NakladacAIsWorking = true;
                    StartContinualAssistant(message);
                }
            }
        }

        //meta! userInfo="Process messages defined in code", id="0"
        public void ProcessDefault(MessageForm message)
        {
            switch (message.Code)
            {
            }
        }

        //meta! sender="ProcesNakladacA", id="64", type="Finish"
        public void ProcessFinishProcesNakladacA(MessageForm message)
        {
            MyAgent.NakladacAIsWorking = false;
            message.Addressee = MySim.FindAgent(SimId.AgentDopravy);
            message.Code = Mc.NalozAuto;
            try
            {
                Response(message);
            }
            catch (System.ArgumentOutOfRangeException e)
            {

            }

            // ak v rade niekto dalsi caka, zacne sa znova nakladanie
            lock (Constants.queueLock)
            {
                if (!MyAgent.AutaSkladkaQueue.IsEmpty())
                {
                    var naNalozenie = MyAgent.AutaSkladkaQueue.First.Value;
                    MyAgent.AutaSkladkaQueue.RemoveFirst();

                    MyMessage msg = new MyMessage(MySim, naNalozenie);
                    msg.Code = Mc.NalozAuto;
                    msg.Addressee = MySim.FindAgent(SimId.AgentSkladky);
                    Request(msg);
                }
            }
        }

        //meta! sender="ProcesNakladacB", id="70", type="Finish"
        public void ProcessFinishProcesNakladacB(MessageForm message) 
        {
            MyAgent.NakladacBIsWorking = false;
            message.Addressee = MySim.FindAgent(SimId.AgentDopravy);
            message.Code = Mc.NalozAuto;
            try
            {
                Response(message);
            }
            catch (System.ArgumentOutOfRangeException e)
            {

            }
            

            // ak v rade niekto dalsi caka, zacne sa znova nakladanie
            lock (Constants.queueLock)
            {
                if (!MyAgent.AutaSkladkaQueue.IsEmpty())
                {
                    var naNalozenie = MyAgent.AutaSkladkaQueue.First.Value;
                    MyAgent.AutaSkladkaQueue.RemoveFirst();

                    MyMessage msg = new MyMessage(MySim, naNalozenie);
                    msg.Code = Mc.NalozAuto;
                    msg.Addressee = MySim.FindAgent(SimId.AgentSkladky);
                    Request(msg);
                }
            }
        }

        //meta! userInfo="Generated code: do not modify", tag="begin"
        public void Init()
        {
        }

        override public void ProcessMessage(MessageForm message)
        {
            switch (message.Code)
            {
                case Mc.Finish:
                    switch (message.Sender.Id)
                    {
                        case SimId.ProcesNakladacA:
                            ProcessFinishProcesNakladacA(message);
                            break;

                        case SimId.ProcesNakladacB:
                            ProcessFinishProcesNakladacB(message);
                            break;
                    }
                    break;

                case Mc.NalozAuto:
                    ProcessNalozAuto(message);
                    break;

                default:
                    ProcessDefault(message);
                    break;
            }
        }
        //meta! tag="end"
        public new AgentSkladky MyAgent
        {
            get
            {
                return (AgentSkladky)base.MyAgent;
            }
        }
    }
}