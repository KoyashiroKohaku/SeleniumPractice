using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumPractice
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GoogleSearchTest()
        {
            using var driver = CreateDriver();

            // Qiitaにアクセス
            driver.Navigate().GoToUrl("https://www.google.com/");

            // 検索ボックス取得
            var input = driver.FindElementByCssSelector("form div div div div div input");

            // 検索ボックスに selenium をセット
            input.SendKeys("selenium");

            // Enterキー押下
            input.SendKeys(Keys.Enter);
            Assert.IsTrue(driver.Url.Contains("https://www.google.com/search"));
            Assert.IsTrue(driver.Url.Contains("q=selenium"));

            // 検索結果をスクリーンショットに出力
            File.WriteAllBytes("GoogleSearchTest_result_0.png", driver.GetScreenshot().AsByteArray);

            // 次ページへのリンクを押下
            driver.FindElementById("pnnext").Click();
            Assert.IsTrue(driver.Url.Contains("https://www.google.com/search"));
            Assert.IsTrue(driver.Url.Contains("q=selenium"));
            Assert.IsTrue(driver.Url.Contains("start=10"));

            // 検索結果をスクリーンショットに出力
            File.WriteAllBytes("GoogleSearchTest_result_1.png", driver.GetScreenshot().AsByteArray);
        }

        [TestMethod]
        public void QiitaSearchTest()
        {
            using var driver = CreateDriver();

            // ウィンドウ最大化
            driver.Manage().Window.Maximize();

            // Qiitaにアクセス
            driver.Navigate().GoToUrl("https://qiita.com/");

            // 検索ボックス取得
            var input = driver.FindElementByClassName("st-Header_searchInput");

            // 検索ボックスに selenium をセット
            input.SendKeys("selenium");

            // Enterキー押下
            input.SendKeys(Keys.Enter);
            Assert.AreEqual("https://qiita.com/search?q=selenium", driver.Url);

            // 検索結果をスクリーンショットに出力
            File.WriteAllBytes("QiitaSearchTest_result_0.png", driver.GetScreenshot().AsByteArray);

            // 次ページへのリンクを押下
            driver.FindElementByClassName("js-next-page-link").Click();
            Assert.AreEqual("https://qiita.com/search?page=2&q=selenium", driver.Url);

            // 検索結果をスクリーンショットに出力
            File.WriteAllBytes("QiitaSearchTest_result_1.png", driver.GetScreenshot().AsByteArray);
        }

        private ChromeDriver CreateDriver()
        {
            var options = new ChromeOptions();
            options.AddArgument("--headless");
            options.AddArgument("--window-size=1920,1080");

            return new ChromeDriver(options);
        }
    }
}
