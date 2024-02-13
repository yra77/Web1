

using Font = Microsoft.Maui.Graphics.Font;


namespace Web1.Controls.CarouselGraphics
{
    public class CarouselDrawable : IDrawable
    {


        #region Property

        internal Action Invalidate { get; set; }

        internal List<string> TextList { get; set; } = new List<string>();
        //  internal List<Microsoft.Maui.Graphics.IImage> Images { get; set; } = new List<Microsoft.Maui.Graphics.IImage>();
        internal Paint BackgroundPaint { get; set; } = Brush.Transparent;
        internal bool IsMoving { get; set; } = false;
        internal int Count { get; set; } = 0;
        internal int StateAnim { get; set; } = 0;// 0 - default, 1 - left, 2 - right

        internal float WidthCard { get; set; } = 0;
        internal float HeightCard { get; set; } = 0;

        private List<PointF> _points = new();
        private List<PointF> _cardsRect = new();
        private List<bool> _isCard;
        private PointF _elipseCenter;
        private PointF _elipseSize;
        private int _position = 0;

        #endregion


        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            // System.Console.WriteLine($"Ooooooooo {dirtyRect.X} {dirtyRect.Y} {dirtyRect.Center.X} {dirtyRect.Center.Y}");

            canvas.StrokeColor = Colors.Red;
            canvas.StrokeSize = 4;
            _elipseSize = new Point(dirtyRect.Width / 2, dirtyRect.Height / 8);
            _elipseCenter = new Point(_elipseSize.X / 2, dirtyRect.Center.Y - _elipseSize.Y);
            canvas.DrawEllipse(_elipseCenter.X, _elipseCenter.Y, _elipseSize.X, _elipseSize.Y);

            if (TextList is null) return;

            if (Count == 0)
            {
                WidthCard = dirtyRect.Width / 4;
                HeightCard = dirtyRect.Height / 12;
                CheckCoord(dirtyRect);
            }

            if (_points is null || _points.Count == 0) return;

            _position = (TextList.Count % 2 == 0) ? TextList.Count / 2 : (TextList.Count - 1) / 2;

          //  if (StateAnim == 1) _position += 1;
          //  if (StateAnim == 2) _position -= 1;

            if (Count == 6)
            {
                if (StateAnim == 2)
                {
                    //include last elem to first
                    var last = TextList[TextList.Count - 1];
                    TextList.Insert(0, last);
                    TextList.RemoveAt(TextList.Count - 1);
                }
                if (StateAnim == 1)
                {
                    //include first elem to last
                    var first = TextList[0];
                    TextList.Add(first);
                    TextList.RemoveAt(0);
                }
            }

            for (int i = 0, j = 0, v = TextList.Count - 1, b = _points.Count - 1; i < _points.Count; i++, b--)
            {
                //j
                if (i < _position && _isCard[i])
                {
                    var res = TranslateTo(_cardsRect[j].X, _cardsRect[j].Y, i, -1, -1, j);
                    CreateField(canvas, TextList[j], new RectF(res.X, res.Y, WidthCard, HeightCard),
                                Colors.LightGray, Colors.Gray, Font.Default, 14, 2);
                    j++;
                }
                //v
                if (b > _position && _isCard[b])
                {
                    var res = TranslateTo(_cardsRect[v].X, _cardsRect[v].Y, -1, b, v, -1);
                    CreateField(canvas, TextList[v], new RectF(res.X, res.Y, WidthCard, HeightCard),
                                Colors.LightGray, Colors.Gray, Font.Default, 14, 2);
                    v--;
                }
                //center - position
                if (i == _points.Count - 1 && _isCard[_position])
                {
                    var res = TranslateTo(_cardsRect[v].X - WidthCard / 4,
                                          _cardsRect[v].Y - HeightCard / 2, i, -1, v, -1);
                    CreateField(canvas, TextList[v], new RectF(res.X, res.Y, WidthCard * 1.5f, HeightCard),
                                Colors.Black, Colors.Black, Font.DefaultBold, 18, 3);
                    v--;
                }
            }

            if (Count == 6)
            {
                IsMoving = false;
                Count = 0;
                StateAnim = 0;
            }
            if (IsMoving && Count > 0)
            {
                Count++;
                MainThread.BeginInvokeOnMainThread(Invalidate);
            }
        }


        private PointF TranslateTo(float x, float y, int i, int b, int v, int j)
        {
            if (StateAnim == 1)
            {
                if (i > 0)
                {
                    x -= Count * ((x - _points[i - 1].X) / 6);
                    y -= Count * ((y - _points[i - 1].Y) / 6);
                }

                if(b > -1)
                {
                    x += Count * ((_points[b - 1].X - x) / 6);
                    y += Count * ((_points[b - 1].Y - y) / 6);
                }

                if (i == 0)
                {
                    x -= Count * ((x - _points[_points.Count - 1].X) / 6);
                    y -= Count * ((y - _points[_points.Count - 1].Y) / 6);
                }
            }
            if (StateAnim == 2)
            {
                if (i < TextList.Count - 1)
                {
                    x = _points[i].X + Count * ((_points[i + 1].X - _points[i].X) / 5);
                    y = _points[i].Y + Count * ((_points[i + 1].Y - _points[i].Y) / 5);
                }
                else if (i == TextList.Count - 1)
                {
                    x = _points[i].X + Count * ((_points[0].X - _points[i].X) / 5);
                    y = _points[i].Y + Count * ((_points[0].Y - _points[i].Y) / 5);
                }
            }
            return new PointF(x, y);
        }

        private void CreateField(ICanvas canvas, string text, RectF rect, Color colorText,
                                 Color colorBorder, IFont font, int textSize, float thinBorder)
        {
            canvas.SetFillPaint(Brush.White, rect);
            canvas.FillRoundedRectangle(rect, 10);

            canvas.StrokeColor = colorBorder;
            canvas.StrokeSize = thinBorder;
            canvas.DrawRoundedRectangle(rect, 10);

            canvas.FontSize = textSize;
            canvas.FontColor = colorText;
            canvas.Font = font;
            canvas.DrawString(text, rect, HorizontalAlignment.Center, VerticalAlignment.Center);
        }

        private void CheckCoord(RectF dirtyRect)
        {

           // _points.Add(new PointF(_elipseCenter.X + WidthCard / 2, _elipseCenter.Y - HeightCard * .5f));
            _points.Add(new PointF(WidthCard * 0.9f, _elipseCenter.Y - HeightCard * .5f));

            //midle left
            _points.Add(new PointF(WidthCard * 0.35f, _elipseCenter.Y - HeightCard * .3f));
           // _points.Add(new PointF(WidthCard * 0.15f, _elipseCenter.Y));
            _points.Add(new PointF(WidthCard * 0.55f, _elipseCenter.Y + HeightCard * .3f));

            //center
            _points.Add(new PointF(_elipseCenter.X + WidthCard / 2, _elipseCenter.Y + HeightCard * 1.15f));

            //midle right
            _points.Add(new PointF(dirtyRect.Width - WidthCard * 1.55f, _elipseCenter.Y + HeightCard * .3f));
           // _points.Add(new PointF(dirtyRect.Width - WidthCard * 1.15f, _elipseCenter.Y));
            _points.Add(new PointF(dirtyRect.Width - WidthCard * 1.35f, _elipseCenter.Y - HeightCard * .3f));

            //last
            _points.Add(new PointF(dirtyRect.Width - WidthCard * 1.95f, _elipseCenter.Y - HeightCard * .5f));

            switch (TextList.Count)
            {
                case 2:
                    _isCard = new List<bool>() { true, false, false, false, false, true, false, false, false, false };
                    break;

                case 3:
                    _isCard = new List<bool>() { false, true, false, false, false, true, false, false, false, true };
                    break;

                case 4:
                    _isCard = new List<bool>() { true, false, false, true, false, true, false, true, false, false };
                    break;

                case 6:
                case 8:
                case 10:
                    _isCard = new List<bool>() { true, false, true, false, true, true, true, false, true, false };
                    break;

                case 5:
                case 7:
                case 9:
                    _isCard = new List<bool>() { true, true, true, true, true, true, true };
                    _cardsRect = new List<PointF>() { _points[0], _points[1], _points[2], _points[3],
                                                      _points[4], _points[5], _points[6], };
                    break;

                default:
                    _isCard = new List<bool>() { false, false, false, false, false, false, false, false, false, false };
                    break;
            }
        }

    }
}