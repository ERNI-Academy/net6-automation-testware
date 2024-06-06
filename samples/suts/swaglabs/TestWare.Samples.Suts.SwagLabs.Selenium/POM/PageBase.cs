using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWare.Engines.SeleniumEngine;

namespace TestWare.Samples.Suts.SwagLabs.Selenium.POM;

public class PageBase
{
    protected readonly ISeleniumEngine Engine;
    protected IWebDriver Driver { get => Engine.Driver; }

    public PageBase(ISeleniumEngine engine)
    {
        Engine = engine;
    }
}
