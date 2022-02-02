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

    [FindsBy(How = How.Name, Using = "=")]
    private IWebElement EqualsButton { get; set; }

    [FindsBy(How = How.Name, Using = "+")]
    private IWebElement AdditionButton { get; set; }

    [FindsBy(How = How.Name, Using = "-")]
    private IWebElement MinusButton { get; set; }

    [FindsBy(How = How.Name, Using = "*")]
    private IWebElement MultiplyButton { get; set; }

    [FindsBy(How = How.Name, Using = "/")]
    private IWebElement DivideButton { get; set; }

    [FindsBy(How = How.AccessibilityId, Using = "2010")]
    private IWebElement ResultTextField { get; set; }
    #nullable disable

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
            ClickElement(Driver.FindElement(MobileBy.Name(digit.ToString())));
        }
    }

    public void CheckResultIs(string expectedResult)
    {
        ResultTextField.Text.Should().Be(expectedResult);
    }
}
