// Desenvolvido por: Izack G. Passos Rodrigues - Setembro/2020
// O objetivo deste teste é validar o fluxo completo de inclusão de um novo registro no Bug Tracker
// sem os dados obrigatórios.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using NUnit.Framework;
[TestFixture]
public class Ct01CreateIssueRequiredTest {
  private IWebDriver driver;
  public IDictionary<string, object> vars {get; private set;}
  private IJavaScriptExecutor js;
  public string textSummary = "A necessary field \"Summary\" was empty. Please recheck your inputs.";

  [SetUp]
  public void SetUp() {
    driver = new ChromeDriver();
    js = (IJavaScriptExecutor)driver;
    vars = new Dictionary<string, object>();
  }
  [TearDown]
  protected void TearDown() {
    driver.Quit();
  }
  [Test]
  public void ct01CreateIssueRequired() {
    driver.Navigate().GoToUrl("https://mantis-prova.base2.com.br/login_page.php");
    driver.FindElement(By.Name("username")).SendKeys("izack.rodrigues");
    driver.FindElement(By.Name("password")).SendKeys("Teste@19");
    driver.FindElement(By.CssSelector(".button")).Click();
    driver.FindElement(By.LinkText("Report Issue")).Click();

    Thread.Sleep(2000);
    driver.FindElement(By.CssSelector("td > select")).Click();
    {
      var dropdown = driver.FindElement(By.CssSelector("td > select"));
      dropdown.FindElement(By.XPath("//option[. = 'Izack Rodrigues´s project']")).Click();
    }
    driver.FindElement(By.CssSelector(".button")).Click();
    var elements = driver.FindElements(By.CssSelector(".left > .required"));
    Assert.True(elements.Count > 0);
    driver.FindElement(By.CssSelector(".button")).Click();
    Assert.That(driver.FindElement(By.CssSelector(".form-title")).Text, Is.EqualTo("APPLICATION ERROR #11"));
    Assert.That(driver.FindElement(By.CssSelector("tr:nth-child(2) .center")).Text, Is.EqualTo(textSummary));
    
     Thread.Sleep(2000);

    }
}
