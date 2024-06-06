using TestWare.Core.Interfaces;

namespace TestWare.Samples.Suts.SwagLabs.Interfaces.POM;

public interface ILoginPage : ITestwareComponent
{
    public void SetUserName(string value);

    public void SetPassword(string value);

    public void SubmitCredentials();

    public bool IsLoaded();

    public bool IsNotLoaded();

    public bool CheckErrorMessage(string expectedText);
}
