using TestWare.Core.Interfaces;

namespace TestWare.Samples.WinAppDriver.Desktop.POM.Notepad;

internal interface INotepadPage : ITestWareComponent
{
    void WriteText(string textToWrite);

    void CheckText(string textToVerify);

    void DeleteCharacters(int charsToDelete);
}
