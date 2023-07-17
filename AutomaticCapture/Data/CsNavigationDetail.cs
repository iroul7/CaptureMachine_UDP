using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace AutomaticCapture.Data
{
    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    public class CsNavigationDetail
    {
        [FieldOffset(0)]
        public double timestamp;
        [FieldOffset(8)]
        public byte zone_char1;
        [FieldOffset(9)]
        public byte zone_char2;
        [FieldOffset(10)]
        public double east;
        [FieldOffset(18)]
        public double north;
        [FieldOffset(26)]
        public double elevation;
        [FieldOffset(34)]
        public double roll;
        [FieldOffset(42)]
        public double pitch;
        [FieldOffset(50)]
        public double heading;
        [FieldOffset(58)]
        public double l_velocity_x;
        [FieldOffset(66)]
        public double l_velocity_y;
        [FieldOffset(74)]
        public double l_velocity_z;
        [FieldOffset(82)]
        public double a_velocity_x;
        [FieldOffset(90)]
        public double a_velocity_y;
        [FieldOffset(98)]
        public double a_velocity_z;

        public CsNavigationDetail()
        {
            this.Initialize();
        }

        private void Initialize()
        {
            timestamp = 0.0;
            zone_char1 = 0;
            zone_char2 = 0;
            east = 0.0;
            north = 0.0;
            elevation = 0.0;
            roll = 0.0;
            pitch = 0.0;
            heading = 0.0;
            l_velocity_x = 0.0;
            l_velocity_y = 0.0;
            l_velocity_z = 0.0;
            a_velocity_x = 0.0;
            a_velocity_y = 0.0;
            a_velocity_z = 0.0;
        }

        public void SetValues(CsNavigation _Navigation, double _TimeStamp)
        {
            east = _Navigation.East;
            north = _Navigation.North;
            elevation = _Navigation.Elevation;
            roll = _Navigation.roll / 180; // -PI~PI ***
            pitch = _Navigation.pitch / 180; // -PI~PI ***
            heading = _Navigation.yaw / 180; // -PI~PI ***
            l_velocity_x = _Navigation.velocity_x;
            l_velocity_y = _Navigation.velocity_y;
            l_velocity_z = _Navigation.velocity_z;
            a_velocity_x = _Navigation.roll_rate;
            a_velocity_y = _Navigation.pitch_rate;
            a_velocity_z = _Navigation.yaw_rate;
            timestamp = _TimeStamp;
        }
    }
}
