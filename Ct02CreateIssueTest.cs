// Desenvolvido por: Izack G. Passos Rodrigues - Setembro/2020
// O objetivo deste teste é validar o fluxo completo de inclusão de um novo registro no Bug Tracker.

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
public class Ct02CreateIssueTest {
  private IWebDriver driver;
  public IDictionary<string, object> vars {get; private set;}
  private IJavaScriptExecutor js;
  public string username = ""; //Variavel de preenchimento obrigatório para o teste.
  public string pass = ""; //Variavel de preenchimento obrigatório para o teste.
  public string project = ""; //Preencha com um projeto com acesso valido ex.:"Izack Rodrigues´s project"
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
  public void ct02CreateIssue() {
    driver.Navigate().GoToUrl("https://mantis-prova.base2.com.br/login_page.php");
    driver.FindElement(By.Name("username")).SendKeys(username);
    driver.FindElement(By.Name("password")).SendKeys(pass);
    driver.FindElement(By.CssSelector(".button")).Click();
    driver.FindElement(By.CssSelector(".hide:nth-child(1) tr:nth-child(1) > td:nth-child(2)")).Click();
    driver.FindElement(By.LinkText("Report Issue")).Click();
    driver.FindElement(By.CssSelector("td > select")).Click();
    {
      var dropdown = driver.FindElement(By.CssSelector("td > select"));
      dropdown.FindElement(By.XPath("//option[. = '" +project + "']")).Click();
    }
    driver.FindElement(By.CssSelector("td > select")).Click();
    driver.FindElement(By.CssSelector(".button")).Click();
    var category_id = driver.FindElements(By.Name("category_id"));
    Assert.True(category_id.Count > 0);
    {
      var dropdown = driver.FindElement(By.Name("category_id"));
      dropdown.FindElement(By.XPath("//option[. = '[All Projects] Teste']")).Click();
    }
    var reproducibility = driver.FindElements(By.Name("reproducibility"));
    Assert.True(reproducibility.Count > 0);
    {
      var dropdown = driver.FindElement(By.Name("reproducibility"));
      dropdown.FindElement(By.XPath("//option[. = 'N/A']")).Click();
    }
    {
      var dropdown = driver.FindElement(By.Name("priority"));
      dropdown.FindElement(By.XPath("//option[. = 'none']")).Click();
    }
    driver.FindElement(By.Id("platform")).Click();

    var summary = driver.FindElements(By.Name("summary"));
    Assert.True(summary.Count > 0);
    driver.FindElement(By.Name("summary")).SendKeys("Realização de testes automaticos.");
    var description = driver.FindElements(By.Name("description"));
    Assert.True(description.Count > 0);
    driver.FindElement(By.Name("description")).SendKeys("Descrição do bug a ser verificado.");
    var steps_to_reproduce = driver.FindElements(By.Name("steps_to_reproduce"));
    Assert.True(steps_to_reproduce.Count > 0);
    driver.FindElement(By.Name("steps_to_reproduce")).SendKeys("1º Passo\n2º Passo\n3º Passo\n4º Passo");
    Thread.Sleep(2000);

    var additional_info = driver.FindElements(By.Name("additional_info"));
    Assert.True(additional_info.Count > 0);
    driver.FindElement(By.Name("additional_info")).SendKeys("Não há informação adicional.");
    var CreateButton = driver.FindElements(By.CssSelector(".button"));
    Assert.True(CreateButton.Count > 0);
    Thread.Sleep(2000);
    driver.FindElement(By.CssSelector(".button")).Click();
    Thread.Sleep(2000);
    driver.FindElement(By.LinkText("My View")).Click();
    driver.FindElement(By.LinkText("View Issues")).Click();
    driver.Close();
    driver.Quit();
  }
}
