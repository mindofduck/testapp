using System;
using System.Data;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Runtime.InteropServices.JavaScript;
using Npgsql;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace ConsoleTestSite
{
  class MainTest
  {
    
    static void Main(string[] args)
    {
      Console.WriteLine("Начало итерации");
      
      Stopwatch stopwatch = new Stopwatch();
      String nameOfProduct = "";
      Random rnd = new Random();
      Dictionary<string, string> infoToDataBase = new Dictionary<string, string>()
      {
        {"TimestampStart", ""},
        {"TimestampEnd", ""},
        {"Latency", ""},
        {"OperationCode", ""},
        {"Result", ""},
        {"Error", ""}
      };
      //sqlConnection(infoToDataBase);
      
      //IWebDriver driver = new FirefoxDriver();
      List<int> nums = new() {1, 2, 3};
      foreach (var num in nums)
      {
        IWebDriver driver = new FirefoxDriver();
        Console.WriteLine(
          $"--Начало итерации - [{num}]- \n--Время {new DateTime(1970, 1, 1).AddSeconds(DateTimeOffset.UtcNow.ToUnixTimeSeconds())}");
        Console.WriteLine("");
        try
        {
          //throw new ArgumentException("Проверка бд");
          infoToDataBase["OperationCode"] = "opening";
          //long unixtime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
          infoToDataBase["TimestampStart"] = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
          stopwatch.Start();
          driver.Navigate().GoToUrl("https://shop.mts.by");
          stopwatch.Stop();
          infoToDataBase["TimestampEnd"] = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
          //String latency = String.Format("{0:00}:{1:00}:{2:00}:{3:00}", ts.Hours, ts.Minutes, ts.Seconds,
          //  ts.Milliseconds / 10);
          infoToDataBase["Latency"] = stopwatch.ElapsedMilliseconds.ToString();
          infoToDataBase["Result"] = "passed";
          infoToDataBase["Error"] = "-";
          stopwatch.Reset();
          sqlConnection(infoToDataBase);
          Thread.Sleep(2000);

          driver.FindElement(By.XPath("//*[@id=\"cookie-cancel\"]")).Click();

          infoToDataBase["OperationCode"] = "catalog";
          infoToDataBase["TimestampStart"] = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
          stopwatch.Start();
          driver.FindElement(By.XPath("/html/body/header/div[5]/div[1]/div/button")).Click();
          stopwatch.Stop();
          infoToDataBase["TimestampEnd"] = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
          infoToDataBase["Latency"] = stopwatch.ElapsedMilliseconds.ToString();
          infoToDataBase["Result"] = "passed";
          infoToDataBase["Error"] = "-";
          stopwatch.Reset();
          sqlConnection(infoToDataBase);
          Thread.Sleep(2000);

          infoToDataBase["OperationCode"] = "all";
          infoToDataBase["TimestampStart"] = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
          stopwatch.Start();
          driver.FindElement(By.LinkText("Все")).Click();
          stopwatch.Stop();
          infoToDataBase["TimestampEnd"] = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
          infoToDataBase["Latency"] = stopwatch.ElapsedMilliseconds.ToString();
          infoToDataBase["Result"] = "passed";
          infoToDataBase["Error"] = "-";
          stopwatch.Reset();
          sqlConnection(infoToDataBase);
          Thread.Sleep(2000);

          infoToDataBase["OperationCode"] = "apple";
          infoToDataBase["TimestampStart"] = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
          stopwatch.Start();
          driver.FindElement(By.XPath("/html/body/main/div[3]/div[2]/div/div/div/div[1]/div[2]/div/div[1]/a")).Click();
          stopwatch.Stop();
          infoToDataBase["TimestampEnd"] = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
          infoToDataBase["Latency"] = stopwatch.ElapsedMilliseconds.ToString();
          infoToDataBase["Result"] = "passed";
          infoToDataBase["Error"] = "-";
          stopwatch.Reset();
          sqlConnection(infoToDataBase);
          Thread.Sleep(2000);

          infoToDataBase["OperationCode"] = "element";
          infoToDataBase["TimestampStart"] = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
          stopwatch.Start();
          IList<IWebElement> elements = driver.FindElements(By.ClassName("products__unit__body--hover"));
          int randomElement = rnd.Next(elements.Count);
          nameOfProduct = elements[randomElement].FindElement(By.ClassName("linkTovar")).Text;
          elements[randomElement].Click();
          stopwatch.Stop();
          infoToDataBase["TimestampEnd"] = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
          infoToDataBase["Latency"] = stopwatch.ElapsedMilliseconds.ToString();
          infoToDataBase["Result"] = "passed";
          infoToDataBase["Error"] = "-";
          stopwatch.Reset();
          sqlConnection(infoToDataBase);
          Thread.Sleep(2000);

          infoToDataBase["OperationCode"] = "add-to-card";
          infoToDataBase["TimestampStart"] = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
          stopwatch.Start();
          driver.FindElement(By.LinkText("Добавить в корзину")).Click();
          stopwatch.Stop();
          infoToDataBase["TimestampEnd"] = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
          infoToDataBase["Latency"] = stopwatch.ElapsedMilliseconds.ToString();
          infoToDataBase["Result"] = "passed";
          infoToDataBase["Error"] = "-";
          stopwatch.Reset();
          sqlConnection(infoToDataBase);
          Thread.Sleep(2000);

          infoToDataBase["OperationCode"] = "go-to-cart-and-check";
          infoToDataBase["TimestampStart"] = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
          stopwatch.Start();
          driver.FindElement(By.LinkText("Перейти в корзину")).Click();
          Thread.Sleep(500);
          String name = driver.FindElement(By.XPath(
            "/html/body/main/div[1]/div[2]/div/form/div[1]/div/div[1]/div/div/div/div[2]/div[1]/div[1]/div[1]/a")).Text;
          Boolean resultTest = Equals(name, nameOfProduct);
          if (!resultTest)
            throw new ArgumentException(
              "Сработала ошибка: Элемент найден в каталоге не совпадает с элементом в корзине");
          infoToDataBase["TimestampEnd"] = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
          infoToDataBase["Latency"] = stopwatch.ElapsedMilliseconds.ToString();
          infoToDataBase["Result"] = "passed";
          infoToDataBase["Error"] = "-";
          stopwatch.Reset();
          sqlConnection(infoToDataBase);

          driver.Close();
        }
        catch (Exception e)
        {
          infoToDataBase["TimestampEnd"] = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
          infoToDataBase["Result"] = "failed";
          infoToDataBase["Error"] = e.ToString();
          sqlConnection(infoToDataBase);
          Console.WriteLine(e);
          //driver.Close();
        }
        // 300 000 мск = 5 минут
        Thread.Sleep(300000);
      }

      //sqlConnection(infoToDataBase);
    }

    static void sqlConnection(Dictionary<string, string> infoToDataBase)
    {
      string vStrConnection =
        "host=localhost;Port=5432;Database=testchecksitebd; User Id=mainuser; password=mainuser;";
      try
      {
        NpgsqlConnection sqlConnection = new NpgsqlConnection(vStrConnection);
        sqlConnection.Open();
        NpgsqlCommand command = new NpgsqlCommand();
        command.Connection = sqlConnection;
        command.CommandType = CommandType.Text;
        command.CommandText =
        String.Format(
          "INSERT INTO infoAboutOperation(TimestampStart, TimestampEnd, Latency, CodeOperation, Result, Error) VALUES ({0}, {1}, {2}, '{3}', '{4}', '{5}')",
          Int32.Parse(infoToDataBase["TimestampStart"]), Int32.Parse(infoToDataBase["TimestampEnd"]),
          Int32.Parse(infoToDataBase["Latency"]), infoToDataBase["OperationCode"], infoToDataBase["Result"],
          infoToDataBase["Error"]);

        NpgsqlDataReader dr = command.ExecuteReader();
        Console.WriteLine(dr);
        command.Dispose();
        
        sqlConnection.Close();
      }
      catch (Exception e)
      {
        Console.WriteLine("---Error! Connecting to database is failed---");
        Console.WriteLine($"Text Exception: {e}");
        throw;
      }
    }
    
  }
  
}
