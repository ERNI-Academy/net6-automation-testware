using TestWare.Core;
using TestWare.Samples.WinAppDriver.Desktop.POM.WindowsCalculator;

namespace TestWare.Samples.WinAppDriver.Desktop.StepDefinitions;

[Binding]
public sealed class WindowsCalculatorSteps
{
    private readonly IWindowsCalculatorPage windowsCalculatorPage;

    public WindowsCalculatorSteps()
    {
        windowsCalculatorPage = ContainerManager.GetTestWareComponent<IWindowsCalculatorPage>();
    }


    [Given("number '(.*)'")]
    [When("number '(.*)'")]
    [Then("number '(.*)'")]
    public void SelectNumber(string number)
    {
        number = number ?? throw new ArgumentNullException(nameof(number));

        this.windowsCalculatorPage.ClickNumberButton(number);
    }

    [Given("operation '(.*)'")]
    [When("operation '(.*)'")]
    [Then("operation '(.*)'")]
    public void SelectOperation(string operationType)
    {
        var validOperations = new List<string> { "+", "-", "*", "/" };

        operationType = validOperations.Contains(operationType) ? operationType : throw new ArgumentOutOfRangeException("Not a valid operation.");

        switch (operationType)
        {
            case "+":
                this.windowsCalculatorPage.ClickAddButton();
                break;
            case "-":
                this.windowsCalculatorPage.ClickMinusButton();
                break;
            case "*":
                this.windowsCalculatorPage.ClickMultiplyButton();
                break;
            case "/":
                this.windowsCalculatorPage.ClickDivideButton();
                break;
        }
    }

    [When("click on equals button")]
    public void ClickEqualsButton()
    {
        this.windowsCalculatorPage.ClickEqualsButton();
    }

    [Then("result should be '(.*)'")]
    public void ThenTheResultShouldBe(string result)
    {
        result = result ?? throw new ArgumentNullException(nameof(result));

        this.windowsCalculatorPage.CheckResultIs(result);
    }
}
