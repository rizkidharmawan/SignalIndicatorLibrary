using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Java.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SignalIndicatorsLibrary
{
    public class GpsSignalIndicator
    {

        private ImageView imgGpsSignal;
        private Activity activity;
        private LocationManager locationManager;
        public int RequestFineLocationPermissionCode = 2;
        private ILocationListener locationListener;
        private GpsStatus.IListener listener;

        public void init(
                Activity activity,
                ImageView imgGpsSignal,
                LocationManager locationManager,
                ILocationListener locationListener,
                GpsStatus.IListener listener
            )
        {
            this.imgGpsSignal = imgGpsSignal;
            this.activity = activity;
            this.locationManager = locationManager;
            this.locationListener = locationListener;
            this.listener = listener;
            initLocationManager();
        }

        private void initLocationManager()
        {

            // Periksa apakah izin ACCESS_FINE_LOCATION sudah diberikan sebelumnya
            if (activity.CheckSelfPermission(Android.Manifest.Permission.AccessFineLocation) == Permission.Granted)
            {
                locationManager.AddGpsStatusListener(listener);
                // Request pembaruan lokasi
                locationManager.RequestLocationUpdates(LocationManager.GpsProvider, 0, 0, locationListener);
            }
            else
            {
                // Izin ACCESS_FINE_LOCATION belum diberikan, mungkin perlu meminta izin di sini
                // ...
                activity.RequestPermissions(new string[] { Android.Manifest.Permission.AccessFineLocation }, RequestFineLocationPermissionCode);
            }
        }

        public void setGpsStatusChanged() {
            GpsStatus gpsStatus = locationManager.GetGpsStatus(null);

            if (gpsStatus != null)
            {
                IIterable satellites = gpsStatus.Satellites;

                int totalSatellites = 0;
                int strongSignalCount = 0;
                int moderateSignalCount = 0;
                int weakSignalCount = 0;

                IIterator sat = satellites.Iterator();
                string lSatellites = null;
                int i = 0;
                while (sat.HasNext)
                {
                    GpsSatellite satellite = sat.Next().JavaCast<GpsSatellite>();
                    totalSatellites++;

                    // Jika metode GetSnr() tidak tersedia, gunakan GetAzimuth(), GetElevation(), atau GetPrn() jika diperlukan.
                    float snr = satellite.Snr;

                    if (snr >= 35)
                    {
                        strongSignalCount++;
                    }
                    else if (snr >= 25)
                    {
                        moderateSignalCount++;
                    }
                    else
                    {
                        weakSignalCount++;
                    }

                    Log.Debug("Satellite", satellite.Prn.ToString());
                }

                // Kategori kekuatan sinyal berdasarkan jumlah satelit
                Log.Debug("SatelliteStatus", "Total Satellites: " + totalSatellites);
                Log.Debug("SatelliteStatus", "Strong Signal Count: " + strongSignalCount);
                Log.Debug("SatelliteStatus", "Moderate Signal Count: " + moderateSignalCount);
                Log.Debug("SatelliteStatus", "Weak Signal Count: " + weakSignalCount);

                if (totalSatellites >= 8)
                {
                    imgGpsSignal.SetImageResource(Resource.Drawable.ic_gps_full_bar); // Ganti dengan gambar 3 bar Anda
                }
                else if (totalSatellites >= 5)
                {
                    imgGpsSignal.SetImageResource(Resource.Drawable.ic_gps_2_bar); // Ganti dengan gambar 2 bar Anda
                }
                else
                {
                    imgGpsSignal.SetImageResource(Resource.Drawable.ic_gps_1_bar); // Ganti dengan gambar 1 bar Anda
                }


                // Tampilkan gambar sesuai dengan kategori kekuatan sinyal
                //if (strongSignalCount > 0)
                //{
                //    activity.RunOnUiThread(() =>
                //    {
                //        imgGpsSignal.SetImageResource(Resource.Drawable.ic_gps_full_bar); // Ganti dengan gambar 3 bar Anda
                //    });
                //}
                //else if (moderateSignalCount > 0)
                //{
                //    activity.RunOnUiThread(() =>
                //    {
                //        imgGpsSignal.SetImageResource(Resource.Drawable.ic_gps_2_bar); // Ganti dengan gambar 2 bar Anda
                //    });
                //}
                //else
                //{
                //    activity.RunOnUiThread(() =>
                //    {
                //        imgGpsSignal.SetImageResource(Resource.Drawable.ic_gps_1_bar); // Ganti dengan gambar 1 bar Anda
                //    });
                //}
            }
        }
    }
}