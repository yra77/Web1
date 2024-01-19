

using Web1.Models;


namespace Web1.Controls.Graphic.WinLines
{
    public class LineView : GraphicsView
    {


        private readonly LineDrawable _lineDrawable;


        public LineView() : base()
        {
            _lineDrawable = new LineDrawable();
            _lineDrawable.Invalidate += CanvInvalidate;
            SizeChanged += MyCanvasView_SizeChanged;
            Drawable = _lineDrawable;
        }


        #region Property

        public static readonly BindableProperty ListResultProperty =
            BindableProperty.Create(nameof(ListResult), typeof(List<ResultSpin>), typeof(LineView), null, BindingMode.TwoWay,
                              propertyChanged: (async (bindableObject, oldValue, newValue) =>
                              {
                                  if (newValue != null && bindableObject is LineView lineView)
                                  {
                                      var a = newValue as List<ResultSpin>;
                                      await MainThread.InvokeOnMainThreadAsync(() =>
                                          {
                                              lineView._lineDrawable.ListResult = new List<ResultSpin>(a);
                                              lineView.Invalidate();
                                          });
                                  }
                              }));

        public List<ResultSpin> ListResult
        {
            get => (List<ResultSpin>)GetValue(ListResultProperty);
            set => SetValue(ListResultProperty, value);
        }

        #endregion


        private void MyCanvasView_SizeChanged(object sender, EventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                Invalidate();
            });
        }

        private void CanvInvalidate()
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                Invalidate();
            });
        }
    }
}

