using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;

namespace AutomaticCapture.ETC
{    
    public static class CsTools
    {
        public static byte[] Merging2Buffers(byte[] _FrontBuffer, byte[] _RearBuffer)
        {
            int FrontBufferSize = _FrontBuffer.Length;
            int RearBufferSize = _RearBuffer.Length;
            int MergingBufferSize = FrontBufferSize + _RearBuffer.Length;

            byte[] MergingBuffer = new byte[MergingBufferSize];
            Array.Copy(_FrontBuffer, 0, MergingBuffer, 0, FrontBufferSize);
            Array.Copy(_RearBuffer, 0, MergingBuffer, FrontBufferSize, RearBufferSize);

            return MergingBuffer;
        }

        public static byte[] MakeContext(char _Value, int _Count)
        {
            byte ConvertedV = (byte)((int)_Value);
            byte[] Out = new byte[_Count];

            for (int i = 0; i < _Count; i++)
            {
                Out[i] = ConvertedV;
            }

            return Out;
        }
    }
}
