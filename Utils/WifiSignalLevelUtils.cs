using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Net;
using Android.Net.Wifi;
using Android.OS;
using Android.Runtime;
using Android.Telephony;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.Core.App;
using AndroidX.Core.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SignalIndicatorsLibrary.Utils
{
    class WifiSignalLevelUtils
    {

        private static string signalType = "";
        public static void get(
            Activity activity,
            ImageView imgWifiSignal
            )
        {

            double signalDbm = double.Parse(GetDbm(activity));

            if (signalDbm >= -70)
            {
                activity.RunOnUiThread(() =>
                {
                    imgWifiSignal.SetImageDrawable(ContextCompat.GetDrawable(activity, Resource.Drawable.ic_signal_bar_full)); // Sangat Kuat
                });
            }
            else if (signalDbm >= -85)
            {
                activity.RunOnUiThread(() =>
                {
                    imgWifiSignal.SetImageDrawable(ContextCompat.GetDrawable(activity, Resource.Drawable.ic_signal_3_bar)); // Kuat
                });
            }
            else if (signalDbm >= -100)
            {
                activity.RunOnUiThread(() =>
                {
                    imgWifiSignal.SetImageDrawable(ContextCompat.GetDrawable(activity, Resource.Drawable.ic_signal_2_bar)); // Sedang
                });
            }
            else
            {
                activity.RunOnUiThread(() =>
                {
                    imgWifiSignal.SetImageDrawable(ContextCompat.GetDrawable(activity, Resource.Drawable.ic_signal_1_bar)); // Lemah
                });
            }

        }

        public static string GetDbm(Context context)
        {
            return GetWifiDbm(context);
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

        private static string GetWifiDbm(Context context)
        {
            WifiManager wifiManager = (WifiManager)context.GetSystemService(Context.WifiService);
            if (wifiManager != null)
            {
                WifiInfo wifiInfo = wifiManager.ConnectionInfo;
                Log.Debug("WifiInfo", wifiInfo.Rssi.ToString());
                return wifiInfo.Rssi.ToString();
            }
            return "0";
        }

        public static void setWifiSignalStrength(
            Activity activity,
            double signalDbm,
            TextView tvSignalLevel,
            ImageView imgSignalLevel
            )
        {
            if (signalDbm >= -70)
            {
                imgSignalLevel.SetImageDrawable(ContextCompat.GetDrawable(activity, Resource.Drawable.ic_signal_bar_full)); // Sangat Kuat
                tvSignalLevel.Text = "Signal Strength: Very Strong (" + signalDbm + ")";
            }
            else if (signalDbm >= -85)
            {
                imgSignalLevel.SetImageDrawable(ContextCompat.GetDrawable(activity, Resource.Drawable.ic_signal_3_bar)); // Kuat
                tvSignalLevel.Text = "Signal Strength: Strong (" + signalDbm + ")";
            }
            else if (signalDbm >= -100)
            {
                imgSignalLevel.SetImageDrawable(ContextCompat.GetDrawable(activity, Resource.Drawable.ic_signal_2_bar)); // Sedang
                tvSignalLevel.Text = "Signal Strength: Moderate (" + signalDbm + ")";
            }
            else
            {
                imgSignalLevel.SetImageDrawable(ContextCompat.GetDrawable(activity, Resource.Drawable.ic_signal_1_bar)); // Lemah
                tvSignalLevel.Text = "Signal Strength: Weak (" + signalDbm + ")";
            }
        }
    }
}