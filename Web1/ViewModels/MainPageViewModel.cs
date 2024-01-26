

using Web1.Services.Auth;


namespace Web1.ViewModels
{
	public class MainPageViewModel : BaseViewModel, INavigatedAware
	{


		public MainPageViewModel(IAuth auth,
                                 PageDialogService dialogService,
                                 ISemanticScreenReader screenReader,
                                 INavigationService navigationService)
		{
            _auth = auth;
            _dialogService = dialogService;
            _screenReader = screenReader;
            _navigationService = navigationService;

            InpPath = "https://learn.microsoft.com/dotnet/maui";
        }


        #region property

        private string _inpPath;
        public string InpPath
        {
            get => _inpPath;
            set => SetProperty(ref _inpPath, value);
        }


        private bool _isValidInput;
        public bool IsValidInput
        {
            get => _isValidInput;
            set => SetProperty(ref _isValidInput, value);
        }


        private string _login;
        public string Login
        {
            get => _login;
            set => SetProperty(ref _login, value);
        }


        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }


        public DelegateCommand OkBtn => new DelegateCommand(OkClick);
        public DelegateCommand GoSignUp_Btn => new DelegateCommand(GoSignUpClick);
        public DelegateCommand<string> AuthWith_Btn => new DelegateCommand<string>(AuthGoogleApple);

        #endregion


        private async void GoSignUpClick()
        {
            await _navigationService.NavigateAsync("SignUpPage");
        }

        private async void AuthGoogleApple(string obj)
        {
            await _navigationService.NavigateAsync("SlotPage");
        }

        private async void OkClick()
        {
            try
            {
                if (IsValidInput)
                {
                    var res = await _auth.AuthAsync(Login, Password);
                    System.Console.WriteLine($"AAAAAAAAA {res.Email} {res.Token}");

                    if (InpPath?.Length > 10)
                    {
                        var parameters = new NavigationParameters { { "path", InpPath } };
                        await _navigationService.NavigateAsync("HomePage", parameters);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"Ssssssss {ex.Message}");
            }
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            
        }
    }
}

