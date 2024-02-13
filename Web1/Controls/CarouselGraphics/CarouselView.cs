

using Microsoft.Maui.Graphics.Platform;
using IImage = Microsoft.Maui.Graphics.IImage;
using System.Collections;


namespace Web1.Controls.CarouselGraphics
{
	public class CarouselView : GraphicsView
    {


        public event EventHandler ImagesLoaded;// Invoked when the images have finished loading.

        private CarouselDrawable _carouselDrawable;
        private bool _isLoaded;


        public CarouselView()
        {
            Drawable = _carouselDrawable = new CarouselDrawable();
            _carouselDrawable.Invalidate += View_Invalidate;

            SwipeGestureRecognizer gestureRecognizer = new SwipeGestureRecognizer() { Direction = SwipeDirection.Left };
            gestureRecognizer.Swiped += GestureRecognizer_Swiped;
            GestureRecognizers.Add(gestureRecognizer);
            SwipeGestureRecognizer gestureRecognizer2 = new SwipeGestureRecognizer() { Direction = SwipeDirection.Right };
            gestureRecognizer2.Swiped += GestureRecognizer_Swiped;
            GestureRecognizers.Add(gestureRecognizer2);

            TapGestureRecognizer tapGesture = new TapGestureRecognizer();
            tapGesture.Tapped += TapGesture_Tapped;
            GestureRecognizers.Add(tapGesture);
            //PointerGestureRecognizer
            Background = new SolidColorBrush(Colors.Transparent);

            _carouselDrawable.IsMoving = false;
            _carouselDrawable.StateAnim = 0;
        }


        #region Property

        public static readonly BindableProperty TextListProperty =
                                                BindableProperty.Create("TextList",
                                                typeof(IList),
                                                typeof(CarouselView),
                            propertyChanged: async (bindableObject, oldValue, newValue) =>
                            {
                                if (newValue is String[] texts && bindableObject is CarouselView view)
                                {
                                    view._carouselDrawable.TextList = new List<string>(texts);
                                    view._isLoaded = true;
                                    await MainThread.InvokeOnMainThreadAsync(view.View_Invalidate);
                                }
                            });
        public IList TextList
        {
            get => (IList)GetValue(TextListProperty);
            set => SetValue(TextListProperty, value);
        }


        //public static readonly BindableProperty ImagesProperty =
        //   BindableProperty.Create(nameof(Images), typeof(IList), typeof(CarouselView), default(IList),
        //                    propertyChanged: async (bindableObject, oldValue, newValue) =>
        //                    {
        //                        if (newValue is String[] images && bindableObject is CarouselView view)
        //                        {
        //                            await MainThread.InvokeOnMainThreadAsync(() => view.Load_Images(images));
        //                            await MainThread.InvokeOnMainThreadAsync(view.View_Invalidate);
        //                        }
        //                    });
        //public IList Images
        //{
        //    get => (IList)GetValue(ImagesProperty);
        //    set => SetValue(ImagesProperty, value);
        //}


        public static readonly BindableProperty DelayProperty =
            BindableProperty.Create(nameof(Delay), typeof(int), typeof(CarouselView), 10);
        public int Delay
        {
            get => (int)GetValue(DelayProperty);
            set => SetValue(DelayProperty, value);
        }


        public static readonly new BindableProperty BackgroundColorProperty =
            BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(CarouselView), null,
                              propertyChanged: (bindableObject, oldValue, newValue) =>
                              {
                                  if (newValue is Color color && bindableObject is CarouselView view)
                                  {
                                      view._carouselDrawable.BackgroundPaint = new SolidPaint { Color = color };
                                  }
                              });
        public new Color BackgroundColor
        {
            get => (Color)GetValue(BackgroundColorProperty);
            set => SetValue(BackgroundColorProperty, value);
        }

        #endregion



        private void TapGesture_Tapped(object sender, TappedEventArgs e)
        {
            // Position inside window
            Point? windowPosition = e.GetPosition(null);

            // Position relative to an Image
            Point? relativeToImagePosition = e.GetPosition(this);

            // Position relative to the container view
            Point? relativeToContainerPosition = e.GetPosition((View)sender);

            System.Console.WriteLine($"Ooooooooooo {relativeToImagePosition.Value.X} {relativeToImagePosition.Value.Y}");
        }

        private async void GestureRecognizer_Swiped(object sender, SwipedEventArgs e)
        {
            if (!_isLoaded) return;
            if (_carouselDrawable.IsMoving) return;
           
            if (e.Direction == SwipeDirection.Right)
            {
                _carouselDrawable.StateAnim = 2;
                await StartAnim();
            }
            else if (e.Direction == SwipeDirection.Left)
            {
                _carouselDrawable.StateAnim = 1;
                await StartAnim();
            }
        }

        private void View_Invalidate()
        {
             MainThread.BeginInvokeOnMainThread(Invalidate);
        }

        private async Task StartAnim()
        {
            _carouselDrawable.IsMoving = true;
            _carouselDrawable.Count = 1;

            //do
            //{
            //    if (!_carouselDrawable.IsMoving || _carouselDrawable.Count == 5)
            //    {
            //        _carouselDrawable.IsMoving = false;
            //        break;
            //    }

               await MainThread.InvokeOnMainThreadAsync(View_Invalidate);
            //    _carouselDrawable.Count++;

            //    await Task.Delay(Delay);
            //} while (_carouselDrawable.IsMoving && _carouselDrawable.Count < 5);

            //_carouselDrawable.IsMoving = false;
            //_carouselDrawable.Count = 0;
            //_carouselDrawable.StateAnim = 0;
        }

        private async Task Load_Images(String[] sources)
        {
            //create sources with indexes
            //var indexedSources = new List<(int index, string source)>();
            //for (int i = 0; i < sources.Length; i++)
            //{
            //    indexedSources.Add((i, sources[i]));
            //}

            //var result = new IImage[sources.Length];

            //if (_carouselDrawable.Images is null) _carouselDrawable.Images = new List<IImage>();
            //ParallelOptions parallelOptions = new() { MaxDegreeOfParallelism = 5 };

            //await Parallel.ForEachAsync(indexedSources, parallelOptions, async (item, token) =>
            //{
            //    using var stream = await LoadImages.GetImageStream(item.source, token);
            //    IImage image = null;

            //    image = PlatformImage.FromStream(stream);
            //    result[item.index] = image;
            //});

            ////check if every item the array is set
            //if (!result.Any(x => x is null))
            //{
            //    _isLoaded = true;
            //    _carouselDrawable.Images = result.ToList();
            //    ImagesLoaded?.Invoke(this, EventArgs.Empty);
            //}

           // View_Invalidate();
        }
    }
}