using Android.App;
using Android.Content;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App1.Utils
{
    public class GetSatellites : GnssStatus.Callback
    {
        public int satelliteCount;
        public override void OnSatelliteStatusChanged(GnssStatus status)
        {
            base.OnSatelliteStatusChanged(status);
            satelliteCount = status.SatelliteCount;
            int usedCount = 0;
            for (int i = 0; i < satelliteCount; ++i)
            {
                if (status.UsedInFix(i))
                {
                    ++usedCount;
                }
            }

            Log.Debug("MyServiceTag", $"satellites count = {satelliteCount}, used = {usedCount}");


            Log.Debug("satelliteCount", "Total Satellites: " + satelliteCount);
            Console.WriteLine("--------------------------" + satelliteCount);
        }
    }
}