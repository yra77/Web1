

using System.Windows.Input;


namespace Web1.ViewModels
{
	public class HomePageViewModel : BaseViewModel, INavigatedAware
    {


        public HomePageViewModel(PageDialogService dialogService,
                                 ISemanticScreenReader screenReader,
                                 INavigationService navigationService)
        {
            _dialogService = dialogService;
            _screenReader = screenReader;
            _navigationService = navigationService;
        }


        #region property

        private string _path;
        public string Path
        {
            get => _path;
            set => SetProperty(ref _path, value);
        }


        private ICommand _btnBack;
        public ICommand BtnBack
        {
            get => _btnBack;
            set => SetProperty(ref _btnBack, value);
        }


        private ICommand _btnFwd;
        public ICommand BtnFwd
        {
            get => _btnFwd;
            set => SetProperty(ref _btnFwd, value);
        }


        private ICommand _refreshWebPage;
        public ICommand RefreshWebPage
        {
            get => _refreshWebPage;
            set => SetProperty(ref _refreshWebPage, value);
        }


        private bool _isRefresh;
        public bool IsRefresh
        {
            get => _isRefresh;
            set => SetProperty(ref _isRefresh, value);
        }


        public DelegateCommand BackWebBtn => new DelegateCommand(BackWebClick);
        public DelegateCommand FwdWebBtn => new DelegateCommand(FwdWebClick);
        public DelegateCommand RefreshCommand => new DelegateCommand(RefreshWeb);

        #endregion


        private void RefreshWeb()
        {
            IsRefresh = true;
            RefreshWebPage = new Command((x) => { });
            RefreshWebPage.Execute(this);
            IsRefresh = false;
        }

        private void BackWebClick()
        {
            BtnBack = new Command((x) => { });
            BtnBack.Execute(this);
        }

        private void FwdWebClick()
        {
            BtnFwd = new Command((x) => { });
            BtnFwd.Execute(this);
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            IsRefresh = false;
            if (parameters.ContainsKey("path"))
            {
                Path = parameters.GetValue<string>("path");
            }
        }
    }
}

