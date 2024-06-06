using OpenQA.Selenium;
using TestWare.Core;
using TestWare.Engines.SeleniumEngine;
using TestWare.Engines.SeleniumEngine.Extensions;

namespace TestWare.Samples.Tests.MsTest;

[TestClass]
public class LinearScriptSeleniumMultiScope : TestSuiteBase
{

    [TestWareMethod]
    [TestWareScopes("TheInternet-tables")]
    [TestWareDoc("Description", "Test case for Tables. Test Defining scope")]

    public void CheckUsersBy50Due(ISeleniumEngine InternetTables)
    {
        var table = InternetTables.Driver.FindElement(By.Id("table1"));
        var headers = table.FindElements(By.XPath(".//thead/tr/th"));
        var rows = table.FindElements(By.XPath("./tbody/tr"));

        var headerIndex = -1;

        headerIndex = headers.Select(x => x.Text).ToList().IndexOf("Due");

        var usersWith50Due = new List<string>();

        var rowsWithMatches = rows.Where(x => x.FindElements(By.XPath("./td")).Any(y => y.Text.Contains("50"))).ToList();
        rowsWithMatches.ForEach(match => usersWith50Due.Add(match.FindElements(By.XPath("./td"))[1].Text));

        Assert.AreEqual("John", usersWith50Due[0]);
        Assert.AreEqual("Tim", usersWith50Due[1]);
    }

    [TestWareMethod]
    [TestWareScopes("TheInternet-tables")]
    public void CheckTableCanBeSorted(ISeleniumEngine InternetTables)
    {
        var table = InternetTables.Driver.FindElement(By.Id("table1"));
        var headers = table.FindElements(By.XPath("./thead/tr/th"));
        var rows = table.FindElements(By.XPath("./tbody/tr"));

        headers.Where(x => x.Text == "First Name").FirstOrDefault().Click();
        var firstNameHeaderIndex = headers.Select(x => x.Text).ToList().IndexOf("First Name");

        rows = table.FindElements(By.XPath("./tbody/tr"));
        var headerFirstNameValues = rows.Select(x => x.FindElements(By.XPath("./td"))[firstNameHeaderIndex].Text).ToList();

        Assert.AreEqual(headerFirstNameValues[0], "Frank");
        Assert.AreEqual(headerFirstNameValues[1], "Jason");
        Assert.AreEqual(headerFirstNameValues[2], "John");
        Assert.AreEqual(headerFirstNameValues[3], "Tim");
    }

    [TestWareMethod]
    [TestWareScopes("TheInternet-checkboxes")]
    public void ClickAndVerifyCheckbox(ISeleniumEngine InternetCheckboxes)
    {
        var checkBoxList = InternetCheckboxes.Driver.FindElements(By.XPath("//*[@type='checkbox']")).ToList();

        checkBoxList.ForEach(checkbox => checkbox.Click());

        Assert.IsTrue(checkBoxList[0].Selected);
        Assert.IsFalse(checkBoxList[1].Selected);
    }
}
