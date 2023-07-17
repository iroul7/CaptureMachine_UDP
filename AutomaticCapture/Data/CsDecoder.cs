using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace AutomaticCapture.Data
{
    public static class CsDecoder
    {
        static int Offset = 0;
        public static IClass GetInstance(byte[] _buffer)
        {
            IClass ClassInstance = null;
            byte MessageID = GetMessageID(_buffer);

            switch (MessageID)
            {
                case 0x00:
                    {
                        break;
                    }
                case 0x11:
                    {
                        break;
                    }

                case 0x09:
                    {
                        break;
                    }

                case 0x0A:
                    {
                        break;
                    }
            }

            return ClassInstance;
        }

        private static byte GetMessageID(byte[] _buffer)
        {
            byte[] HeaderBuffer = GetHeaderFromBuffer(_buffer);
            byte MessageID = GetMessageIDFromHeaderBuffer(HeaderBuffer);

            return MessageID;
        }

        private static byte[] GetHeaderFromBuffer(byte[] _buffer)
        {
            int HeaderSize = 16;
            byte[] HeaderBuffer = new byte[HeaderSize];

            Array.Copy(_buffer, Offset, HeaderBuffer, 0, HeaderSize);

            return HeaderBuffer;
        }

        private static byte GetMessageIDFromHeaderBuffer(byte[] _headerBuffer)
        {
            byte MessageID = _headerBuffer[1];

            return MessageID;
        }

        private static byte[] GetHeadBuffer(byte[] _buffer, int _headSize)
        {
            byte[] HeadBuffer = new byte[_headSize];

            Array.Copy(_buffer, HeadBuffer, _headSize);

            return HeadBuffer;
        }

        public static object CreateObject<T>(byte[] _buffer) where T : class
        {
            IntPtr ClassPtr = Marshal.AllocHGlobal(_buffer.Length);
            Marshal.Copy(_buffer, 0, ClassPtr, _buffer.Length);

            object obj = Marshal.PtrToStructure(ClassPtr, typeof(T));

            return obj;
        }

        private static T[] CreateVariableObject<T>(byte[] _buffer, int _beforeVariableBufferSize, int _totalPointNo) where T : class
        {
            int ArraySize = _buffer.Length - _beforeVariableBufferSize;
            IntPtr ArrayPtr = Marshal.AllocHGlobal(ArraySize);
            Marshal.Copy(_buffer, _beforeVariableBufferSize, ArrayPtr, ArraySize);

            T[] managedArray = new T[_totalPointNo];
            int TSize = Marshal.SizeOf(typeof(T));

            for (int i = 0; i < _totalPointNo; i++)
            {
                IntPtr Ins = ArrayPtr + i * TSize;
                managedArray[i] = (T)Marshal.PtrToStructure(Ins, typeof(T));//Marshal.PtrToStructure<T>(Ins);

            }

            return managedArray;
        }
    }
}
