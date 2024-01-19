

using Web1.Models;


namespace Web1.Controls.Graphic.WinLines
{
    public class LineDrawable : IDrawable
    {


        public Action Invalidate { get; set; }
        public List<ResultSpin> ListResult { get; set; } = new List<ResultSpin>();


        public async void Draw(ICanvas canvas, RectF dirtyRect)
        {
            if (ListResult != null && ListResult.Count > 0)
            {
                foreach (var item in ListResult)
                {
                    float y1 = 0, y2 = 0;
                    Color color;

                    if (item.LineName == 0) y1 = y2 = 40f;
                    if (item.LineName == 1) y1 = y2 = 120f;
                    if (item.LineName == 2) y1 = y2 = 210f;

                    color = Colors.Green;// (!IsView) ? Colors.Green : Colors.Transparent;
                    canvas.StrokeSize = 4;
                    canvas.StrokeColor = color;
                    canvas.StrokeLineJoin = LineJoin.Miter;

                    if (item.LineName == 2 || item.LineName == 1 || item.LineName == 0) Line123(canvas, y1, y2, color);
                    if (item.LineName == 3) Zigzag(canvas, color, true);
                    if (item.LineName == 4) Zigzag(canvas, color, false);
                    if (item.LineName == 5) VLine(canvas, color, true);
                    if (item.LineName == 6) VLine(canvas, color, false);
                    if (item.LineName == 8) Diagonal(canvas, color, true);
                    if (item.LineName == 7) Diagonal(canvas, color, false);
                    if (item.LineName == 9) GLine(canvas, color, true);
                    if (item.LineName == 10) GLine(canvas, color, false);
                    if (item.LineName == 11) UpBottom(canvas, color, true);
                    if (item.LineName == 12) UpBottom(canvas, color, false);
                }
            }

            await MainThread.InvokeOnMainThreadAsync(Invalidate.Invoke);
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

