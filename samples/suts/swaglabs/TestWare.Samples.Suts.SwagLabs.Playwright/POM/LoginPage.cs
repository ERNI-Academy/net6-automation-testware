using TestWare.Core.Interfaces;
using TestWare.Samples.Suts.SwagLabs.Interfaces.POM;

namespace TestWare.Samples.Suts.SwagLabs.Playwright.POM;

public class LoginPage : ILoginPage
{


    public LoginPage(ITestWareEngine engine)
    {
    }

    public void SetUserName(string value)
    {
        throw new NotImplementedException();
        //SendKeysElement(UserNameInput, value);
    }

    public void SetPassword(string value)
    {
        throw new NotImplementedException();
        //SendKeysElement(PasswordInput, value);
    }

    public void ClickSubmitButton()
    {
        throw new NotImplementedException();
        //ClickElement(SubmitBtn);
    }

    public void CheckUserIsAtLoginPage()
    {
        throw new NotImplementedException();
        //ElementIsPresent(UserNameInput);
        //ElementIsPresent(PasswordInput);
        //ElementIsPresent(SubmitBtn);
    }

    public void CheckUserIsNotAtLoginPage()
    {
        throw new NotImplementedException();
        //ElementIsNotPresent(UserNameInput);
        //ElementIsNotPresent(PasswordInput);
        //ElementIsNotPresent(SubmitBtn);
    }

    public void CheckErrorMessageText(string expectedText)
    {
        throw new NotImplementedException();
        //CheckElementText(ErrorMessage, expectedText);
    }

    public void SubmitCredentials()
    {
        throw new NotImplementedException();
    }

    public void IsLoaded()
    {
        throw new NotImplementedException();
    }

    public void IsNotLoaded()
    {
        throw new NotImplementedException();
    }

    bool ILoginPage.IsLoaded()
    {
        throw new NotImplementedException();
    }

    bool ILoginPage.IsNotLoaded()
    {
        throw new NotImplementedException();
    }

    public bool CheckErrorMessage(string expectedText)
    {
        throw new NotImplementedException();
    }
}
