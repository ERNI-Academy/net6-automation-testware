using OpenQA.Selenium;
using TestWare.Engines.Selenium.Extras;
using TestWare.Engines.Selenium.Pages;

namespace TestWare.Samples.Selenium.Web.POM.Stinto.CreateChat;

public class CreateChatPage : WebPage, ICreateChatPage
{
    [FindsBy(How = How.XPath, Using = "//*[@id='loginNick']")]
    private IWebElement UserIdInput { get; set; }

    [FindsBy(How = How.XPath, Using = "//*[@id='loginTermsOfUse']")]
    private IWebElement LoginTermsOfUseCheckbox { get; set; }

    [FindsBy(How = How.XPath, Using = "//*[@id='loginDialog']/form/div/div/div[3]/button")]
    private IWebElement SubmitButton { get; set; }

    public CreateChatPage(IWebDriver driver) : base(driver)
    {
    }

    public void SetUserId(string userId)
        => SendKeysElement(this.UserIdInput, userId);

    public void AcceptTermsOfUse()
        => ClickElement(this.LoginTermsOfUseCheckbox);

    public void ClickSubmitButton()
        => ClickElement(this.SubmitButton);

    public string GetChatUrl()
        => this.Driver.Url;
}

