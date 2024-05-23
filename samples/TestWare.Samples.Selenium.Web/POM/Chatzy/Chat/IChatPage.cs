using TestWare.Core.Interfaces;

namespace TestWare.Samples.Selenium.Web.POM.Chat;

public interface IChatPage : ITestWareComponent
{
    void CheckChatMessage(string userId, string message);
    void SendMessage(string message);
}
