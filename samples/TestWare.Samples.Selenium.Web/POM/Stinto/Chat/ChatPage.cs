using OpenQA.Selenium;
using TestWare.Core.Libraries;
using TestWare.Engines.Selenium.Extras;
using TestWare.Engines.Selenium.Factory;
using TestWare.Engines.Selenium.Pages;

namespace TestWare.Samples.Selenium.Web.POM.Stinto.Chat;

public class ChatPage : WebPage, IChatPage
{
    [FindsBy(How = How.XPath, Using = "//*[contains(@id,'msg')]")]
    private IList<IWebElement> ChatMessages { get; set; }

    [FindsBy(How = How.Id, Using = "prompt")]
    private IWebElement SendMessageInput { get; set; }

    public ChatPage(IBrowserDriver driver) : base(driver)
    {
    }

    public void CheckMessage(string userId, string message)
    {
        RetryPolicies.ExecuteActionWithRetries(
            () =>
                {
                    var messages = ChatMessages.Select(x => x.Text).ToList();
                    messages.Any(x => x == string.Concat(userId, message)).Should().BeTrue();
                },
            numberOfRetries: 10
            );
    }

    public void SendMessage(string message)
        => SendKeysElement(this.SendMessageInput, message + Keys.Enter);
}
