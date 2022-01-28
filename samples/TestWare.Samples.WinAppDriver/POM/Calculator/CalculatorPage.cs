using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using TestWare.Engines.Appium.Extras;
using TestWare.Engines.Appium.WinAppDriver.Factory;
using TestWare.Engines.Appium.WinAppDriver.Pages;

namespace TestWare.Samples.WinAppDriver.Desktop.POM;

/// <summary>
/// Initializes a new instance of the <see cref="CalculatorPage"/> class that contains
/// all the elements and the interaction methods that will be exposed at it's Interface class.
/// </summary>
public class CalculatorPage : WinAppDriverPage, ICalculatorPage
{
    #nullable enable

    [FindsBy(How = How.Name, Using = "Equals")]
    private IWebElement EqualsButton { get; set; }

    [FindsBy(How = How.Name, Using = "Plus")]
    private IWebElement AdditionButton { get; set; }

    [FindsBy(How = How.Name, Using = "Minus")]
    private IWebElement MinusButton { get; set; }

    [FindsBy(How = How.Name, Using = "Multiply by")]
    private IWebElement MultiplyButton { get; set; }

    [FindsBy(How = How.Name, Using = "Divide by")]
    private IWebElement DivideButton { get; set; }

    [FindsBy(How = How.AccessibilityId, Using = "CalculatorResults")]
    private IWebElement ResultTextField { get; set; }
    #nullable disable


    private readonly string numberLocator = "num{0}Button";

    public CalculatorPage(IWindowsDriver driver) : base(driver)
    {
    }

    public void ClickEqualsButton()
        => ClickElement(EqualsButton);

    public void ClickAddButton()
        => ClickElement(AdditionButton);

    public void ClickMinusButton()
        => ClickElement(MinusButton);

    public void ClickMultiplyButton()
        => ClickElement(MultiplyButton);

    public void ClickDivideButton()
        => ClickElement(DivideButton);

    public void ClickNumberButton(string number)
    {
        foreach (var digit in number.ToCharArray())
        {
            var numberLocator = string.Format(this.numberLocator, digit);
            ClickElement(Driver.FindElement(MobileBy.AccessibilityId(numberLocator)));
        }
    }

    public void CheckResultIs(string expectedResult)
    {
        var result = ResultTextField.Text.Replace("Display is ", "").Replace(",", "");
        result.Should().Be(expectedResult);
    }
}
