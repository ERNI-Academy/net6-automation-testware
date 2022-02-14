using TestWare.Core.Interfaces;

namespace TestWare.Samples.Selenium.Web.POM.Stinto.Chat;

public interface IChatPage : ITestWareComponent
{
    void CheckMessage(string userId, string message);
    void SendMessage(string message);
}
