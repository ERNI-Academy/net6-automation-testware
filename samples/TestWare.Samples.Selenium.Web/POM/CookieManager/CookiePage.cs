using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWare.Engines.Selenium.Pages;

namespace TestWare.Samples.Selenium.Web.POM.CookieManager
{
    public class CookiePage : WebPage, ICookiePage
    {
        private const string PrivacyFrameId = "gdpr-consent-notice";
        private const string AcceptAllCookiesButtonId = "save";

        public CookiePage(IWebDriver driver) : base(driver)
        {
        }

        public void ClickAcceptAllCookiesButton()
        {
            Thread.Sleep(2000);
            WaitUntilElementIsVisible(By.Id(PrivacyFrameId));
            var frame = Driver.FindElement(By.Id(PrivacyFrameId));
            Driver.SwitchTo().Frame(frame);
            var acceptAllCookiesButton = Driver.FindElement(By.Id(AcceptAllCookiesButtonId));
            ClickElement(acceptAllCookiesButton);
            Driver.SwitchTo().DefaultContent();
        }
    }
}
