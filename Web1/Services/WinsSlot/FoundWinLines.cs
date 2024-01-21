

using Web1.Delegates;
using Web1.Models;


namespace Web1.Services.WinsSlot
{
    public class FoundWinLines : IFoundWinLines
    {


        private int[] arr = new int[15];
//index images
        // 0, 3, 6, 9, 12
        // 1, 4, 7, 10, 13
        // 2, 5, 8, 11, 14

        public FoundWinLines()
        {
        }


        public void SearchWinLines(int[] nums, SpinResultCallback _spinResultCallback)
        {
            GetArr(nums);

            List<ResultSpin> results = new List<ResultSpin>();

            foreach (var item in Constants.SlotConstants.LineName_List)
            {
                int a = 0;
                ResultSpin result = new ResultSpin();

                if (item.Value)
                {
                    switch (item.Key)
                    {
                        case "Line1":
                            if (arr[0] == arr[3] && arr[6] == arr[3])
                            {
                                a = 3;
                                if (arr[9] == arr[6])
                                {
                                    a = 4;
                                    if (arr[12] == arr[9])
                                    {
                                        a = 5;
                                    }
                                }
                                result.Digit = arr[0];
                                result.LineName = 0;
                                result.Quantity = a;
                                results.Add(result);
                            }
                            break;

                        case "Line2":
                            if (arr[1] == arr[4] && arr[7] == arr[4])
                            {
                                a = 3;
                                if (arr[10] == arr[7])
                                {
                                    a = 4;
                                    if (arr[13] == arr[10])
                                    {
                                        a = 5;
                                    }
                                }
                                result.Digit = arr[1];
                                result.LineName = 1;
                                result.Quantity = a;
                                results.Add(result);
                            }
                            break;

                        case "Line3":
                            if (arr[2] == arr[5] && arr[8] == arr[5])
                            {
                                a = 3;
                                if (arr[11] == arr[8])
                                {
                                    a = 4;
                                    if (arr[14] == arr[11])
                                    {
                                        a = 5;
                                    }
                                }
                                result.Digit = arr[2];
                                result.LineName = 2;
                                result.Quantity = a;
                                results.Add(result);
                            }
                            break;

                        case "ZUp":
                            if (arr[0] == arr[4] && arr[6] == arr[4])
                            {
                                a = 3;
                                if (arr[10] == arr[6])
                                {
                                    a = 4;
                                    if (arr[12] == arr[10])
                                    {
                                        a = 5;
                                    }
                                }
                                result.Digit = arr[0];
                                result.LineName = 3;
                                result.Quantity = a;
                                results.Add(result);
                            }
                            break;

                        case "ZBottom":
                            if (arr[2] == arr[4] && arr[8] == arr[4])
                            {
                                a = 3;
                                if (arr[10] == arr[8])
                                {
                                    a = 4;
                                    if (arr[14] == arr[10])
                                    {
                                        a = 5;
                                    }
                                }
                                result.Digit = arr[2];
                                result.LineName = 4;
                                result.Quantity = a;
                                results.Add(result);
                            }
                            break;

                        case "VUp":
                            if (arr[0] == arr[4] && arr[8] == arr[4])
                            {
                                a = 3;
                                if (arr[10] == arr[8])
                                {
                                    a = 4;
                                    if (arr[12] == arr[10])
                                    {
                                        a = 5;
                                    }
                                }
                                result.Digit = arr[0];
                                result.LineName = 5;
                                result.Quantity = a;
                                results.Add(result);
                            }
                            break;

                        case "VBottom":
                            if (arr[2] == arr[4] && arr[6] == arr[4])
                            {
                                a = 3;
                                if (arr[10] == arr[6])
                                {
                                    a = 4;
                                    if (arr[14] == arr[10])
                                    {
                                        a = 5;
                                    }
                                }
                                result.Digit = arr[2];
                                result.LineName = 6;
                                result.Quantity = a;
                                results.Add(result);
                            }
                            break;

                        case "DiagLeft":
                            if (arr[2] == arr[5] && arr[7] == arr[5])
                            {
                                a = 3;
                                if (arr[9] == arr[7])
                                {
                                    a = 4;
                                    if (arr[12] == arr[9])
                                    {
                                        a = 5;
                                    }
                                }
                                result.Digit = arr[2];
                                result.LineName = 7;
                                result.Quantity = a;
                                results.Add(result);
                            }
                            break;

                        case "DiagRight":
                            if (arr[0] == arr[3] && arr[7] == arr[3])
                            {
                                a = 3;
                                if (arr[11] == arr[7])
                                {
                                    a = 4;
                                    if (arr[14] == arr[11])
                                    {
                                        a = 5;
                                    }
                                }
                                result.Digit = arr[0];
                                result.LineName = 8;
                                result.Quantity = a;
                                results.Add(result);
                            }
                            break;

                        case "GUp":
                            if (arr[0] == arr[4] && arr[7] == arr[4])
                            {
                                a = 3;
                                if (arr[10] == arr[7])
                                {
                                    a = 4;
                                    if (arr[13] == arr[10])
                                    {
                                        a = 5;
                                    }
                                }
                                result.Digit = arr[0];
                                result.LineName = 9;
                                result.Quantity = a;
                                results.Add(result);
                            }
                            break;

                        case "GBottom":
                            if (arr[2] == arr[4] && arr[7] == arr[4])
                            {
                                a = 3;
                                if (arr[10] == arr[7])
                                {
                                    a = 4;
                                    if (arr[13] == arr[10])
                                    {
                                        a = 5;
                                    }
                                }
                                result.Digit = arr[2];
                                result.LineName = 10;
                                result.Quantity = a;
                                results.Add(result);
                            }
                            break;

                        case "Up":
                            if (arr[0] == arr[3] && arr[7] == arr[3])
                            {
                                a = 3;
                                if (arr[9] == arr[7])
                                {
                                    a = 4;
                                    if (arr[12] == arr[9])
                                    {
                                        a = 5;
                                    }
                                }
                                result.Digit = arr[0];
                                result.LineName = 11;
                                result.Quantity = a;
                                results.Add(result);
                            }
                            break;

                        case "Bottom":
                            if (arr[2] == arr[5] && arr[7] == arr[5])
                            {
                                a = 3;
                                if (arr[11] == arr[7])
                                {
                                    a = 4;
                                    if (arr[14] == arr[11])
                                    {
                                        a = 5;
                                    }
                                }
                                result.Digit = arr[2];
                                result.LineName = 12;
                                result.Quantity = a;
                                results.Add(result);
                            }
                            break;
                    }
                }
            }
                _spinResultCallback(results);
        }

        private void GetArr(int[] nums)
        {
            for (int i = 0, j = 1; i < nums.Length; j += 3, i++)
            {
                arr[j - 1] = (nums[i] > 0) ? nums[i] - 1 : 9;
                arr[j] = nums[i];
                arr[j + 1] = (nums[i] < 9) ? nums[i] + 1 : 0;
                //  System.Console.WriteLine(arr[j - 1] + " " + arr[j] + " " + arr[j + 1]);
            }
        }
    }
}

