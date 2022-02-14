using TestWare.Core.Interfaces;

namespace TestWare.Samples.Selenium.Web.POM.Menu;

/// <summary>
/// Initializes a new instance of the <see cref="IMenuPage"/> interface class that contains
/// all the methods that can be used at Steps level.
/// </summary>
public interface IMenuPage : ITestWareComponent
{
    void ClickLogoutButton();
    void ClickOpenMenuButton();
}
