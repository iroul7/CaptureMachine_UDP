using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace AutomaticCapture.Network
{
    public abstract class AbNetworkSystem
    {
        protected void ReadFile(string _FilePath, out List<CsUDP> _Protocols)
        {
            _Protocols = new List<CsUDP>();
            if (File.Exists(_FilePath))
            {
                using (StreamReader Sr = new StreamReader(_FilePath))
                {
                    string ReadLine = string.Empty;
                    while ((ReadLine = Sr.ReadLine()) != null)
                    {
                        string[] Splited = ReadLine.Split(',');
                        string IpAdress = Splited[0].Trim();
                        string Port = Splited[1].Trim();
                        CsUDP UDP = new CsUDP(IpAdress, Port);
                        _Protocols.Add(UDP);
                    }
                }
            }
            else
            {
                Console.WriteLine("{0} does not exist.", _FilePath);
            }
        }

        protected CsUDP[] ReadFile(string _FilePath, int _ProtocolCount)
        {
            CsUDP[] Protocols = null;
            if (File.Exists(_FilePath))
            {
                Protocols = new CsUDP[_ProtocolCount];
                using (StreamReader Sr = new StreamReader(_FilePath))
                {
                    string ReadLine = string.Empty;
                    for (int i = 0; i < _ProtocolCount; i++)
                    {
                        ReadLine = Sr.ReadLine();
                        string[] Splited = ReadLine.Split(',');
                        string IpAdress = Splited[0].Trim();
                        string Port = Splited[1].Trim();
                        Protocols[i] = new CsUDP(IpAdress, Port);
                    }
                }
            }
            else
            {
                Console.WriteLine("{0} does not exist.", _FilePath);
            }
            return Protocols;
        }

        protected void DestroyPorotoclos(List<CsUDP> _Protocols)
        {
            foreach (CsUDP Protocol in _Protocols)
            {
                Protocol.Destroy();
            }
        }
    }
}
