using OpenQA.Selenium;
using TestWare.Core.Libraries;
using TestWare.Engines.Common.Extras;
using TestWare.Engines.Selenium.Factory;
using TestWare.Engines.Selenium.Pages;

namespace TestWare.Samples.Selenium.Web.POM.CreateChat;

public class CreateChatPage : WebPage, ICreateChatPage
{
    [FindsBy(How = How.Id, Using = "X8712")]
    private IWebElement UserIdInput { get; set; }

    [FindsBy(How = How.Id, Using = "X7728")]
    private IWebElement ChatRoomTitleInput { get; set; }

    [FindsBy(How = How.XPath, Using = "//*[@type='submit']")]
    private IWebElement SubmitButton { get; set; }

    public CreateChatPage(IBrowserDriver driver) : base(driver)
    {
    }

    public void SetUserId(string userId)
        => SendKeysElement(this.UserIdInput, userId);

    public void SetChatRoomTitle(string chatRoomTitle)
        => SendKeysElement(this.ChatRoomTitleInput, chatRoomTitle);

    public void ClickSubmitButton()
    {
        var initialUrl = GetChatUrl();
        ClickElement(this.SubmitButton);
        RetryPolicies.ExecuteActionWithRetries(() =>
        {
            this.Driver.Url.Should().NotBe(initialUrl);
        });
    } 

    public string GetChatUrl()
        => this.Driver.Url;

    public void NavigateTo(string url)
        => NavigateToUrl(url);
}

