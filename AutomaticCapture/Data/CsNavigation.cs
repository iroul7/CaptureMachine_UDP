using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace AutomaticCapture.Data
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class CsNavigation : IBuffer, IClass
    {
        public double East;
        public double North;
        public double Elevation;
        public double roll;
        public double pitch;
        public double yaw;
        public double velocity_x;
        public double velocity_y;
        public double velocity_z;
        public double roll_rate;
        public double pitch_rate;
        public double yaw_rate;
        public double a_x;
        public double a_y;
        public double a_z;

        public CsNavigation()
        {
            this.Initialize();
        }

        public CsNavigation(CsNavigation _Navigation)
        {
            this.Copy(_Navigation);
        }

        public void Copy(CsNavigation _Navigation)
        {
            East = _Navigation.East;
            North = _Navigation.North;
            Elevation = _Navigation.Elevation;
            roll = _Navigation.roll; // -PI~PI
            pitch = _Navigation.pitch; // -PI~PI
            yaw = _Navigation.yaw; // -PI~PI
            velocity_x = _Navigation.velocity_x;
            velocity_y = _Navigation.velocity_y;
            velocity_z = _Navigation.velocity_z;
            roll_rate = _Navigation.roll_rate;
            pitch_rate = _Navigation.pitch_rate;
            yaw_rate = _Navigation.yaw_rate;
            a_x = _Navigation.a_x;
            a_y = _Navigation.a_y;
            a_z = _Navigation.a_z;
        }

        private void Initialize()
        {
            East = 0.0;
            North = 0.0;
            Elevation = 0.0;
            roll = 0.0;
            pitch = 0.0;
            yaw = 0.0;
            velocity_x = 0.0;
            velocity_y = 0.0;
            velocity_z = 0.0;
            roll_rate = 0.0;
            pitch_rate = 0.0;
            yaw_rate = 0.0;
            a_x = 0.0;
            a_y = 0.0;
            a_z = 0.0;
        }

        public byte[] GetBuffer()
        {
            return CsEncoder.GetBuffer(this);
        }

        public void Print()
        {
            this.ConvertData();
        }

        public void ConvertData()
        {
            uint CvtEast = (uint)(East * 10);
            uint CvtNorth = (uint)(North * 10);
            int CvtElevation = (int)(Elevation * 10);
            short Cvtroll = (short)((roll * Math.PI) * 17.777778 * 10);
            short Cvtpitch = (short)((pitch * Math.PI) * 17.777778 * 10);
            short Cvtyaw = (short)((yaw * Math.PI) * 17.777778 * 10);
            short Cvtvelocity_x = (short)(velocity_x * 100);
            short Cvtvelocity_y = (short)(velocity_y * 100);
            short Cvtvelocity_z = (short)(velocity_z * 100);
            short Cvtroll_rate = 0;
            short Cvtpitch_rate = 0;
            short Cvtyaw_rate = 0;
            short Cvta_x = (short)(a_x * 100);
            short Cvta_y = (short)(a_y * 100);
            short Cvta_z = (short)(a_z * 100);

            /*Console.WriteLine($"East  : {CvtEast}, North  : {CvtNorth}, Elevation  : {CvtElevation}, roll  : {Cvtroll}, pitch  : {Cvtpitch}," +
                $" yaw  : {Cvtyaw}, velocity_x  : {Cvtvelocity_x}, velocity_y  : {Cvtvelocity_y}, velocity_z  : {Cvtvelocity_z}," +
                $" roll_rate  : {Cvtroll_rate}, pitch_rate  : {Cvtpitch_rate}, yaw_rate  : {Cvtyaw_rate}, a_x  : {Cvta_x}, a_y  : {Cvta_y}, a_z  : {Cvta_z},");*/
        }
    }
}
