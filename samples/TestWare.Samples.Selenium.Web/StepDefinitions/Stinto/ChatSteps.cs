using TestWare.Core;
using TestWare.Samples.Selenium.Web.POM.Stinto;
using TestWare.Samples.Selenium.Web.POM.Stinto.Chat;
using TestWare.Samples.Selenium.Web.POM.Stinto.CreateChat;

namespace TestWare.Samples.Selenium.Web.StepDefinitions.Stinto;

[Binding]
public sealed class ChatSteps
{
    private readonly IEnumerable<ICreateChatPage> createChatPages;
    private readonly IEnumerable<IChatPage> chatPages;

    public ChatSteps(FeatureContext featureContext, ScenarioContext scenarioContext)
    {
        var tags = featureContext.FeatureInfo.Tags.Concat(scenarioContext.ScenarioInfo.Tags);
        createChatPages = ContainerManager.GetTestWareComponents<ICreateChatPage>(tags);
        chatPages = ContainerManager.GetTestWareComponents<IChatPage>(tags);
    }

    [Given(@"the '([^']*)' creates a new chat session on '([^']*)'")]
    public void GivenTheCreatesANewChatSessionOn(string user, string browser)
    {
        var homePage = ContainerManager.GetTestWareComponent<IHomePage>(browser);
        homePage.ClickCreateChat();

        var createChatPage = ContainerManager.GetTestWareComponent<ICreateChatPage>(browser);
        createChatPage.SetUserId(user);
        createChatPage.AcceptTermsOfUse();
        createChatPage.ClickSubmitButton();
    }

    [When(@"the '([^']*)' joins chat session on '([^']*)'")]
    public void WhenTheJoinsChatSessionOn(string user, string browser)
    {
        var homePage = ContainerManager.GetTestWareComponent<IHomePage>(browser);
        var createChatPage = ContainerManager.GetTestWareComponent<ICreateChatPage>(browser);

        var urls = new List<string>();
        createChatPages.ToList().ForEach(x => urls.Add(x.GetChatUrl()));

        homePage.NavigateTo(urls.OrderByDescending(s => s.Length).First());
        createChatPage.SetUserId(user);
        createChatPage.AcceptTermsOfUse();
        createChatPage.ClickSubmitButton();
    }

    [Then(@"the '([^']*)' message from '([^']*)' appears on '([^']*)'")]
    public void ThenTheMessageFromAppearsOn(string message, string fromUser, string browser)
    {
        var chatPage = ContainerManager.GetTestWareComponent<IChatPage>(browser);
        chatPage.CheckMessage(fromUser, message);
    }

    [Then(@"the '([^']*)' message from '([^']*)' appears on all browsers")]
    public void ThenTheMessageFromAppearsOnAllBrowsers(string message, string userFrom)
    {
        chatPages.ToList().ForEach(x => x.CheckMessage(userFrom, message));
    }

    [When(@"the user sends '([^']*)' message on '([^']*)'")]
    public void WhenTheUserSendsMessageOn(string message, string browser)
    {
        var chatPage = ContainerManager.GetTestWareComponent<IChatPage>(browser);
        chatPage.SendMessage(message);
    }
}

