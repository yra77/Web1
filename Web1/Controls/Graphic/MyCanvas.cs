

namespace Web1.Controls.Graphic
{
	public class MyCanvas : IDrawable
	{


        public Action Invalidate { get; set; }
       // private bool _isPortretOrient;


        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
           // _isPortretOrient = (DeviceDisplay.Current.MainDisplayInfo.Orientation == DisplayOrientation.Portrait);

            foreach (var item in Constants.SlotConstants.LineName_List)
            {
                float y1 = 0, y2 = 0;
                Color color;

                if (item.Key == "Line1") y1 = y2 = 40f;// (_isPortretOrient) ? 40f : 40f;
                if (item.Key == "Line2") y1 = y2 = 120f;// (_isPortretOrient) ? 120f : 120f;
                if (item.Key == "Line3") y1 = y2 = 210f;// (_isPortretOrient) ? 210f : 210f;

                color = (item.Value == true) ? Colors.Red : Colors.Transparent;
                canvas.StrokeSize = 2;
                canvas.StrokeColor = color;
                canvas.StrokeLineJoin = LineJoin.Miter;

                if (item.Key == "Line1" || item.Key == "Line2" || item.Key == "Line3") Line123(canvas, y1, y2, color);
                if (item.Key == "ZUp") Zigzag(canvas, color, true);
                if (item.Key == "ZBottom") Zigzag(canvas, color, false);
                if (item.Key == "VUp") VLine(canvas, color, true);
                if (item.Key == "VBottom") VLine(canvas, color, false);
                if (item.Key == "DiagLeft") Diagonal(canvas, color, true);
                if (item.Key == "DiagRight") Diagonal(canvas, color, false);
                if (item.Key == "GUp") GLine(canvas, color, true);
                if (item.Key == "GBottom") GLine(canvas, color, false);
                if (item.Key == "Up") UpBottom(canvas, color, true);
                if (item.Key == "Bottom") UpBottom(canvas, color, false);
            }
              MainThread.BeginInvokeOnMainThread(Invalidate);
        }


        private void UpBottom(ICanvas canvas, Color color, bool isUp)
        {
            PathF path = new PathF();
            if (isUp)
            {
                path.MoveTo(20, 35);
                path.LineTo(95, 35);
                path.LineTo(160, 115);
                path.LineTo(235, 35);
                path.LineTo(310, 35);
            }
            else
            {
                path.MoveTo(20, 215);
                path.LineTo(95, 215);
                path.LineTo(160, 115);
                path.LineTo(235, 215);
                path.LineTo(310, 215);
            }
            canvas.DrawPath(path);
        }

        private void Diagonal(ICanvas canvas, Color color, bool isUp)
        {
            PathF path = new PathF();
            if (isUp)
            {
                path.MoveTo(20, 30);
                path.LineTo(95, 30);
                path.LineTo(235, 230);
                path.LineTo(310, 230);
            }
            else
            {
                path.MoveTo(20, 230);
                path.LineTo(95, 220);
                path.LineTo(235, 30);
                path.LineTo(310, 30);
            }
            canvas.DrawPath(path);
        }

        private void GLine(ICanvas canvas, Color color, bool isUp)
        {
            PathF path = new PathF();
            if (isUp)
            {
                path.MoveTo(15, 30);
                path.LineTo(95, 110);
                path.LineTo(310, 110);
            }
            else
            {
                path.MoveTo(20, 230);
                path.LineTo(95, 125);
                path.LineTo(310, 125);
            }
            canvas.DrawPath(path);
        }

        private void Line123(ICanvas canvas, float y1, float y2, Color color)
        {
            canvas.DrawLine(0, y1, 330, y2);
        }

        private void Zigzag(ICanvas canvas, Color color, bool isUp)
        {
            PathF path = new PathF();
            if (isUp)
            {
                path.MoveTo(20, 30);
                path.LineTo(95, 120);
                path.LineTo(165, 30);
                path.LineTo(235, 120);
                path.LineTo(310, 30);
            }
            else
            {
                path.MoveTo(20, 230);
                path.LineTo(95, 120);
                path.LineTo(165, 230);
                path.LineTo(235, 120);
                path.LineTo(310, 230);
            }
            canvas.DrawPath(path);
        }

        private void VLine(ICanvas canvas, Color color, bool isUp)
        {
            PathF path = new PathF();
            if (isUp)
            {
                path.MoveTo(15, 30);
                path.LineTo(165, 230);
                path.LineTo(315, 30);
            }
            else
            {
                path.MoveTo(15, 230);
                path.LineTo(165, 30);
                path.LineTo(315, 230);
            }
            canvas.DrawPath(path);
        }
    }
}

