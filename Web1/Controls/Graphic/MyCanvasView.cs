

namespace Web1.Controls.Graphic
{
	public class MyCanvasView : GraphicsView
	{


		private readonly MyCanvas _myCanvas;


		public MyCanvasView():base()
		{
			_myCanvas = new MyCanvas();
			_myCanvas.Invalidate += CanvInvalidate;
            SizeChanged += MyCanvasView_SizeChanged;
			Drawable = _myCanvas;
			//Invalidate();
		}


        #region Property

        public static readonly BindableProperty ChangeProperty =
            BindableProperty.Create(nameof(Change), typeof(int), typeof(MyCanvasView), null, BindingMode.TwoWay,
                              propertyChanged: ((bindableObject, oldValue, newValue) =>
                              {
                                  if (newValue != null && bindableObject is MyCanvasView slotView)
                                  {
                                      slotView.Invalidate();
                                  }
                              }));

        public int Change
        {
            get => (int)GetValue(ChangeProperty);
            set => SetValue(ChangeProperty, value);
        }

        #endregion


        private void MyCanvasView_SizeChanged(object sender, EventArgs e)
        {
           // System.Console.WriteLine(Width + " " + Height);
            Invalidate();
        }

        private void CanvInvalidate()
        {
            Invalidate();
        }
    }
}