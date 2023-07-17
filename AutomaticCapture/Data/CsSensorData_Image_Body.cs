using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace AutomaticCapture.Data
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class CsSensorData_Image_Body : IBuffer
    {
        public CsHeader_16Byte Header_16Byte;
        public double SyncTime;
        public CsNavigationDetail NavigationDetail;
        public ushort SensorType;
        public ushort ImageSize_Width;
        public ushort ImageSize_Height;
        public byte BitPixel;
        public byte Channels;
        public byte ImageCount;

        public CsSensorData_Image_Body()
        {
            this.Initialize();
        }

        private void Initialize()
        {
            Header_16Byte = new CsHeader_16Byte();
            SyncTime = 0.0;
            NavigationDetail = new CsNavigationDetail();
            SensorType = 0;
            ImageSize_Width = 0;
            ImageSize_Height = 0;
            BitPixel = 0;
            Channels = 0;
            ImageCount = 0;
        }

        public CsHeader_16Byte GetHeader()
        {
            return Header_16Byte;
        }

        public CsNavigationDetail GetNavigationDetail()
        {
            return NavigationDetail;
        }

        public byte[] GetBuffer()
        {
            return CsEncoder.GetBuffer(this);
        }

        public void Print()
        {

        }
    }
}
