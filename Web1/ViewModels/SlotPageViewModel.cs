

using Web1.Models;
using Web1.Constants;
using Web1.Delegates;
using Web1.Services.WinsSlot;
using Web1.Services.SettingsManager;

using System.ComponentModel;


namespace Web1.ViewModels
{
	public class SlotPageViewModel : BaseViewModel, INavigatedAware
    {


        private SpinResultCallback _spinResultCallback;
        private bool _isAllLines = false;


		public SlotPageViewModel(IFoundWinLines foundWinLines,
                                 PageDialogService dialogService,
                                 ISettingsManager settingsManager,
                                 ISemanticScreenReader screenReader,
                                 INavigationService navigationService)
        {
            _screenReader = screenReader;
            _foundWinLines = foundWinLines;
            _dialogService = dialogService;
            _settingsManager = settingsManager;
            _navigationService = navigationService;

            Start = new int[5] { 1, 1, 1, 1, 1 };
            Delays = Array.Empty<int>();
            Durations = Array.Empty<int>();
            Sum = 0f;
            Lines = 0;
            BetCount = 1;
            IsSettingsVisible = false;
            IsBtnVisible = true;
            IsSumVisible = true;
            IsSumFieldVisible = false;

            _spinResultCallback = SpinResult;
            WinLine = new List<ResultSpin>();
            IsWinLineVisible = false;
        }


        #region Property

        private int[] _start;
        public int[] Start
        {
            get=>_start;
            set=>SetProperty(ref _start, value);
        }


        private int[] _delays;
        public int[] Delays
        {
            get => _delays;
            set => SetProperty(ref _delays, value);
        }


        private int[] _durations;
        public int[] Durations
        {
            get => _durations;
            set => SetProperty(ref _durations, value);
        }


        private float _sum;
        public float Sum
        {
            get => _sum;
            set => SetProperty(ref _sum, value);
        }


        private int _lines;
        public int Lines
        {
            get => _lines;
            set => SetProperty(ref _lines, value);
        }


        private bool _isSettingsVisible;
        public bool IsSettingsVisible
        {
            get => _isSettingsVisible;
            set => SetProperty(ref _isSettingsVisible, value);
        }


        private bool _isBtnVisible;
        public bool IsBtnVisible
        {
            get => _isBtnVisible;
            set => SetProperty(ref _isBtnVisible, value);
        }


        private bool _isSumVisible;
        public bool IsSumVisible
        {
            get => _isSumVisible;
            set => SetProperty(ref _isSumVisible, value);
        }


        private bool _isSumFieldVisible;
        public bool IsSumFieldVisible
        {
            get => _isSumFieldVisible;
            set => SetProperty(ref _isSumFieldVisible, value);
        }


        private bool _isWinLineVisible;
        public bool IsWinLineVisible
        {
            get => _isWinLineVisible;
            set => SetProperty(ref _isWinLineVisible, value);
        }


        private int _isSpinStop;
        public int IsSpinStop
        {
            get => _isSpinStop;
            set => SetProperty(ref _isSpinStop, value);
        }


        private int _changeLine;
        public int ChangeLine
        {
            get => _changeLine;
            set => SetProperty(ref _changeLine, value);
        }


        private int _betCount;
        public int BetCount
        {
            get => _betCount;
            set => SetProperty(ref _betCount, value);
        }


        private List<ResultSpin> _winLine;
        public List<ResultSpin> WinLine
        {
            get => _winLine;
            set => SetProperty(ref _winLine, value);
        }


        private Color _allLinesColor;
        public Color AllLinesColor
        {
            get => _allLinesColor;
            set => SetProperty(ref _allLinesColor, value);
        }


        public DelegateCommand StartBtn => new (StartClick);
        public DelegateCommand SetLinesBtn => new (SetLinesClick);
        public DelegateCommand SetBetBtn => new(SetBetClick);
        public DelegateCommand<string> LinesBtn => new(LinesClick);

        #endregion


        private void LinesClick(string model)
        {
            if (model == "All")
            {
                foreach (var item in SlotConstants.LineName_List)
                {
                    SlotConstants.LineName_List[item.Key] = _isAllLines ? false : true;
                }
                _isAllLines = (_isAllLines) ? false : true;
                AllLinesColor = _isAllLines ? Colors.YellowGreen : Color.Parse("#C8C8C8");//for buttons
                Lines = _isAllLines ? 13 : 0;
            }
            else
            {
                SlotConstants.LineName_List[model] = (SlotConstants.LineName_List[model]) ? false : true;
                if (SlotConstants.LineName_List[model]) Lines += 1;
                else Lines-=1;
            }
            ChangeLine = (ChangeLine == 0) ? 0 : 1;
        }

        private void SetLinesClick()
        {
            if (IsSettingsVisible) { IsSettingsVisible = false; IsBtnVisible = true; }
            else { IsSettingsVisible = true; IsBtnVisible = false; }
        }

        private void SetBetClick()
        {
            if (IsSumFieldVisible) { IsSumFieldVisible = false; IsSettingsVisible = false; IsBtnVisible = true; IsSumVisible = true; }
            else { IsSumFieldVisible = true; IsSettingsVisible = false; IsBtnVisible = false; IsSumVisible = false; }
        }

        private async void StartClick()
        {
            if (Lines == 0) { SetLinesClick(); return; };
            if (!_isPressed)
            {
                if (Sum > Lines * BetCount)
                {
                    _isPressed = true;
                    await MainThread.InvokeOnMainThreadAsync(() =>
                    {
                        IsWinLineVisible = false;
                        WinLine = new();
                    });

                    var i0 = GetRendom(Start[0]);
                    var i1 = GetRendom(Start[1]);
                    var i2 = GetRendom(Start[2]);
                    var i3 = GetRendom(Start[3]);
                    var i4 = GetRendom(Start[4]);

                    Random rd = new();

                    Durations = new int[] { rd.Next(200, 500), rd.Next(200, 500), rd.Next(200, 500), rd.Next(200, 500), rd.Next(200, 500) };
                    Delays = new int[] { rd.Next(0, 50), rd.Next(0, 50), rd.Next(0, 50), rd.Next(0, 50), rd.Next(0, 50) };
                    Start = new int[] { i0, i1, i2, i3, i4 };
                    Sum -= Lines * BetCount;
                }
                else
                {
                    SetBetClick();
                }
            }
        }

        private void SpinResult(List<ResultSpin> resultSpins)
        {
            if(resultSpins.Count > 0)
            {
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    IsWinLineVisible = true;
                    WinLine = new(resultSpins);
                });

                foreach (var item in resultSpins)
                {
                    System.Console.WriteLine($"Oooooo {item.Digit} {item.LineName} {item.Quantity}");
                    int res = 0;
                    switch (item.Quantity)
                    {
                        case 3:
                            res = 5;
                            break;
                        case 4:
                            res = 30;
                            break;
                        case 5:
                            res = 70;
                            break;
                        default:
                            break;
                    }
                    Sum += Lines * BetCount * res;
                }
            }
            _isPressed = false;
        }

        private int GetRendom(int num)
        {
            Random rd = new();
            do
            {
                var res = rd.Next(0, 10);
                if (res != num) return res;
            } while (true);
        }

        protected override async void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);

            switch (args.PropertyName)
            {
                case "IsSpinStop":
                    if(IsSpinStop == 5)
                        await Task.Run(() =>
                        {
                            _foundWinLines.SearchWinLines(Start, _spinResultCallback);
                        });
                    break;

                case "BetCount":
                    if(BetCount > 0)
                    {

                    }
                    break;

                case "Sum":
                    if(Sum > 0)
                    {

                    }
                    break;
                default:
                    break;
            }
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            _isPressed = false;
            IsSpinStop = 0;
            BetCount = 1;
        }
    }
}

