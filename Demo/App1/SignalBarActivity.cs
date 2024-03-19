using Android.App;
using Android.Content.PM;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Widget;
using App1.Utils;
using Java.Lang;
using Java.Util;
using SignalIndicatorsLibrary;
using System;

namespace App1
{
    [Activity(Label = "Signal Bar Example", Theme = "@style/AppTheme", MainLauncher = false)]
    public class SignalBarActivity : Activity, GpsStatus.IListener, ILocationListener
    {

        private ImageView imgGpsSignal;
        private TextView tvGpsSignal;

        private ImageView imgWifiSignal;
        private TextView tvWifiSignal;

        private ImageView imgGsmSignal;
        private TextView tvGsmSignal;

        private ImageView imgSignalLevel;
        private TextView tvSignalLevel;

        private EditText etWifiSignalValue;
        private Button btnSubmitWifiValue;

        private ImageView imgGsmSignalLevel;
        private TextView tvGsmSignalLevel;

        private EditText etGsmSignalValue;
        private Button btnSubmitGsmValue;

        private Handler handler;
        private const int RequestWifiPermissionCode = 1;
        private const int RequestFineLocationPermissionCode = 2;
        private LocationManager locationManager;

        private WifiSignalIndicator wifiSignalIndicator;
        private GsmSignalIndicator gsmSignalIndicator;
        private GpsSignalIndicator gpsSignalIndicator;
        private string locationProvider;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_signal_bar);

            imgGpsSignal = FindViewById<ImageView>(Resource.Id.imgGpsSignal);
            tvGpsSignal = FindViewById<TextView>(Resource.Id.tvGpsSignal);

            imgWifiSignal = FindViewById<ImageView>(Resource.Id.imgWifiSignal);
            tvWifiSignal = FindViewById<TextView>(Resource.Id.tvWifiSignal);

            imgGsmSignal = FindViewById<ImageView>(Resource.Id.imgGsmSignal);
            tvGsmSignal = FindViewById<TextView>(Resource.Id.tvGsmSignal);

            imgSignalLevel = FindViewById<ImageView>(Resource.Id.imgSignalLevel);
            tvSignalLevel = FindViewById<TextView>(Resource.Id.tvSignalLevel);
            etWifiSignalValue = FindViewById<EditText>(Resource.Id.etWifiSignalValue);
            btnSubmitWifiValue = FindViewById<Button>(Resource.Id.btnSubmitWifiValue);
            btnSubmitWifiValue.Click += (sender, e) =>
            {
                SignalLevelUtils.setWifiSignalStrength(
                        this,
                        etWifiSignalValue.Text.Equals("") ? 0 : System.Double.Parse(etWifiSignalValue.Text),
                        tvSignalLevel,
                        imgSignalLevel
                    );
            };


            imgGsmSignalLevel = FindViewById<ImageView>(Resource.Id.imgGsmSignalLevel);
            tvGsmSignalLevel = FindViewById<TextView>(Resource.Id.tvGsmSignalLevel);
            etGsmSignalValue = FindViewById<EditText>(Resource.Id.etGsmSignalValue);
            btnSubmitGsmValue = FindViewById<Button>(Resource.Id.btnSubmitGsmValue);
            btnSubmitGsmValue.Click += (sender, e) =>
            {
                SignalLevelUtils.setGsmSignalStrength(
                        this,
                        etWifiSignalValue.Text.Equals("") ? 0 : System.Double.Parse(etGsmSignalValue.Text),
                        tvGsmSignalLevel,
                        imgGsmSignalLevel
                    );
            };

            wifiSignalIndicator = new WifiSignalIndicator();
            wifiSignalIndicator.init(this, imgWifiSignal);
            wifiSignalIndicator.setSchedule(3000);

            gsmSignalIndicator = new GsmSignalIndicator();
            gsmSignalIndicator.init(this, imgGsmSignal);
            gsmSignalIndicator.setSchedule(3000);


            locationManager = GetSystemService(LocationService) as LocationManager;
            locationManager.RegisterGnssStatusCallback(new GetSatellites(), new Handler(Looper.MainLooper));

            gpsSignalIndicator = new GpsSignalIndicator();
            gpsSignalIndicator.init(this, imgGpsSignal, locationManager, this, this);

            handler = new Handler();


            //schedulingSignalLevel();

            //initLocationManager();

        }

        public void OnGpsStatusChanged(GpsEvent gpsEvent)
        {
            gpsSignalIndicator.setGpsStatusChanged();
        }


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            if (requestCode == wifiSignalIndicator.RequestWifiPermissionCode)
            {
                if (grantResults.Length > 0 && grantResults[0] == Permission.Granted)
                {

                    wifiSignalIndicator.init(this, imgWifiSignal);
                    wifiSignalIndicator.setSchedule(3000);

                }
                else
                {
                    // Izin ditolak oleh pengguna
                    Toast.MakeText(this, "Izin ACCESS_WIFI_STATE ditolak.", ToastLength.Short).Show();
                }
            }

            if (requestCode == gpsSignalIndicator.RequestFineLocationPermissionCode)
            {
                if (grantResults.Length > 0 && grantResults[0] == Permission.Granted)
                {
                    gpsSignalIndicator.init(this, imgGpsSignal, locationManager, this, this);
                }
                else
                {
                    // Izin ditolak oleh pengguna
                    Toast.MakeText(this, "Izin ACCESS_WIFI_STATE ditolak.", ToastLength.Short).Show();
                }
            }
        }

        public void OnLocationChanged(Location location)
        {
            // Mendapatkan lokasi saat ini
            double latitude = location.Latitude;
            double longitude = location.Longitude;

            // Gunakan nilai latitude dan longitude sesuai kebutuhan Anda
            // ...
            //tvGpsSignal.Text = latitude.ToString();

            Log.Debug("latitude: ", latitude.ToString());
            Log.Debug("longitude: ", longitude.ToString());

            // Hentikan pembaruan lokasi jika sudah didapatkan
            //locationManager.RemoveUpdates(this);
        }

        public void OnProviderDisabled(string provider)
        {
            throw new NotImplementedException();
        }

        public void OnProviderEnabled(string provider)
        {
            throw new NotImplementedException();
        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {
            throw new NotImplementedException();
        }
    }
}