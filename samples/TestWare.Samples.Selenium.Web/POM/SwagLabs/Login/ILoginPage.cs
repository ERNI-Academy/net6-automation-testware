using TestWare.Core.Interfaces;

namespace TestWare.Samples.Selenium.Web.POM.Login;

/// <summary>
/// Encapsulate all Loging busines logic
/// </summary>
public interface ILoginPage : ITestWareComponent
{
    /// <summary>
    /// Script to send User Name
    /// </summary>
    /// <param name="name"></param>
    void EnterUserName(string name);

    /// <summary>
    /// Script to send User Password
    /// </summary>
    /// <param name="name"></param>
    void EnterUserPassword(string password);

    /// <summary>
    /// Click in Login button
    /// </summary>
    void ClickLoginButton();

    /// <summary>
    /// Check that the user is at Login Page
    /// </summary>
    void CheckUserIsAtLoginpage();
}
