

using Web1.Services.Auth;
using Web1.Services.WinsSlot;
using Web1.Services.SettingsManager;


namespace Web1.ViewModels
{
	public class BaseViewModel : BindableBase
    {


        protected bool _isPressed;

        protected IAuth _auth;
        protected IFoundWinLines _foundWinLines;
        protected IPageDialogService _dialogService;
        protected ISettingsManager _settingsManager;
        protected ISemanticScreenReader _screenReader;
        protected INavigationService _navigationService;


        public BaseViewModel()
		{
		}
	}
}

