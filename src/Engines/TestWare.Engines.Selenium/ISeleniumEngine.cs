using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWare.Core.Interfaces;

namespace TestWare.Engines.SeleniumEngine;

public interface ISeleniumEngine:ITestWareEngine
{
    IWebDriver Driver { get; }
}
