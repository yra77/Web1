

namespace Web1.Services.SettingsManager
{
	public class SettingsManager : ISettingsManager
	{
        public int Sum
        {
            get => Preferences.Get(nameof(Sum), 0);
            set => Preferences.Set(nameof(Sum), value);
        }

        public int Lines
        {
            get => Preferences.Get(nameof(Lines), 0);
            set => Preferences.Set(nameof(Lines), value);
        }

        public int Bet//stavka
        {
            get => Preferences.Get(nameof(Bet), 0);
            set => Preferences.Set(nameof(Bet), value);
        }
    }
}

