using Autofac;
using TestWare.Core;
using TestWare.Samples.WinAppDriver.Desktop.POM;

namespace TestWare.Samples.WinAppDriver.Desktop.StepDefinitions;

[Binding]
public sealed class CalculatorSteps
{
    private readonly ICalculatorPage calculatorPage;

    public CalculatorSteps()
    {
        using (var scope = ContainerManager.Container.BeginLifetimeScope())
        {
            calculatorPage = scope.Resolve<ICalculatorPage>() ?? throw new ArgumentNullException(nameof(ICalculatorPage));
        }
    }

    [Given("select number '(.*)'")]
    [When("select number '(.*)'")]
    [Then("select number '(.*)'")]
    public void SelectNumber(string number)
    {
        number = number ?? throw new ArgumentNullException(nameof(number));

        this.calculatorPage.ClickNumberButton(number);
    }

    [Given("select operation '(.*)'")]
    [When("select operation '(.*)'")]
    [Then("select operation '(.*)'")]
    public void SelectOperation(string operationType)
    {
        var validOperations = new List<string> { "+", "-", "*", "/" };

        operationType = validOperations.Contains(operationType) ? operationType : throw new ArgumentOutOfRangeException("Not a valid operation.");

        switch (operationType)
        {
            case "+":
                this.calculatorPage.ClickAddButton();
                break;
            case "-":
                this.calculatorPage.ClickMinusButton();
                break;
            case "*":
                this.calculatorPage.ClickMultiplyButton();
                break;
            case "/":
                this.calculatorPage.ClickDivideButton();
                break;
        }
    }

    [When("click on operation equals button")]
    public void ClickEqualsButton()
    {
        this.calculatorPage.ClickEqualsButton();
    }

    [Then("the result should be '(.*)'")]
    public void ThenTheResultShouldBe(string result)
    {
        result = result ?? throw new ArgumentNullException(nameof(result));

        this.calculatorPage.CheckResultIs(result);
    }
}
