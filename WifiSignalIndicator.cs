using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Widget;
using SignalIndicatorsLibrary.Utils;
using System;

namespace SignalIndicatorsLibrary
{
    public class WifiSignalIndicator
    {

        private ImageView imageView;
        private Activity activity;
        private Handler handler;
        public int RequestWifiPermissionCode = 1;

        public void init(
                Activity activity,
                ImageView imageView
            )
        {
            this.imageView = imageView;
            this.activity = activity;
            handler = new Handler();
        }

        public void setSchedule(int milliseconds) {
            if (activity.CheckSelfPermission(Android.Manifest.Permission.AccessWifiState) == Permission.Granted)
            {
                var thread = new System.Threading.Thread(() =>
                {

                    while (true)
                    {
                        handler.Post(() =>
                        {
                            // Panggil metode yang ingin dieksekusi setiap 3 detik di sini
                            WifiSignalLevelUtils.get(
                                    activity,
                                    imageView
                                );
                        });

                        // Tunggu 3 detik sebelum mengulang eksekusi
                        System.Threading.Thread.Sleep(milliseconds);
                    }

                });

                thread.Start();
            }
            else
            {
                // Izin belum diberikan, minta izin secara dinamis
                activity.RequestPermissions(new string[] { Android.Manifest.Permission.AccessWifiState }, RequestWifiPermissionCode);
            }
        }

        public string getDbm() {
            return WifiSignalLevelUtils.GetDbm(activity);
        }

    }
}
