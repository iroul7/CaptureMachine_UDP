using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading;
using AutomaticCapture.Data;
using AutomaticCapture.ETC;

namespace AutomaticCapture.Capture
{
    static class Constants
    {
        public const int WIDTH = 640;
        public const int HEIGHT = 480;

        public const int BUFFER_FULL_LENGTH = 921654;
        public const int BUFFER_HEAD_LENGTH = 54;
        public const int BUFFER_BODY_LENGTH = 921600;
        public const int BUFFER_SEND_LENGTH = 60000;

        public const int BUFFER_ADD_FULL_LENGTH = 921600 + 146;
    }

    public struct StScreen
    {
        public int X;
        public int Y;
    }

    public class CsCaptureMachine
    {
        public BitmapGetDelegate BitmapSendEvent;
        private CsNetworkSystem NetworkSystem = null;
        private CsWindowPosition WindowPosition = null;
        private BackgroundWorker CaptureWorker = null;
        private BackgroundWorker SendWorker = null;
        private Stopwatch Sw = null;
        private Bitmap Bt = null;
        private StScreen WinScreen;
        private Size Sz;
        private byte[] RGBDatas = null;
        private byte[] ConvertRGBDatas = null;
        private byte[] ADDFullDatas = null;
        private bool IsDone = false;
        private int XMargin = 0;
        private int YMargin = 0;

        public CsCaptureMachine()
        {
            this.Initialize();
        }

        private void Initialize()
        {
            NetworkSystem = new CsNetworkSystem();
            WindowPosition = new CsWindowPosition();
            WinScreen = new StScreen();
            Sw = new Stopwatch();

            RGBDatas = new byte[Constants.BUFFER_FULL_LENGTH];
            ConvertRGBDatas = new byte[Constants.BUFFER_FULL_LENGTH];
            ADDFullDatas = new byte[Constants.BUFFER_ADD_FULL_LENGTH];
            ADDFullDatas = this.MakeADDBufferInit();

            InitMonitor();
            InitBackgourdWorker();            
        }

        private void InitMonitor()
        {
            Bt = new Bitmap(Constants.WIDTH, Constants.HEIGHT, PixelFormat.Format24bppRgb);
            Sz = new Size(Constants.WIDTH, Constants.HEIGHT);

            Point ScPt = new Point();
            Size ScSz = new Size();
            WindowPosition.GetWindowPos(WindowPosition.GetWinAscHandle(), ref ScPt, ref ScSz);

            this.InitMonitorMargin();

            WinScreen.X = ScPt.X + XMargin;
            WinScreen.Y = ScPt.Y + YMargin;
        }

        private void InitMonitorMargin()
        {
            string MonitorMarginInfoPath = @"../Config\MonitorMarginInfoFile.txt";
            string ReadLine = System.IO.File.ReadAllText(MonitorMarginInfoPath);
            string[] LineSplit = ReadLine.Split(',');

            if (LineSplit.Length > 1)
            {
                XMargin = Convert.ToInt32(LineSplit[0]);
                YMargin = Convert.ToInt32(LineSplit[1]);
            }
        }

        private void InitBackgourdWorker()
        {
            CaptureWorker = new BackgroundWorker();
            CaptureWorker.DoWork += new DoWorkEventHandler(CaptureWorker_DoWork);
            CaptureWorker.WorkerSupportsCancellation = true;

            SendWorker = new BackgroundWorker();
            SendWorker.DoWork += new DoWorkEventHandler(SendWorker_DoWork);
            SendWorker.WorkerSupportsCancellation = true;
        }

        void CaptureWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                if (CaptureWorker.CancellationPending == true)
                {
                    e.Cancel = true;
                    return;
                }
                else
                {
                    this.Capture();
                    Thread.Sleep(5);
                }
            }
        }

        void SendWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                if (CaptureWorker.CancellationPending == true)
                {
                    e.Cancel = true;
                    return;
                }
                else
                {
                    this.SendADD(ADDFullDatas, 65000);
                }
            }
        }

        public void Play()
        {
            CaptureWorker.RunWorkerAsync();
            SendWorker.RunWorkerAsync();
        }

        public void Stop()
        {
            CaptureWorker.CancelAsync();
            SendWorker.CancelAsync();
        }

        public void ChangeScreenPos(int _X, int _Y)
        {
            WinScreen.X = _X - 7;
            WinScreen.Y = _Y;
        }

        private void Capture()
        {
            if (IsDone == false)
            {
                this.CaptureMonitor();
            }
        }

        private void CaptureMonitor()
        {
            Graphics Gp = Graphics.FromImage(Bt);
            //Gp.CopyFromScreen(500, 300, 0, 0, Sz);
            Gp.CopyFromScreen(WinScreen.X + 7, WinScreen.Y, 0, 0, Sz);
            
            if (Bt != null)
            {
                // Bitmap to Byte Array
                MemoryStream Ms = new MemoryStream();
                Bt.Save(Ms, ImageFormat.Bmp);
                RGBDatas = Ms.ToArray();

                // Reverse
                Array.Reverse(RGBDatas);

                // Reverse Width
                // this.ReverseWidth(RGBDatas, Constants.WIDTH, Constants.HEIGHT);
                //
                byte[] WidthData = new byte[640 * 3];
                for (int i = 0; i < 480; i++)
                {
                    Buffer.BlockCopy(RGBDatas, i * 640 * 3 , WidthData, 0, WidthData.Length);

                    int WidthCount = 0;
                    for (int j = 0; j < 960; j += 3)
                    {
                        WidthCount++;
                        byte temp1 = WidthData[j];
                        byte temp2 = WidthData[j + 1];
                        byte temp3 = WidthData[j + 2];

                        int a = 1920 - ((3 * WidthCount) - 0);
                        int b = 1920 - ((3 * WidthCount) - 1);
                        int c = 1920 - ((3 * WidthCount) - 2);

                        WidthData[j] = WidthData[a];
                        WidthData[j + 1] = WidthData[b];
                        WidthData[j + 2] = WidthData[c];

                        WidthData[a] = temp1;
                        WidthData[b] = temp2;
                        WidthData[c] = temp3;
                    }

                    Buffer.BlockCopy(WidthData, 0, RGBDatas, i * 640 * 3, WidthData.Length);
                }
                //

                Buffer.BlockCopy(RGBDatas, 0, ADDFullDatas, 142, RGBDatas.Length - 54);

                IsDone = true;

                // Bitmap Draw Test
                /*
                Bitmap ResultBt;
                using (var ms = new MemoryStream(ConvertRGBDatas))
                {
                    ResultBt = new Bitmap(ms);

                    //BitmapSendEvent(ResultBt);
                    //Thread.Sleep(10);
                }*/

            }
        }

        private void SendADD(byte[] _SendDatas, int _SplitBufferSize)
        {
            if (IsDone)
            {
                int ConvertBufferSize = _SendDatas.Length;
                double BufferDivide = ConvertBufferSize / _SplitBufferSize;
                double Quotient = Math.Truncate(BufferDivide);
                double Remain = ConvertBufferSize % _SplitBufferSize;

                byte[] SendRGBDatas = new byte[ConvertBufferSize];
                Buffer.BlockCopy(_SendDatas, 0, SendRGBDatas, 0, ConvertBufferSize);

                int nQuotient = Convert.ToInt32(Quotient);
                int nRemain = Convert.ToInt32(Remain);

                for (int i = 0; i < nQuotient + 1; i++)
                {
                    if (i < nQuotient)
                    {
                        byte[] SendData = new byte[_SplitBufferSize];
                        Buffer.BlockCopy(SendRGBDatas, i * _SplitBufferSize, SendData, 0, _SplitBufferSize);
                        // Send Data
                        NetworkSystem.SendCaptureData(SendData);
                    }
                    else
                    {
                        byte[] SendData = new byte[nRemain];
                        Buffer.BlockCopy(SendRGBDatas, i * _SplitBufferSize, SendData, 0, nRemain);
                        // Send Data
                        NetworkSystem.SendCaptureData(SendData);
                        // Thread.Sleep(3);
                    }
                }
                IsDone = false;
            }
        }

        private byte[] MakeADDBufferInit()
        {
            int ImageBufferSize = Constants.BUFFER_BODY_LENGTH;
            CsSensorData_Image_Body SensorData_Image_Body = new CsSensorData_Image_Body();
            SensorData_Image_Body.Header_16Byte.MessageType = 0xff;
            SensorData_Image_Body.Header_16Byte.MessageID = 0x26;
            SensorData_Image_Body.Header_16Byte.LengthAdd = (uint)(139 + ImageBufferSize);
            SensorData_Image_Body.Header_16Byte.SourUnit = 67;
            SensorData_Image_Body.SyncTime = 0.0;
            SensorData_Image_Body.SensorType = 301;
            SensorData_Image_Body.ImageSize_Width = 640;
            SensorData_Image_Body.ImageSize_Height = 480;
            SensorData_Image_Body.BitPixel = 8;
            SensorData_Image_Body.Channels = 3;
            SensorData_Image_Body.ImageCount = 1;

            byte[] BodyBuffer = SensorData_Image_Body.GetBuffer();
            ADDFullDatas = this.UpperLower(ADDFullDatas);

            Buffer.BlockCopy(BodyBuffer, 0, ADDFullDatas, 3, BodyBuffer.Length);
            return ADDFullDatas;
        }

        private byte[] UpperLower(byte[] _Buffer)
        {
            byte[] Upper = CsTools.MakeContext('#', 3);
            Buffer.BlockCopy(Upper, 0, _Buffer, 0, Upper.Length);

            byte[] Lower = CsTools.MakeContext((char)0xFF, 1);
            byte[] Lower1 = CsTools.MakeContext('@', 3);

            Buffer.BlockCopy(Lower, 0, _Buffer, _Buffer.Length - 4, Lower.Length);
            Buffer.BlockCopy(Lower1, 0, _Buffer, _Buffer.Length - 3, Lower1.Length);

            return _Buffer;
        }

        private void ReverseWidth(byte[] _Buffer, int _Width, int _Height)
        {
            byte[] WidthData = new byte[_Width * 3];

            for (int i = 0; i < _Height; i++)
            {
                Buffer.BlockCopy(_Buffer, i * _Width * 3, WidthData, 0, WidthData.Length);

                int WidthCount = 0;
                for (int j = 0; j < (_Width * 3) / 2; j += 3)
                {
                    WidthCount++;
                    byte temp1 = WidthData[j];
                    byte temp2 = WidthData[j + 1];
                    byte temp3 = WidthData[j + 2];

                    int a = (_Width * 3) - ((3 * WidthCount) - 0);
                    int b = (_Width * 3) - ((3 * WidthCount) - 1);
                    int c = (_Width * 3) - ((3 * WidthCount) - 2);

                    WidthData[j] = WidthData[a];
                    WidthData[j + 1] = WidthData[b];
                    WidthData[j + 2] = WidthData[c];

                    WidthData[a] = temp1;
                    WidthData[b] = temp2;
                    WidthData[c] = temp3;
                }

                Buffer.BlockCopy(WidthData, 0, _Buffer, i * _Width * 3, WidthData.Length);
            }
        }

        private void ReverseRGB(byte[] _data)
        {
            for (int i = 0; i < _data.Length; i += 3)
            {
                byte temp = _data[i];
                _data[i] = _data[i + 2];
                _data[i + 2] = temp;
            }
        }
    }
}