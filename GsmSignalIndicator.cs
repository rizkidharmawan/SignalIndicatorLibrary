using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SignalIndicatorsLibrary.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SignalIndicatorsLibrary
{
    public class GsmSignalIndicator
    {

        private ImageView imageView;
        private Activity activity;
        private Handler handler;

        public void init(
                Activity activity,
                ImageView imageView
            )
        {
            this.imageView = imageView;
            this.activity = activity;
            handler = new Handler();
        }

        public void setSchedule(int milliseconds)
        {
            var thread = new System.Threading.Thread(() =>
            {

                while (true)
                {
                    handler.Post(() =>
                    {
                        // Panggil metode yang ingin dieksekusi setiap 3 detik di sini
                        GsmSignalLevelUtils.get(
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

        public string getDbm()
        {
            return GsmSignalLevelUtils.GetDbm(activity);
        }

    }
}