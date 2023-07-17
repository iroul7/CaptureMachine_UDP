using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace AutomaticCapture.Data
{
    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    public class CsHeader_16Byte
    {
        [FieldOffset(0)]
        public byte MessageType;
        [FieldOffset(1)]
        public byte MessageID;
        [FieldOffset(2)]
        public byte SourUnit;
        [FieldOffset(3)]
        public byte DestUnit;
        [FieldOffset(4)]
        public byte SequenceNumber;
        [FieldOffset(5)]
        public byte QoS;
        [FieldOffset(6)]
        public ushort Length;
        [FieldOffset(8)]
        public ushort SourComp;
        [FieldOffset(10)]
        public ushort DestComp;
        [FieldOffset(12)]
        public uint LengthAdd;

        public CsHeader_16Byte()
        {
            this.Initialize();
        }

        public byte[] GetBuffer()
        {
            return CsEncoder.GetBuffer(this);
        }

        private void Initialize()
        {
            MessageType = 0;
            MessageID = 0;
            SourUnit = 0;
            DestUnit = 0;
            SequenceNumber = 0;
            QoS = 0;
            Length = 0;
            SourComp = 0;
            DestComp = 0;
            LengthAdd = 0;
        }
    }
}
