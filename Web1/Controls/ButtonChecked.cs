

namespace Web1.Controls
{
    public class ButtonChecked : Button
    {


        private bool _isPressed = false;


        public ButtonChecked() : base()
        {
            Pressed += MyButton_Pressed;
            BackgroundColor = Color.Parse("#C8C8C8");
            BorderColor = Colors.Black;
            BorderWidth = 1;
        }


        #region Property

        public static readonly BindableProperty AllLinesColorProperty =
            BindableProperty.Create(nameof(AllLinesColor), typeof(Color), typeof(ButtonChecked), null,
                              propertyChanged: ((bindableObject, oldValue, newValue) =>
                              {
                                  if (newValue != null && bindableObject is ButtonChecked btn)
                                  {
                                      var a = newValue as Color;
                                          MainThread.BeginInvokeOnMainThread(() =>
                                          {
                                              btn.BackgroundColor = a;
                                              btn._isPressed = btn._isPressed ? false : true;
                                          });
                                  }
                              }));
        public Color AllLinesColor
        {
            get => (Color)GetValue(AllLinesColorProperty);
            set => SetValue(AllLinesColorProperty, value);
        }

        #endregion


        private void MyButton_Pressed(object sender, EventArgs e)
        {
            if (BackgroundColor != Colors.YellowGreen)
            {
                this.BackgroundColor = Colors.YellowGreen;
                _isPressed = true;
            }
            else
            {
                this.BackgroundColor = Color.Parse("#C8C8C8");
                _isPressed = false;
            }
        }
    }
}

