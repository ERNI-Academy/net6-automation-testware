using TestWare.Core.Interfaces;

namespace TestWare.Samples.Appium.Mobile.POM.Menu;

public interface IMenuPage : ITestWareComponent
{
    void CheckMenuIsVisible();

    void OpenMenu();

    void ClickLogoutButton();
}
