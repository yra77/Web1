

using Web1.Models;
using Web1.Services.Auth;


namespace Web1.ViewModels
{
	public class SignUpPageViewModel : BaseViewModel, INavigatedAware
    {


		public SignUpPageViewModel(IAuth auth,
                                   PageDialogService dialogService,
                                   ISemanticScreenReader screenReader,
                                   INavigationService navigationService)
        {
            _auth = auth;
            _dialogService = dialogService;
            _screenReader = screenReader;
            _navigationService = navigationService;
        }


        #region Property


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


        public DelegateCommand OkBtn => new DelegateCommand(SignUpClick);
        public DelegateCommand GoBack_Btn => new DelegateCommand(GoBackClick);
        public DelegateCommand<string> AuthWith_Btn => new DelegateCommand<string>(AuthGoogleApple);

        #endregion


        private async void GoBackClick()
        {
            await _navigationService.NavigateAsync("MainPage");
        }

        private void AuthGoogleApple(string obj)
        {
            System.Console.WriteLine($"Ppppppppppp {obj}");
        }

        private async void SignUpClick()
        {
            RegisterModel registerModel = new() { Email = Login, Password = Password };
            try
            {
                if (IsValidInput)
                {
                    var res = await _auth.InsertAsync(registerModel);
                    System.Console.WriteLine($"AAAAAAAAA {res.Email} {res.Token}");
                    await _navigationService.NavigateAsync("MainPage");
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

