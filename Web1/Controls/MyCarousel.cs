

using System.Collections.ObjectModel;


namespace Web1.Controls
{
    public class MyCarousel : StackLayout
    {


        private ObservableCollection<StackLayout> _stacksLayout;
        private int _count;


        public MyCarousel()
        {
            _stacksLayout = new ObservableCollection<StackLayout>();
            ItemsList = new ObservableCollection<string>();
            _count = 0;
            Loaded += MyCarousel_Loaded;

            //Swipe
            SwipeGestureRecognizer gestureRecognizer = new SwipeGestureRecognizer() { Direction = SwipeDirection.Left};
            gestureRecognizer.Swiped += GestureRecognizer_Swiped;
            GestureRecognizers.Add(gestureRecognizer);
            SwipeGestureRecognizer gestureRecognizer2 = new SwipeGestureRecognizer() { Direction = SwipeDirection.Right };
            gestureRecognizer2.Swiped += GestureRecognizer_Swiped;
            GestureRecognizers.Add(gestureRecognizer2);
        }


        #region Property

        public static readonly BindableProperty ItemsListProperty =
                                                BindableProperty.Create("ItemsList",
                                                typeof(ObservableCollection<string>),
                                                typeof(MyCarousel));
        public ObservableCollection<string> ItemsList
        {
            get { return GetValue(ItemsListProperty) as ObservableCollection<string>; }
            set { SetValue(ItemsListProperty, value); }
        }


        public static readonly BindableProperty PositionProperty =
                                                BindableProperty.Create("Position",
                                                typeof(int),
                                                typeof(MyCarousel));
        public int Position
        {
            get { return (int)GetValue(PositionProperty); }
            set { SetValue(PositionProperty, value); }
        }

        #endregion


        private void MoveLeft()
        {
            ItemsList.Move(0, _count - 1);
            _stacksLayout = new ObservableCollection<StackLayout>();
            Clear();
            _count = 0;
            foreach (var item in ItemsList)
            {
                CreateCard(item);
                _count++;
            }
            //_stacksLayout[Position].RotationY = 60;
            //_stacksLayout[Position].WidthRequest = 50;
            //_stacksLayout[Position].HeightRequest = 75;

            //if (Position > 0) Position--;
            //else Position = 4;
            //_stacksLayout[Position].RotationY = 0;
            //_stacksLayout[Position].WidthRequest = 100;
            //_stacksLayout[Position].HeightRequest = 150;
        }

        private async void MoveRight()
        {
            for (int i = 0; i < _stacksLayout.Count; i++)
            {
                if (i == 4)
                {
                    _stacksLayout[i].TranslationX = 50;
                    await Task.Delay(20);
                    ItemsList.Move(_count - 1, 0);
                    _stacksLayout = new ObservableCollection<StackLayout>();
                    Clear();
                    _count = 0;
                    foreach (var item in ItemsList)
                    {
                        CreateCard(item);
                        _count++;
                    }
                }
                else
                {
                    if(i == Position)
                    {
                        _stacksLayout[Position].RotationY = 60;
                        _stacksLayout[Position].WidthRequest = 50;
                        _stacksLayout[Position].HeightRequest = 75;
                        _stacksLayout[i].TranslationX = 50;
                        continue;
                    }
                    _stacksLayout[i].TranslationX = 50;
                    //await _stacksLayout[i].TranslateTo(_stacksLayout[i].AnchorX + x, _stacksLayout[i].AnchorY, 10, Easing.Default);

                }
                await Task.Delay(20);
            }
            // ItemsList.Move(_count - 1, 0);
            //  _stacksLayout = new ObservableCollection<StackLayout>();
            //  Clear();
            //  _count = 0;
            //foreach (var item in ItemsList)
            //{
            //    CreateCard(item);
            //    _count++;
            //}

            //_stacksLayout[Position].RotationY = 60;
            //_stacksLayout[Position].WidthRequest = 50;
            //_stacksLayout[Position].HeightRequest = 75;

            //if (Position < 4) Position++;
            //else Position = 0;
            //_stacksLayout[Position].RotationY = 0;
            //_stacksLayout[Position].WidthRequest = 100;
            //_stacksLayout[Position].HeightRequest = 150;
        }

        private void GestureRecognizer_Swiped(object sender, SwipedEventArgs e)
        {
            if (e.Direction == SwipeDirection.Right) MoveRight();
            else if(e.Direction == SwipeDirection.Left) MoveLeft();
        }

        private void MyCarousel_Loaded(object sender, EventArgs e)
        {
            _count = 0;
            foreach (var item in ItemsList)
            {
                CreateCard(item);
                _count++;
            }
        }

        private void CreateCard(string path)
        {
            _stacksLayout.Add(new StackLayout()
            {
                BackgroundColor = Colors.Gray,
                HorizontalOptions = new LayoutOptions(LayoutAlignment.Center, true),
                HeightRequest = (_count == Position)? 150 : 75,
                WidthRequest = (_count == Position) ? 100 : 50,
                Padding = 0
            });

            Image image = new Image();
            image.VerticalOptions = new LayoutOptions(LayoutAlignment.Center, true);
            image.Source = path;

            _stacksLayout[_count].Add(image);

            if(_count != Position)
            {
                _stacksLayout[_count].RotationY = (_count < Position) ? 60 : -60; 
            }

            Add(_stacksLayout[_count]);
        }
    }
}

