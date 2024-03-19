using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Net;
using Android.Telephony;
using Android.Util;
using Android.Widget;
using AndroidX.Core.App;
using AndroidX.Core.Content;
namespace SignalIndicatorsLibrary.Utils
{
    class GsmSignalLevelUtils
    {

        private static string signalType = "";
        public static void get(
            Activity activity,
            ImageView imgGsmSignal
            )
        {

            double signalDbm = double.Parse(GetDbm(activity));

            if (signalDbm >= -70)
            {
                activity.RunOnUiThread(() =>
                {
                    imgGsmSignal.SetBackgroundColor(new Android.Graphics.Color(ContextCompat.GetColor(activity, Resource.Color.green))); // Sangat Kuat
                });
            }
            else if (signalDbm >= -85)
            {
                activity.RunOnUiThread(() =>
                {
                    imgGsmSignal.SetBackgroundColor(new Android.Graphics.Color(ContextCompat.GetColor(activity, Resource.Color.yellow))); // Kuat
                });
            }
            else if (signalDbm >= -100)
            {
                activity.RunOnUiThread(() =>
                {
                    imgGsmSignal.SetBackgroundColor(new Android.Graphics.Color(ContextCompat.GetColor(activity, Resource.Color.yellow))); // Sedang
                });
            }
            else
            {
                activity.RunOnUiThread(() =>
                {
                    imgGsmSignal.SetBackgroundColor(new Android.Graphics.Color(ContextCompat.GetColor(activity, Resource.Color.red))); // Lemah
                });
            }

        }

        public static string GetDbm(Context context)
        {
            return GetCellularDbm(context);
        }

        private static bool IsConnectedToWifi(Context context)
        {
            ConnectivityManager connectivityManager = (ConnectivityManager)context.GetSystemService(Context.ConnectivityService);
            if (connectivityManager != null)
            {
                NetworkInfo networkInfo = connectivityManager.ActiveNetworkInfo;
                return networkInfo != null && networkInfo.Type == ConnectivityType.Wifi;
            }
            return false;
        }

        private static string GetCellularDbm(Context context)
        {
            TelephonyManager telephonyManager = (TelephonyManager)context.GetSystemService(Context.TelephonyService);
            string strength = null;

            if (ActivityCompat.CheckSelfPermission(context, Android.Manifest.Permission.AccessFineLocation) != Permission.Granted)
            {
                // TODO: Consider calling ActivityCompat#RequestPermissions
                // here to request the missing permissions, and then overriding
                // public void OnRequestPermissionsResult(int requestCode, string[] permissions,
                //                                          Permission[] grantResults)
                // to handle the case where the user grants the permission. See the documentation
                // for ActivityCompat#RequestPermissions for more details.
                return "0";
            }

            if (telephonyManager != null)
            {
                if (telephonyManager.AllCellInfo != null)
                {
                    Log.Debug("CellInfo", "onCellInfo");
                    foreach (CellInfo cellInfo in telephonyManager.AllCellInfo)
                    {
                        if (cellInfo.IsRegistered)
                        {
                            if (cellInfo is CellInfoWcdma wcdmaInfo)
                            {
                                return wcdmaInfo.CellSignalStrength.Dbm.ToString();
                            }
                            else if (cellInfo is CellInfoGsm gsmInfo)
                            {
                                return gsmInfo.CellSignalStrength.Dbm.ToString();
                            }
                            else if (cellInfo is CellInfoLte lteInfo)
                            {
                                return lteInfo.CellSignalStrength.Dbm.ToString();
                            }
                            else if (cellInfo is CellInfoCdma cdmaInfo)
                            {
                                return cdmaInfo.CellSignalStrength.Dbm.ToString();
                            }
                        }
                    }
                }
            }
            return "0";
        }


        public static void setGsmSignalStrength(
            Activity activity,
            double percentage,
            TextView tvGsmSignalLevel,
            ImageView imgGsmSignalLevel
            )
        {
            if (percentage >= 67)
            {
                imgGsmSignalLevel.SetBackgroundColor(new Android.Graphics.Color(ContextCompat.GetColor(activity, Resource.Color.green))); // Sangat Kuat
                tvGsmSignalLevel.Text = "Signal Strength: Strong (" + percentage + "%)";
            }
            else if (percentage >= 34)
            {
                imgGsmSignalLevel.SetBackgroundColor(new Android.Graphics.Color(ContextCompat.GetColor(activity, Resource.Color.yellow))); // Kuat
                tvGsmSignalLevel.Text = "Signal Strength: Moderate (" + percentage + "%)";
            }
            else
            {
                imgGsmSignalLevel.SetBackgroundColor(new Android.Graphics.Color(ContextCompat.GetColor(activity, Resource.Color.red))); // Lemah
                tvGsmSignalLevel.Text = "Signal Strength: Weak (" + percentage + "%)";
            }
        }

    }
}