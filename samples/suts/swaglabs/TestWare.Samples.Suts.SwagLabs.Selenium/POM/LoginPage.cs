using OpenQA.Selenium;
using TestWare.Engines.SeleniumEngine;
using TestWare.Engines.SeleniumEngine.Extensions;
using TestWare.Samples.Suts.SwagLabs.Interfaces.POM;

namespace TestWare.Samples.Suts.SwagLabs.Selenium.POM;

public class LoginPage : PageBase, ILoginPage
{

    private By _userNameInput = By.Id("user-name"); 
    private By _passwordInput =  By.Name("password");
    private By _submitBtn = By.XPath("//*[@type='submit']"); 
    private By _errorMessage = By.XPath("//*[@data-test='error']");
    private By _loginContainer = By.ClassName("login_wrapper");

    public LoginPage(ISeleniumEngine engine) : base(engine)
    {
    }

    public void SetUserName(string value) 
    {
        Driver.SafeSendKeys(_userNameInput, value);
    }

    public void SetPassword(string value) 
    {
        Driver.SafeSendKeys(_passwordInput, value);
    }

    public void SubmitCredentials()
    {
        Driver.SafeClick(_submitBtn);
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

    public bool IsLoaded()
    {
        return Driver.ElementIsPresent(_loginContainer);
        
    }

    public bool IsNotLoaded()
    {
        return Driver.ElementIsNotPresent(_loginContainer);
    }

    public bool CheckErrorMessage(string expectedText)
    {
        throw new NotImplementedException();
    }
}
