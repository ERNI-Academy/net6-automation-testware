using OpenQA.Selenium;
using TestWare.Core.Libraries;
using TestWare.Engines.Common.Extras;
using TestWare.Engines.Selenium.Factory;
using TestWare.Engines.Selenium.Pages;

namespace TestWare.Samples.Selenium.Web.POM.Chat;

public class ChatPage : WebPage, IChatPage
{
    [FindsBy(How = How.ClassName, Using = "b")]
    [FindsBy(How = How.ClassName, Using = "a")]
    private IList<IWebElement> ChatMessages { get; set; }

    [FindsBy(How = How.Id, Using = "X9225")]
    private IWebElement SendMessageInput { get; set; }

    public ChatPage(IBrowserDriver driver) : base(driver)
    {
    }

    public void CheckChatMessage(string userId, string message)
    {
        RetryPolicies.ExecuteActionWithRetries(
            () =>
                {
                    var messages = ChatMessages.Select(x => x.Text).ToList();
                    messages.Any(x => x.Contains(string.Concat(userId, message))).Should().BeTrue("Value '" + string.Concat(userId, message) + "' not found");
                },
            numberOfRetries: 10
            );
    }

    public void SendMessage(string message)
        => SendKeysElement(this.SendMessageInput, message + Keys.Enter);
}
