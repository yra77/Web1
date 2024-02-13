

using System.Collections.ObjectModel;


namespace Web1.ViewModels
{
	public class CarouselPageViewModel : BaseViewModel, INavigatedAware
	{


		public CarouselPageViewModel()
        {
            CarouList = new ObservableCollection<string>() { "slot0img.png", "slot1img.png", "slot2img.png", "slot3img.png", "slot4img.png" };
            Position = 2;
        }


        #region Property

        private ObservableCollection<string> _carouList;
        public ObservableCollection<string> CarouList
        {
            get => _carouList;
            set => SetProperty(ref _carouList, value);
        }


        private int _position;
        public int Position
        {
            get => _position;
            set => SetProperty(ref _position, value);
        }

        #endregion


        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            MainThread.InvokeOnMainThreadAsync(() =>
            {
            });
        }
    }
}

