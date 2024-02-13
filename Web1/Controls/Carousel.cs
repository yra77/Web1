

namespace Web1.Controls
{
    public class Carousel : CarouselView
    {


        public Carousel()
        {
            Loaded += Carousel_Loaded;
        }


        private void Carousel_Loaded(object sender, EventArgs e)
        {
            Create();
        }

        private void Create()
        {
            ItemsLayout = new LinearItemsLayout(ItemsLayoutOrientation.Horizontal)
            {
                ItemSpacing = 0,
                SnapPointsAlignment = SnapPointsAlignment.Center,
                SnapPointsType = SnapPointsType.None
            };

            ItemTemplate = new DataTemplate(() =>
            {
                StackLayout _stackLayut = new StackLayout()
                {
                    BackgroundColor = Colors.Gray,
                    HorizontalOptions = new LayoutOptions(LayoutAlignment.Center, true),
                    HeightRequest = 75,
                    WidthRequest = 50,
                    Padding = 0
                };

                VisualStateManager.GetVisualStateGroups(_stackLayut).Add(
    new VisualStateGroup()
    {
        Name = "CommonStates",
        States = {
            new VisualState
            {
                Name="CurrentItem",
                Setters=
                {
                    new Setter
                    {
                        Property = ScaleProperty,
                        Value = 2
                    },
                    new Setter
                    {
                        Property = RotationYProperty,
                        Value = 0
                    },
                },
            },
            new VisualState
            {
                Name="PreviousItem",
                Setters=
                {
                    new Setter
                    {
                        Property = ScaleProperty,
                        Value = 1
                    },
                    new Setter
                    {
                        Property = RotationYProperty,
                        Value = 60
                    },
                },
            },
            new VisualState
            {
                Name="NextItem",
                Setters=
                {
                    new Setter
                    {
                        Property = ScaleProperty,
                        Value = 1
                    },
                    new Setter
                    {
                        Property = RotationYProperty,
                        Value = -60
                    },
                },
            },
            //new VisualState
            //{
            //    Name="DefaultItem",
            //    Setters=
            //    {
            //        new Setter
            //        {
            //            Property = ScaleProperty,
            //            Value = 0.85
            //        },
            //        new Setter
            //        {
            //            Property = RotationYProperty,
            //            Value = ((image.AnchorX) < 0.6) ? 60 : -60
            //        },
            //        new Setter
            //        {
            //            Property = MarginProperty,
            //            Value = new Thickness(-20,0,-20,0)
            //        }
            //    }
            //}
        }
    });

                Image image = new Image();
                image.VerticalOptions = new LayoutOptions(LayoutAlignment.Center, true);
                image.SetBinding(Image.SourceProperty, ".");

                _stackLayut.Add(image);
                return _stackLayut;
            });
        }
    }
}