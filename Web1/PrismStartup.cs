


using Web1.Views;
using Web1.ViewModels;
using Web1.Services.Auth;
using Web1.Services.WinsSlot;
using Web1.Services.Repository;
using Web1.Services.SettingsManager;


namespace Web1
{
	public static class PrismStartup
    {
        public static void Configure(PrismAppBuilder builder)
        {
            builder.RegisterTypes(RegisterTypes)
                .OnInitialized(OnInitialized)
                    .OnAppStart("NavigationPage/MainPage");
        }

        private static void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>("MainPage")
                             .RegisterForNavigation<HomePage, HomePageViewModel>("HomePage")
                             .RegisterForNavigation<SlotPage, SlotPageViewModel>("SlotPage")
                             .RegisterForNavigation<SignUpPage, SignUpPageViewModel>("SignUpPage")
                             .RegisterForNavigation<CarouselPage, CarouselPageViewModel>("CarouselPage")
                             .RegisterInstance(SemanticScreenReader.Default);

            //Services
            containerRegistry.RegisterSingleton<ISettingsManager, SettingsManager>()
                             .RegisterSingleton<IFoundWinLines, FoundWinLines>()
                             .RegisterSingleton<IRepository, Repository>()
                             .RegisterSingleton<IAuth, Auth>();
        }

        private static void OnInitialized(IContainerProvider container)
        {
            ViewModelLocationProvider.SetDefaultViewModelFactory((view, type) => container.Resolve(type));
        }
    }
}

