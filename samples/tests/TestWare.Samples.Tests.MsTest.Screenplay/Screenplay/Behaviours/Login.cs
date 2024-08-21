using TestWare.Samples.Suts.SwagLabs.Playwright.Screenplay;
using TestWare.Samples.Tests.MsTest.Screenplay.Core.Bases;
using TestWare.Samples.Tests.MsTest.Screenplay.Core.Interfaces;
using TestWare.Samples.Tests.MsTest.Screenplay.Interactions;

namespace TestWare.Samples.Tests.MsTest.Screenplay.Behaviours;

public class Login : Behaviour
{
    private string _user;
    private string _password;

    public static Login WithUser(string username)
    {
        return new Login() { _user = username };
    }

    public Login WithPassword(string password)
    {
        _password = password;
        return this;
    }

    public override Login PerformedAs(IActor actor)
    {
        actor
            .Performs(Log.TheMessage($"{_user} Introduces credentials"))
            .Performs(Enter.TheValue(_user).Into(Locators.UserField))
            .Performs(Enter.TheValue(_password).Into(Locators.PasswordField))
            .Performs(Log.TheMessage($"{_user} submits login form"))
            .Performs(Click.On(Locators.SubmitBtn));
        return this;
    }

}
