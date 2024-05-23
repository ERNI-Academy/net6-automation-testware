using TestWare.Core;
using TestWare.Samples.Selenium.Web.POM.Chat;
using TestWare.Samples.Selenium.Web.POM.CreateChat;

namespace TestWare.Samples.Selenium.Web.StepDefinitions.Chat;

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
        var createChatPage = ContainerManager.GetTestWareComponent<ICreateChatPage>(browser);
        createChatPage.SetUserId(user);
        createChatPage.SetChatRoomTitle("testRoom");
        createChatPage.ClickSubmitButton();
    }

    [When(@"the '([^']*)' joins chat session on '([^']*)'")]
    public void WhenTheJoinsChatSessionOn(string user, string browser)
    {
        var createChatPage = ContainerManager.GetTestWareComponent<ICreateChatPage>(browser);

        var urls = new List<string>();
        createChatPages.ToList().ForEach(x => urls.Add(x.GetChatUrl()));

        createChatPage.NavigateTo(urls.OrderByDescending(s => s.Length).First());
        createChatPage.SetUserId(user);
        createChatPage.ClickSubmitButton();
    }

    [Then(@"the '([^']*)' message from '([^']*)' appears on '([^']*)'")]
    public void ThenTheMessageFromAppearsOn(string message, string fromUser, string browser)
    {
        var chatPage = ContainerManager.GetTestWareComponent<IChatPage>(browser);
        chatPage.CheckChatMessage(fromUser, message);
    }

    [Then(@"the '([^']*)' message from '([^']*)' appears on all browsers")]
    public void ThenTheMessageFromAppearsOnAllBrowsers(string message, string userFrom)
    {
        chatPages.ToList().ForEach(x => x.CheckChatMessage(userFrom, message));
    }

    [When(@"the user sends '([^']*)' message on '([^']*)'")]
    public void WhenTheUserSendsMessageOn(string message, string browser)
    {
        var chatPage = ContainerManager.GetTestWareComponent<IChatPage>(browser);
        chatPage.SendMessage(message);
    }
}

