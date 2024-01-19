

using Web1.Delegates;

using Android.OS;
using Android.App;
using Android.Views;
using Android.Widget;
using Android.Content.PM;


namespace Web1
{

    [Activity(Theme = "@style/Maui.SplashTheme",
              MainLauncher = true,
              ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation
              | ConfigChanges.UiMode | ConfigChanges.ScreenLayout
              | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {


        public static MainActivity Inst { get; set; }

        public static event GoBackDelegate goBackEvent;

        private long _milliSeconds = 0;


        protected override void OnCreate(Android.OS.Bundle bundle)
        {
            base.OnCreate(bundle);

            Inst = this;

            if (Build.VERSION.SdkInt >= BuildVersionCodes.OMr1)
            {
                SetShowWhenLocked(true);
            }
            else
            {
                Window.AddFlags(Android.Views.WindowManagerFlags.KeepScreenOn | Android.Views.WindowManagerFlags.TurnScreenOn | Android.Views.WindowManagerFlags.DismissKeyguard | Android.Views.WindowManagerFlags.ShowWhenLocked);
            }

            Window.SetSoftInputMode(SoftInput.AdjustNothing);

            if (Build.VERSION.SdkInt >= BuildVersionCodes.R)
            {
                Window.InsetsController?.SetSystemBarsAppearance(
                              (int)WindowInsetsControllerAppearance.LightStatusBars,
                              (int)WindowInsetsControllerAppearance.LightStatusBars);
            }
            else
            {
                Window.DecorView.SystemUiVisibility =
                              (StatusBarVisibility)SystemUiFlags.LightStatusBar;
            }

            Platform.Init(this, bundle);

        //    await Permissions.RequestAsync<Permissions.StorageWrite>();
        //    await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
        //    await Permissions.RequestAsync<Permissions.Microphone>();
        //    await Permissions.RequestAsync<Permissions.Camera>();

        //    const int requestNotification = 0;
        //    string[] notiPermission =
        //      {
        //        Manifest.Permission.PostNotifications
        //      };

        //    if ((int)Build.VERSION.SdkInt > 32)
        //    {
        //        if (this.CheckSelfPermission(Manifest.Permission.PostNotifications) != Permission.Granted)
        //        {
        //            this.RequestPermissions(notiPermission, requestNotification);
        //        }
        //    }
        //}

        //public override void OnRequestPermissionsResult(int requestCode,
        //                                                      string[] permissions,
        //                                                      Permission[] grantResults)
        //{
        //    Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

        //    base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public override void OnBackPressed()
        {
            var navigation = Microsoft.Maui.Controls.Application.Current?.MainPage?.Navigation;

            if (navigation is null ||
                navigation.NavigationStack.Count > 1 ||
                navigation.ModalStack.Count > 0)
            {
                goBackEvent.Invoke();
               // base.OnBackPressed();
            }
            // else
            // { 
            const int delay = 1000;
            if (_milliSeconds + delay > DateTimeOffset.UtcNow.ToUnixTimeMilliseconds())
            {
                FinishAndRemoveTask();
                Process.KillProcess(Process.MyPid());
            }
            else
            {
                //change status bar color
                MainActivity.Inst.Window.SetStatusBarColor(Android.Graphics.Color.ParseColor("#F8F9FA"));
                if (Build.VERSION.SdkInt >= BuildVersionCodes.R)
                {
                    MainActivity.Inst.Window.InsetsController?.
                        SetSystemBarsAppearance((int)WindowInsetsControllerAppearance.LightStatusBars,
                        (int)WindowInsetsControllerAppearance.LightStatusBars);
                }
                else
                {
                    MainActivity.Inst.Window.DecorView.SystemUiVisibility = (StatusBarVisibility)SystemUiFlags.LightStatusBar;
                }

                //view message
                Toast toast = Toast.MakeText(this, "Натисніть ще раз, щоб вийти", ToastLength.Short);
                toast.SetGravity(GravityFlags.Center, 0, 0);
                toast.Show();

                _milliSeconds = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            }
            // }
        }
    }
}

