

using Web1.Delegates;


namespace Web1.Services.WinsSlot
{
	public interface IFoundWinLines
	{
        void SearchWinLines(int[] nums, SpinResultCallback _spinResultCallback);
    }
}

