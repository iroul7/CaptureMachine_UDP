using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutomaticCapture.Network;

namespace AutomaticCapture
{
    public class CsNetworkSystem : AbNetworkSystem
    {
        public List<CsUDP> SendCaptureDatas = null;
        public CsNetworkSystem()
        {
            this.Initialize();
        }

        private void Initialize()
        {
            string ConfigFolder = @"../Config\";

            string OutFile = ConfigFolder + @"Out.txt";
            this.ReadFile(OutFile, out SendCaptureDatas);
        }

        private void Destroy()
        {
            this.DestroyPorotoclos(SendCaptureDatas);
        }

        public void SendCaptureData(byte[] _buffer)
        {
            foreach (CsUDP UDP in SendCaptureDatas)
            {
                UDP.Send(_buffer);
            }
        }
    }
}
