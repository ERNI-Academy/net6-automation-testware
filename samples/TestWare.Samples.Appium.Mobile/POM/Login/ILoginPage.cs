using TestWare.Core.Interfaces;

namespace TestWare.Samples.Appium.Mobile.POM.Login;

public interface ILoginPage : ITestWareComponent
{
    void SendUsername(string userName);
    void SendPassword(string password);
    void ClickLoginButton();
}
