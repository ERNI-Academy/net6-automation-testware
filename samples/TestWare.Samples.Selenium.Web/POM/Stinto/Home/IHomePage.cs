using TestWare.Core.Interfaces;

namespace TestWare.Samples.Selenium.Web.POM.Stinto;

public interface IHomePage : ITestWareComponent
{
    void ClickCreateChat();
    void NavigateTo(string url);
}
