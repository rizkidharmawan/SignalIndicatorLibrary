using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.Core.App;
using AndroidX.Core.Content;
using AndroidX.RecyclerView.Widget;
using System.Collections.Generic;
using System.IO;

namespace App1
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class FirstActivity : AppCompatActivity
    {

        private Button btnGpio;
        private Button btnSignalBar;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_first);



            btnGpio = FindViewById<Button>(Resource.Id.btnGpio);
            btnGpio.Click += (sender, e) =>
            {
                Intent intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
            };

            btnSignalBar = FindViewById<Button>(Resource.Id.btnSignalBar);
            btnSignalBar.Click += (sender, e) =>
            {

                Intent intent = new Intent(this, typeof(SignalBarActivity));
                StartActivity(intent);
            };

           
        }
    }
}