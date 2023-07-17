using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace AutomaticCapture.Network
{
    enum enumNetworkType
    {
        eTCP,
        eUDP
    }
    public delegate void delegateReceivedBuffer(byte[] _buffer);
    public abstract class AbBaseProtocol
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string IpAddress { get; set; }
        public int PortNumber { get; set; }
        public int NetType { get; set; }

        private Stopwatch Sw { get; set; }

        protected AbBaseProtocol(string[] _ValueInformation)
        {
            this.SetValues(_ValueInformation);
            Sw = new Stopwatch();
        }
        public abstract void Initialize();
        public abstract void Send(byte[] _msg);
        public abstract void Begin();
        public abstract void Destroy();

        public void Tick()
        {
            Sw.Reset();
            Sw.Start();
        }

        public uint Tock()
        {
            Sw.Stop();
            string ElapsedTime = Sw.ElapsedMilliseconds.ToString();

            Console.WriteLine("Count Time : {0}ms", ElapsedTime);
            uint CurrentTime = Convert.ToUInt32(ElapsedTime);

            return CurrentTime;
        }

        public void SetValues(string[] _ValueInformation)
        {
            Name = _ValueInformation[0];
            Type = _ValueInformation[1];
            IpAddress = _ValueInformation[2];
            PortNumber = Convert.ToInt32(_ValueInformation[3]);
        }
    }
}
