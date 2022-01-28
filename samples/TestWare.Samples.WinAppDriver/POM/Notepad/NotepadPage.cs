using OpenQA.Selenium;
using TestWare.Engines.Appium.Extras;
using TestWare.Engines.Appium.WinAppDriver.Factory;
using TestWare.Engines.Appium.WinAppDriver.Pages;

namespace TestWare.Samples.WinAppDriver.Desktop.POM.Notepad;

public class NotepadPage : WinAppDriverPage, INotepadPage
{
    [FindsBy(How = How.ClassName, Using = "Edit")]
    private IWebElement TextField { get; set; }

    public NotepadPage(IWindowsDriver driver) : base(driver)
    {
    }

    public void CheckText(string textToVerify)
    {
        TextField.Text.Should().Be(textToVerify);
    }

    public void DeleteCharacters(int charsToDelete)
    {
        for(int i = 0; i < charsToDelete; i++)
        {
            SendKeysElement(TextField, Keys.Backspace);
        }
    }

    public void WriteText(string textToWrite)
    {
        SendKeysElement(TextField, textToWrite);
    }
}
