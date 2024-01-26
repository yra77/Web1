

namespace Web1_Server.Services.PrintService
{
    public interface IPrint
    {
        void ErrorPrint(string message);
        void NotificationPrint(string message);
    }
}
