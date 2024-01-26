

namespace Web1_Server.Services.PrintService
{
    public class Print : IPrint
    {


        private readonly ConsoleColor _currentForeground;


        public Print()
        {
            _currentForeground = Console.ForegroundColor;
        }


        public void ErrorPrint(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);

            Console.ForegroundColor = _currentForeground;
        }

        public void NotificationPrint(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);

            Console.ForegroundColor = _currentForeground;
        }
    }
}
