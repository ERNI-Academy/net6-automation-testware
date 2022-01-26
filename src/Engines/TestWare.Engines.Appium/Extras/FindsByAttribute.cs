using System.ComponentModel;
using OpenQA.Selenium;

namespace TestWare.Engines.Appium.Extras;

public class FindsByAttribute : TestWare.Engines.Selenium.Extras.FindsByAttribute
{
    [DefaultValue(How.Id)]
    new public How How { get; set; }

    public override By Finder
    {
        get
        {
            if (this.finder == null)
            {
                this.finder = ByFactory.From(this);
            }

            return this.finder;
        }
    }
}
