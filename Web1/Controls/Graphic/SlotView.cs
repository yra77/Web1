

using Microsoft.Maui.Graphics.Platform;
using IImage = Microsoft.Maui.Graphics.IImage;
using System.Collections;


namespace Web1.Controls.Graphic
{
	public class SlotView : GraphicsView
    {


        #region Property

        public static readonly BindableProperty ImagesProperty =
            BindableProperty.Create(nameof(Images), typeof(IList), typeof(SlotView), default(IList),
                             propertyChanged: async (bindableObject, oldValue, newValue) =>
            {
                if (newValue is String[] images && bindableObject is SlotView slotView)
                {
                    await slotView.LoadImages(images);
                    MainThread.BeginInvokeOnMainThread(slotView.Invalidate);
                }
            });

        public static readonly BindableProperty SpeedProperty =
            BindableProperty.Create(nameof(Speed), typeof(float), typeof(SlotView), 15.0f,
                              propertyChanged: (bindableObject, oldValue, newValue) =>
            {
                if (newValue is float speed && bindableObject is SlotView slotView)
                {
                    slotView.Slot.Speed = speed;
                    MainThread.BeginInvokeOnMainThread(slotView.Invalidate);
                }
            });

        public static readonly BindableProperty MinimumSpeedProperty =
            BindableProperty.Create(nameof(MinimumSpeed), typeof(float), typeof(SlotView), 4.0f,
                              propertyChanged: (bindableObject, oldValue, newValue) =>
            {
                if (newValue is float speed && bindableObject is SlotView slotView)
                {
                    slotView.Slot.MinimumSpeed = speed;
                    MainThread.BeginInvokeOnMainThread(slotView.Invalidate);
                }
            });

        public static readonly BindableProperty DragProperty =
            BindableProperty.Create(nameof(Drag), typeof(float), typeof(SlotView), 0.01f,
                              propertyChanged: (bindableObject, oldValue, newValue) =>
            {
                if (newValue is float drag && bindableObject is SlotView slotView)
                {
                    slotView.Slot.Drag = drag;
                    MainThread.BeginInvokeOnMainThread(slotView.Invalidate);
                }
            });

        public static readonly BindableProperty DragThresholdProperty =
            BindableProperty.Create(nameof(DragThreshold), typeof(int), typeof(SlotView), 3,
                              propertyChanged: (bindableObject, oldValue, newValue) =>
            {
                if (newValue is int dragthreshold && bindableObject is SlotView slotView)
                {
                    slotView.Slot.DragThreshold = dragthreshold;
                    MainThread.BeginInvokeOnMainThread(slotView.Invalidate);
                }
            });

        public static readonly BindableProperty VisibleCountProperty =
            BindableProperty.Create(nameof(VisibleCount), typeof(int), typeof(SlotView), 3,
                              propertyChanged: (bindableObject, oldValue, newValue) =>
            {
                if (newValue is int visibleCount && bindableObject is SlotView slotView)
                {
                    slotView.Slot.VisibleCount = visibleCount;
                    MainThread.BeginInvokeOnMainThread(slotView.Invalidate);
                }
            });

        public static readonly BindableProperty DelayProperty =
            BindableProperty.Create(nameof(Delay), typeof(float), typeof(SlotView), 0.0f,
                              propertyChanged: (bindableObject, oldValue, newValue) =>
            {
                if (newValue is float delay && bindableObject is SlotView slotView)
                {
                    slotView.Slot.Delay = delay;
                    MainThread.BeginInvokeOnMainThread(slotView.Invalidate);
                }
            });

        public static readonly BindableProperty DurationProperty =
            BindableProperty.Create(nameof(Duration), typeof(float), typeof(SlotView), 1000f,
                              propertyChanged: (bindableObject, oldValue, newValue) =>
            {
                if (newValue is float duration && bindableObject is SlotView slotView)
                {
                    slotView.Slot.Duration = duration;
                    MainThread.BeginInvokeOnMainThread(slotView.Invalidate);
                }
            });

        public static readonly BindableProperty StopIndexProperty =
            BindableProperty.Create(nameof(StopIndex), typeof(int), typeof(SlotView), -1,
                              propertyChanged: (bindableObject, oldValue, newValue) =>
            {
                if (newValue is int stopIndex && oldValue is int oldIndex && bindableObject is SlotView slotView)
                {
                    MainThread.BeginInvokeOnMainThread(slotView.Invalidate);
                }
            });

        public static readonly BindableProperty DirectionProperty =
            BindableProperty.Create(nameof(Direction), typeof(SlotDirection), typeof(SlotView),
                              SlotDirection.Down, propertyChanged: (bindableObject, oldValue, newValue) =>
            {
                if (newValue is SlotDirection dir && bindableObject is SlotView slotView)
                {
                    slotView.Slot.Direction = dir;
                    MainThread.BeginInvokeOnMainThread(slotView.Invalidate);
                }
            });

        public static readonly BindableProperty IsSpinningProperty =
            BindableProperty.Create(nameof(IsSpinning), typeof(bool), typeof(SlotView), false, BindingMode.TwoWay,
                              propertyChanged: (async(bindableObject, oldValue, newValue) =>
            {
                if (newValue is bool IsSpinning && oldValue is bool WasSpinning && bindableObject is SlotView slotView)
                {
                    slotView.Slot.IsSpinning = IsSpinning;
                    if (IsSpinning && !WasSpinning)
                    {
                        if (slotView.Delay > 0)
                        {
                                await Task.Delay(TimeSpan.FromMilliseconds(slotView.Delay)).ContinueWith(async(t) =>
                                {
                                    await MainThread.InvokeOnMainThreadAsync(slotView.StartAnimation);
                                });
                        }
                        else
                        {
                            await MainThread.InvokeOnMainThreadAsync(async ()=> await slotView.StartAnimation());
                        }
                    }
                    else if (IsSpinning && WasSpinning)
                    {
                        slotView.PauseAnimation();
                    }
                    await MainThread.InvokeOnMainThreadAsync(slotView.Invalidate);
                }
            }));

        public static readonly new BindableProperty BackgroundColorProperty =
            BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(SlotView), null,
                              propertyChanged: (bindableObject, oldValue, newValue) =>
            {
                if (newValue != null && bindableObject is SlotView slotView)
                {
                    slotView.UpdateBackground();
                }
            });

        public static readonly BindableProperty StartProperty =
            BindableProperty.Create(nameof(Start), typeof(int), typeof(SlotView), null,
                              propertyChanged: (async (bindableObject, oldValue, newValue) =>
                              {
                                  if (newValue != null && bindableObject is SlotView slotView)
                                  {
                                     await slotView.StopAnimation((int)newValue);
                                  }
                              }));

        public static readonly BindableProperty IsStopProperty =
            BindableProperty.Create(nameof(IsStop), typeof(int), typeof(SlotView), 0, BindingMode.TwoWay);


        /// <summary>
        /// Stop if == 5
        /// </summary>
        public int IsStop
        {
            get => (int)GetValue(IsStopProperty);
            set => SetValue(IsStopProperty, value);
        }

        /// <summary>
        /// Start with pass num
        /// </summary>
        public int Start
        {
            get => (int)GetValue(StartProperty);
            set => SetValue(StartProperty, value);
        }

        /// <summary>
        /// Gets or sets the background color of the SlotView.
        /// </summary>
        public new Color BackgroundColor
        {
            get => (Color)GetValue(BackgroundColorProperty);
            set => SetValue(BackgroundColorProperty, value);
        }

        /// <summary>
        /// Gets or sets the array of image paths to display in the SlotView.
        /// Works with both local and remote images.
        /// Works best with square images with equal width and height.
        /// </summary>
        public IList Images
        {
            get => (IList)GetValue(ImagesProperty);
            set => SetValue(ImagesProperty, value);
        }

        /// <summary>
        /// Gets or sets the speed of the image animation in the SlotView.
        /// </summary>
        public float Speed
        {
            get => (float)GetValue(SpeedProperty);
            set => SetValue(SpeedProperty, value);
        }

        /// <summary>
        /// Gets or sets the minimum speed that the animation can slow down to during deceleration.
        /// </summary>
        public float MinimumSpeed
        {
            get => (float)GetValue(MinimumSpeedProperty);
            set => SetValue(MinimumSpeedProperty, value);
        }

        /// <summary>
        /// Gets or sets the deceleration factor when the SlotView is slowing down.
        /// 
        /// </summary>
        public float Drag
        {
            get => (float)GetValue(DragProperty);
            set => SetValue(DragProperty, value);
        }

        /// <summary>
        /// Gets or sets the threshold distance that must be exceeded before the SlotView begins slowing down.
        /// </summary>
        public int DragThreshold
        {
            get => (int)GetValue(DragThresholdProperty);
            set => SetValue(DragThresholdProperty, value);
        }

        /// <summary>
        /// Gets or sets the number of images visible in the SlotView at once.
        /// Works best with an odd number of visible images.
        /// </summary>
        public int VisibleCount
        {
            get => (int)GetValue(VisibleCountProperty);
            set => SetValue(VisibleCountProperty, value);
        }

        /// <summary>
        /// Gets or sets the delay time before the SlotView starts animating.
        /// </summary>
        public float Delay
        {
            get => (float)GetValue(DelayProperty);
            set => SetValue(DelayProperty, value);
        }

        /// <summary>
        /// Gets or sets the duration of the image animation in the SlotView before it slows down.
        /// </summary>
        public float Duration
        {
            get => (float)GetValue(DurationProperty);
            set => SetValue(DurationProperty, value);
        }

        /// <summary>
        /// Gets or sets the index of the image to stop the animation on.
        /// -1 will keep the animation going indefinitely.
        /// </summary>
        public int StopIndex
        {
            get => (int)GetValue(StopIndexProperty);
            set => SetValue(StopIndexProperty, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the SlotView is currently spinning.
        /// </summary>
        public bool IsSpinning
        {
            get => (bool)GetValue(IsSpinningProperty);
            set => SetValue(IsSpinningProperty, value);
        }

        /// <summary>
        /// Gets or sets the direction of the image animation in the SlotView.
        /// </summary>
        public SlotDirection Direction
        {
            get => (SlotDirection)GetValue(DirectionProperty);
            set => SetValue(DirectionProperty, value);
        }

        /// <summary>
        /// Invoked when the images have finished loading.
        /// </summary>
        public event EventHandler ImagesLoaded;

        /// <summary>
        /// Invoked when the image animation has started.
        /// </summary>
        public event EventHandler Started;

        /// <summary>
        /// Invoked when the image animation has been paused.
        /// </summary>
        public event EventHandler Paused;

        /// <summary>
        /// Invoked when the image animation has finished.
        /// </summary>
        public event EventHandler Finished;


        protected SlotDrawable Slot { get; set; }
        private bool _isLoaded;

#endregion


        public SlotView()
        {
            Drawable = Slot = new SlotDrawable();
            Slot.Invalidate += Slot_Invalidate;
            Slot.Finished += Slot_Finished;
            Slot.Speed = Speed;
            Slot.MinimumSpeed = MinimumSpeed;
            Slot.Drag = Drag;
            Slot.DragThreshold = DragThreshold;
            Slot.VisibleCount = VisibleCount;
            Slot.Delay = Delay;
            Slot.Duration = Duration;

            SizeChanged += SlotView_SizeChanged;
            Background = new SolidColorBrush(Colors.Red);
        }


        private void Slot_Finished()
        {
            Finished?.Invoke(this, EventArgs.Empty);
            IsSpinning = false;
            IsStop++;
        }

        private void Slot_Invalidate()
        {
            MainThread.BeginInvokeOnMainThread(Invalidate);
        }

        private void SlotView_SizeChanged(object sender, EventArgs e)
        {
            if (Slot is null) return;
            if (Slot.Width != (float)Width || Slot.Height != (float)Height)
            {
                Slot.Width = (float)Width;
                Slot.Height = (float)Height;
                MainThread.BeginInvokeOnMainThread(Invalidate);
            }
        }

        async Task LoadImages(String[] sources)
        {
            Slot.ImageCount = sources.Length;

            //create sources with indexes
            var indexedSources = new List<(int index, string source)>();
            for (int i = 0; i < sources.Length; i++)
            {
                indexedSources.Add((i, sources[i]));
            }

            var result = new IImage[sources.Length];

            if (Slot.Images is null) Slot.Images = new List<IImage>();
            ParallelOptions parallelOptions = new() { MaxDegreeOfParallelism = 5 };

            await Parallel.ForEachAsync(indexedSources, parallelOptions, async (item, token) =>
            {
                using var stream = await ImageLoading.GetImageStream(item.source, token);
                IImage image = null;

                image = PlatformImage.FromStream(stream);
                result[item.index] = image;
            });

            //check if every item the array is set
            if (!result.Any(x => x is null))
            {
                _isLoaded = true;
                Slot.Images = result.ToList();
                ImagesLoaded?.Invoke(this, EventArgs.Empty);
            }

            MainThread.BeginInvokeOnMainThread(Slot.Invalidate);
        }

        void UpdateBackground()
        {
            if (Slot == null)
                return;

            var background = new SolidPaint { Color = BackgroundColor };
            Slot.BackgroundPaint = background;

            MainThread.BeginInvokeOnMainThread(Slot.Invalidate);
        }

        public async Task StartAnimation()
        {
            if (!_isLoaded) return;
            if (IsSpinning) return;

            IsStop = 0;

            if (Delay > 0)
            {
                await Task.Delay(TimeSpan.FromMilliseconds(Delay)).ContinueWith(async(t) =>
                {
                    Slot.IsSpinning = true;
                    Slot.StopIndex = StopIndex;
                    Started?.Invoke(this, EventArgs.Empty);
                    await MainThread.InvokeOnMainThreadAsync(Invalidate);
                    IsSpinning = true;
                });
            }
            else
            {
                Slot.IsSpinning = true;
                Slot.StopIndex = StopIndex;
                Started?.Invoke(this, EventArgs.Empty);
                MainThread.BeginInvokeOnMainThread(Invalidate);
                IsSpinning = true;
            }
        }

        public void PauseAnimation()
        {
            if (!_isLoaded) return;
            if (IsSpinning != false)
            {
                IsSpinning = false;
                Slot.IsSpinning = false;
                Paused?.Invoke(this, EventArgs.Empty);
            }
        }

        public async Task StopAnimation(int index)
        {
            if (!_isLoaded) return;
            if (index >= Images.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }
            if (Delay > 0)
            {
                await Task.Delay(TimeSpan.FromMilliseconds(Delay)).ContinueWith(async(t) =>
                {
                    StopIndex = index;
                    Slot.StopIndex = index;
                    await MainThread.InvokeOnMainThreadAsync(StartAnimation);
                });
            }
            else
            {
                StopIndex = index;
                Slot.StopIndex = index;
                await MainThread.InvokeOnMainThreadAsync(StartAnimation);
            }
        }
    }

    public enum SlotDirection
    {
        Up,
        Down,
        Left,
        Right
    }
}