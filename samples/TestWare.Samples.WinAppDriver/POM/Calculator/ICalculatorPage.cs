using TestWare.Core.Interfaces;

namespace TestWare.Samples.WinAppDriver.Desktop.POM;

/// <summary>
/// Initializes a new instance of the <see cref="ICalculatorPage"/> interface class that contains
/// all the methods that can be used at Steps level.
/// </summary>
public interface ICalculatorPage : ITestWareComponent
{
    void ClickEqualsButton();
    void ClickAddButton();
    void ClickMinusButton();
    void ClickMultiplyButton();
    void ClickDivideButton();
    void ClickNumberButton(string number);
    void CheckResultIs(string expectedResult);
}
