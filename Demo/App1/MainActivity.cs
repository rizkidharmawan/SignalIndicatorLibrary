using Android;
using Android.App;
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
    [Activity(Label = "GPIO Example", Theme = "@style/AppTheme")]
    public class MainActivity : AppCompatActivity
    {

        private Button mButtonRead92;
        private Button mButtonRead88;
        private Button mButtonRead89;
        private Button mButtonRead16;
        private Button mButtonClear;

        private RecyclerView rvReadValue;
        private List<string> stringList;
        private ReadValueGPIOAdapter adapter;
        private string pathRoot = "/sys/class/gpio/gpio1003/";
        private string fileName = "value";

        private const int RequestReadExternalStoragePermission = 1;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.ReadExternalStorage) != Permission.Granted)
            {
                // Jika izin belum diberikan, minta izin secara dinamis
                ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.ReadExternalStorage }, RequestReadExternalStoragePermission);
            }

            mButtonRead92 = FindViewById<Button>(Resource.Id.btnRead);
            mButtonRead92.Click += (sender, e) =>
            {
                pathRoot = "/sys/class/gpio/gpio1003/";
                //pathRoot = "/storage/emulated/0/Download/";
                //fileName = "value.txt";
                string value = ReadFromFile(pathRoot, fileName);
                stringList.Add(value);
                adapter.UpdateList(stringList);
            };

            mButtonRead88 = FindViewById<Button>(Resource.Id.btnRead88);
            mButtonRead88.Click += (sender, e) =>
            {
                pathRoot = "/sys/class/gpio/gpio999/";
                string value = ReadFromFile(pathRoot, fileName);
                stringList.Add(value);
                adapter.UpdateList(stringList);
            };

            mButtonRead89 = FindViewById<Button>(Resource.Id.btnRead89);
            mButtonRead89.Click += (sender, e) =>
            {
                pathRoot = "/sys/class/gpio/gpio1000/";
                string value = ReadFromFile(pathRoot, fileName);
                stringList.Add(value);
                adapter.UpdateList(stringList);
            };

            mButtonRead16 = FindViewById<Button>(Resource.Id.btnRead16);
            mButtonRead16.Click += (sender, e) =>
            {
                pathRoot = "/sys/class/gpio/gpio927/";
                string value = ReadFromFile(pathRoot, fileName);
                stringList.Add(value);
                adapter.UpdateList(stringList);
            };

            mButtonClear = FindViewById<Button>(Resource.Id.btnClear);
            mButtonClear.Click += (sender, e) =>
            {
                stringList = new List<string>();
                adapter.UpdateList(stringList);
            };


            rvReadValue = FindViewById<RecyclerView>(Resource.Id.rvReadValue);

            SetAdapter();
        }

        private void SetAdapter()
        {
            stringList = new List<string>();
            adapter = new ReadValueGPIOAdapter(stringList, this);
            rvReadValue.SetAdapter(adapter);
        }

        public static string ReadFromFile(string pathRoot, string nameFile)
        {
            string aBuffer = "";
            try
            {
                var filePath = Path.Combine(pathRoot, nameFile);

                using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                using (var streamReader = new StreamReader(fileStream))
                {
                    string aDataRow = "";
                    while ((aDataRow = streamReader.ReadLine()) != null)
                    {
                        aBuffer += aDataRow;
                    }
                }
            }
            catch (System.IO.IOException)
            {

            }
            return aBuffer;
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);

        }
    }
}