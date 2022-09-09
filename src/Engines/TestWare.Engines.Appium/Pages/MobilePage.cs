using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.MultiTouch;
using System.Drawing;
using TestWare.Engines.Appium.Extras;
using TestWare.Engines.Appium.Factory;
using TestWare.Engines.Selenium.Pages;

namespace TestWare.Engines.Appium.Pages;

public abstract class MobilePage : PageBase
{
    protected MobilePage(IAppiumDriver driver)
    {
        Driver = driver;
        PageFactory.InitElements(Driver, this);
    }

    protected void SwipeElementToLeft(IWebElement element)
        => SwipeFromLocationAToLocationB(new Point(element.Location.X, element.Location.Y), new Point(0, element.Location.Y));

    protected void SwipeElementToRight(IWebElement element)
        => SwipeFromLocationAToLocationB(new Point(element.Location.X, element.Location.Y), new Point(this.Driver.Manage().Window.Size.Width, element.Location.Y));

    protected void DragFromElementAToElementB(IWebElement elementA, IWebElement elementB)
        => DragFromLocationAToLocationB(new Point(elementA.Location.X, elementA.Location.Y), new Point(elementB.Location.X, elementB.Location.Y));

    protected void SwipeFromLocationAToLocationB(Point pointA, Point pointB)
    {
        new TouchAction((AppiumDriver)this.Driver)
            .Press(pointA.X, pointA.Y)
            .Wait(500)
            .MoveTo(pointB.X, pointB.Y)
            .Release()
            .Perform();
    }

    protected void DragFromLocationAToLocationB(Point pointA, Point pointB)
    {
        new TouchAction((AppiumDriver)this.Driver)
            .LongPress(pointA.X, pointA.Y)
            .Wait(500)
            .MoveTo(pointB.X, pointB.Y)
            .Release()
            .Perform();
    }

    protected void ScrollFromLoactionAtoLocationB(Point pointA, Point pointB)
    {
        new TouchAction((AppiumDriver)this.Driver)
            .Press(pointA.X, pointA.Y)
            .Wait(5)
            .MoveTo(pointB.X, pointB.Y)
            .Wait(5)
            .Release()
            .Perform();
    }
}
