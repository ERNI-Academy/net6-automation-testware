using TestWare.Core.Interfaces;

namespace TestWare.Samples.WinAppDriver.Desktop.POM.WindowsCalculator;
public interface IWindowsCalculatorPage : ITestWareComponent
{
    void ClickEqualsButton();
    void ClickAddButton();
    void ClickMinusButton();
    void ClickMultiplyButton();
    void ClickDivideButton();
    void ClickNumberButton(string number);
    void CheckResultIs(string expectedResult);
}
