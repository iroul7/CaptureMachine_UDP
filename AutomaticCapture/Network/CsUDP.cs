using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace AutomaticCapture.Network
{
    public class CsUDP
    {
        private UdpClient UDPClinetSocket;
        private IPEndPoint Ep;
        private Thread ReceiveThread = null;
        private bool IsReceiveThreadRun = false;
        public event delegateReceivedBuffer ReceivedBuffer;

        private string IpAddress = string.Empty;
        private int PortNumber = 0;
        private bool IsEndThread = false;
        public CsUDP(string _IpAdress, string _PortNumber)
        {
            IpAddress = _IpAdress;
            PortNumber = Convert.ToInt32(_PortNumber);
            this.Initialize();
        }

        public void Initialize()
        {
            this.InitReceiveThread();
        }

        public void BeginToReceive()
        {
            UDPClinetSocket = new UdpClient(PortNumber);
            Ep = new IPEndPoint(IPAddress.Any, 0);
            IsReceiveThreadRun = true;
            ReceiveThread.Start();
        }

        public void Destroy()
        {
            if (UDPClinetSocket != null)
            {
                UDPClinetSocket.Close();
            }
            IsReceiveThreadRun = false;
        }

        public void InitReceiveThread()
        {
            ReceiveThread = new Thread(this.ReceiveBuffer);
        }

        private void ReceiveBuffer()
        {
            UDPClinetSocket.Client.ReceiveTimeout = 100;
            while (IsReceiveThreadRun)
            {
                try
                {
                    byte[] RcvBuffer = UDPClinetSocket.Receive(ref Ep);
                    ReceivedBuffer(RcvBuffer);
                }
                catch (Exception E)
                {
                    Console.WriteLine(E.Message);
                }

            }
            IsEndThread = true;
        }

        public void Send(byte[] _SendBuffer)
        {
            try
            {
                if (UDPClinetSocket == null)
                {
                    UDPClinetSocket = new UdpClient();
                    Ep = new IPEndPoint(IPAddress.Parse(IpAddress), PortNumber);
                }
                UDPClinetSocket.Send(_SendBuffer, _SendBuffer.Length, Ep);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
