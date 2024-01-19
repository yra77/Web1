

using System.Windows.Input;
using Microsoft.Maui.Platform;


namespace Web1.Controls
{
	public class MyWebView : WebView
	{


        public MyWebView()
		{
            Navigated += MyWebView_Navigated;
            Navigating += MyWebView_Navigating;

#if ANDROID
            Web1.MainActivity.goBackEvent += MainActivity_goBackEvent;
#elif IOS || MACCATALYST

#endif
        }


        /// <summary>
        /// Android back button press
        /// </summary>
        private void MainActivity_goBackEvent()
        {
            if (CanGoBack) GoBack();
        }


        #region property

        public static readonly BindableProperty BtnBackProperty =
                             BindableProperty.Create("BtnBack",
                             typeof(ICommand),
                             typeof(MyWebView),
                             propertyChanging: BtnBackClick,
                             defaultBindingMode: BindingMode.TwoWay);

        public ICommand BtnBack
        {
            get { return GetValue(BtnBackProperty) as ICommand; }
            set { SetValue(BtnBackProperty, value); }
        }


        public static readonly BindableProperty BtnFwdProperty =
                             BindableProperty.Create("BtnFwd",
                             typeof(ICommand),
                             typeof(MyWebView),
                             propertyChanging: BtnFwdClick,
                             defaultBindingMode: BindingMode.TwoWay);

        public ICommand BtnFwd
        {
            get { return GetValue(BtnFwdProperty) as ICommand; }
            set { SetValue(BtnFwdProperty, value); }
        }


        public static readonly BindableProperty RefreshProperty =
                             BindableProperty.Create("Refresh",
                             typeof(ICommand),
                             typeof(MyWebView),
                             propertyChanging: RefreshCommand,
                             defaultBindingMode: BindingMode.TwoWay);

        public ICommand Refresh
        {
            get { return GetValue(RefreshProperty) as ICommand; }
            set { SetValue(RefreshProperty, value); }
        }

        #endregion


        /// <summary>
        /// Refresh
        /// </summary>
        /// <param name="bindable"></param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        private static void RefreshCommand(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is MyWebView web)
            {
                // System.Diagnostics.Debug.WriteLine("Play");
                web.Reload();
            }
        }

        /// <summary>
        /// Go back
        /// </summary>
        /// <param name="bindable"></param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        private static void BtnBackClick(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is MyWebView web)
            {
                // System.Diagnostics.Debug.WriteLine("Play");
                if(web.CanGoBack) web.GoBack();
            }
        }

        /// <summary>
        /// Go forward
        /// </summary>
        /// <param name="bindable"></param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        private static void BtnFwdClick(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is MyWebView web)
            {
                // System.Diagnostics.Debug.WriteLine("Play");
                if (web.CanGoForward) web.GoForward();
            }
        }

        /// <summary>
        ///  exit from page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void MyWebView_Navigating(object sender, WebNavigatingEventArgs e)
        {
            //e.Cancel = false;
        }

        /// <summary>
        /// to page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void MyWebView_Navigated(object sender, WebNavigatedEventArgs e)
        {
            
        }
    }
}