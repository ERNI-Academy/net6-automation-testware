using OpenQA.Selenium.Appium;
using OpenQA.Selenium;
using TestWare.Engines.Appium.Extras;
using TestWare.Engines.Appium.WinAppDriver.Factory;
using TestWare.Engines.Appium.WinAppDriver.Pages;
using Humanizer;
using System.Globalization;

namespace TestWare.Samples.WinAppDriver.Desktop.POM.WindowsCalculator;

/// <summary>
/// Initializes a new instance of the <see cref="CalculatorPage"/> class that contains
/// all the elements and the interaction methods that will be exposed at it's Interface class.
/// </summary>
public class WindowsCalculatorPage : WinAppDriverPage, IWindowsCalculatorPage
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
#nullable disable

    public WindowsCalculatorPage(IWindowsDriver driver) : base(driver)
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
        foreach (var digit in number.ToCharArray()
                                    .Select(item => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(int.Parse($"{item}").ToWords().ToLower())))
        {
            ClickElement(Driver.FindElement(MobileBy.Name(digit)));
        }
    }

    public void CheckResultIs(string expectedResult)
    {
        var result = Driver.FindElement(MobileBy.Name($"Display is {expectedResult}"));
        result.Text.Should().Be($"Display is {expectedResult}");
    }
}


