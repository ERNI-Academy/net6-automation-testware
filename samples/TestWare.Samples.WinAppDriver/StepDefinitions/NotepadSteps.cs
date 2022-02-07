using TestWare.Core;
using TestWare.Samples.WinAppDriver.Desktop.POM.Notepad;

namespace TestWare.Samples.WinAppDriver.Desktop.StepDefinitions;

[Binding]
public class NotepadSteps
{
    private readonly INotepadPage _notepadPage;

    public NotepadSteps()
    {
        _notepadPage = ContainerManager.GetTestWareComponent<INotepadPage>();
    }

    [Given(@"user writes '([^']*)'")]
    public void GivenUserWrites(string textToWrite)
    {
        textToWrite = textToWrite ?? throw new ArgumentNullException(nameof(textToWrite));
        _notepadPage.WriteText(textToWrite);
    }

    [When(@"user deletes '([^']*)' characters")]
    public void WhenUserDeletesCharacters(int charsToDelete)
    {
        _notepadPage.DeleteCharacters(charsToDelete);
    }

    [Then(@"file text is '([^']*)'")]
    public void ThenFileTextIs(string testToVerify)
    {
        testToVerify = testToVerify ?? throw new ArgumentNullException(nameof(testToVerify));
        _notepadPage.CheckText(testToVerify);
    }
}
