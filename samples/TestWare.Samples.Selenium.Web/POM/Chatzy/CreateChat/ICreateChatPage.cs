using TestWare.Core.Interfaces;

namespace TestWare.Samples.Selenium.Web.POM.CreateChat;

public interface ICreateChatPage : ITestWareComponent
{
    void SetUserId(string userId);
    void SetChatRoomTitle(string chatRoomTitle);
    void ClickSubmitButton();
    string GetChatUrl();
    void NavigateTo(string url);
}
