using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AutomaticCapture.Capture;

namespace AutomaticCapture
{
    public delegate void BitmapGetDelegate(Bitmap _data);
    public partial class CsAutomaticCapture : Form
    {
        CsCaptureMachine CaptureMachnie = null;
        private bool IsPlay = false;

        public CsAutomaticCapture()
        {
            InitializeComponent();
            this.Initialize();
        }

        private void Initialize()
        {
            CaptureMachnie = new CsCaptureMachine();
            CaptureMachnie.BitmapSendEvent = new BitmapGetDelegate(this.GetBitmap);
        }

        private void GetBitmap(Bitmap _data)
        {
            CvCapturePictureBox.Image = _data;
        }

        private void CvSendButton_Click(object sender, EventArgs e)
        {
            if (IsPlay == false)
            {
                IsPlay = true;
                CaptureMachnie.Play();
                CvSendButton.Text = "STOP";
            }
            else if (IsPlay == true)
            {
                IsPlay = false;
                CaptureMachnie.Stop();
                CvSendButton.Text = "SEND";
            }
        }

        private void CsAutomaticCapture_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    int X = Cursor.Position.X;
                    int Y = Cursor.Position.Y;
                    Console.Out.WriteLine("X : {0}\nY : {1}\n\n", X, Y);

                    CaptureMachnie.ChangeScreenPos(X, Y);
                    break;
            }
        }
    }
}
