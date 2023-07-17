using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace AutomaticCapture.Data
{
    public static class CsEncoder
    {
        public static byte[] GetBuffer(Object _objClass)
        {
            int dataSize = Marshal.SizeOf(_objClass);
            IntPtr buff = Marshal.AllocHGlobal(dataSize);
            Marshal.StructureToPtr(_objClass, buff, false);

            byte[] Datas = new byte[dataSize];
            Marshal.Copy(buff, Datas, 0, dataSize);
            Marshal.FreeHGlobal(buff);

            return Datas;
        }

        public static byte[] GetBuffer(object _objClass, int _dataSize)
        {
            int dataSize = _dataSize;
            IntPtr buff = Marshal.AllocHGlobal(dataSize);
            Marshal.StructureToPtr(_objClass, buff, false);

            byte[] Datas = new byte[dataSize];
            Marshal.Copy(buff, Datas, 0, dataSize);
            Marshal.FreeHGlobal(buff);

            return Datas;
        }
    }
}
