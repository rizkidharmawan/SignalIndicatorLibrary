# SignalIndicatorsLibrary

Library ini menyediakan komponen indikator sinyal untuk digunakan dalam aplikasi Xamarin.Forms. Komponen ini memungkinkan Anda untuk menampilkan indikator sinyal untuk GSM, WiFi, dan GPS.

## Instalasi

Anda dapat menginstal library ini dengan menambahkan `SignalIndicatorsLibrary` sebagai existing project ke dalam project anda.

## Penggunaan

### Indikator Sinyal GSM

Untuk menggunakan indikator sinyal GSM, tambahkan class `GsmSignalIndicator` pada method `OnCreate`. Anda dapat menetapkan interval waktu dalam menerima kekuatan sinyal dengan memanggil metode `setSchedule` pada instance  `GsmSignalIndicator`.

Contoh penggunaan:

```csharp
imgGsmSignal = FindViewById<ImageView>(Resource.Id.imgGsmSignal); // inisialisasi ImageView yang akan digunakan untuk menampilkan indikator sinyal GSM

GsmSignalIndicator gsmSignalIndicator = new GsmSignalIndicator();
gsmSignalIndicator.init(this, imgGsmSignal);  
gsmSignalIndicator.setSchedule(3000); // atur scheduller dalam milliseconds
```

### Indikator Sinyal WIFI

Untuk menggunakan indikator sinyal WiFi, tambahkan class `WifiSignalIndicator` pada method `OnCreate`. Anda dapat menetapkan interval waktu dalam menerima kekuatan sinyal dengan memanggil metode `setSchedule` pada instance  `WifiSignalIndicator`.

Contoh penggunaan:

```csharp
imgWifiSignal = FindViewById<ImageView>(Resource.Id.imgWifiSignal); // inisialisasi ImageView yang akan digunakan untuk menampilkan indikator sinyal WIFI

WifiSignalIndicator wifiSignalIndicator = new WifiSignalIndicator();
wifiSignalIndicator.init(this, imgWifiSignal);
wifiSignalIndicator.setSchedule(3000); // atur scheduller dalam milliseconds
```

### Indikator Sinyal GPS

Untuk menggunakan indikator sinyal GPS, tambahkan class `GpsSignalIndicator` pada method `OnCreate`. Anda dapat menerima kekuatan sinyal dengan memanggil metode `setSchedule` pada instance  `WifiSignalIndicator`.

Contoh penggunaan:

```csharp
imgGpsSignal = FindViewById<ImageView>(Resource.Id.imgGpsSignal); // inisialisasi ImageView yang akan digunakan untuk menampilkan indikator sinyal GPS

locationManager = GetSystemService(LocationService) as LocationManager; // panggil locationManager

GpsSignalIndicator gpsSignalIndicator = new GpsSignalIndicator();
gpsSignalIndicator.init(this, imgGpsSignal, locationManager, this, this);
```

Jangan lupa untuk menambahkan implementasi interface `GpsStatus.IListener` pada Activity

```csharp
public class SignalBarActivity : Activity, GpsStatus.IListener
    {
        //existing code...
    }
```

lalu implementasi method `OnGpsStatusChanged` dari interface tersebut dan panggil method `setGpsStatusChanged` pada instance `gpsSignalIndicator`.

```csharp
public class SignalBarActivity : Activity, GpsStatus.IListener
    {
        //existing code...
        public void OnGpsStatusChanged(GpsEvent gpsEvent)
        {
            gpsSignalIndicator.setGpsStatusChanged();
        }
    }
```

#### Penanganan Reuest Permission

untuk menerima hasil dari request permission, implementasi kode berikut pada `OnRequestPermissionsResult`

```csharp
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
```
