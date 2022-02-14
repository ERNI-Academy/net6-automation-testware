using TestWare.Core.Interfaces;

namespace TestWare.Samples.Selenium.Web.POM.Stinto.CreateChat;

public interface ICreateChatPage : ITestWareComponent
{
    void SetUserId(string userId);
    void AcceptTermsOfUse();
    void ClickSubmitButton();
    string GetChatUrl();
}
