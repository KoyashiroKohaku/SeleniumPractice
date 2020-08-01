using System.IO;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumPractice
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void QiitaSearchTest()
        {
            using var chromeDriver = new ChromeDriver();

            // ウィンドウ最大化
            chromeDriver.Manage().Window.Maximize();

            // Qiitaにアクセス
            chromeDriver.Navigate().GoToUrl(@"https://qiita.com/");

            // 検索ボックス取得
            var input = chromeDriver.FindElementByClassName("st-Header_searchInput");

            // 検索ボックスに selenium をセット
            input.SendKeys("selenium");

            // Enterキー押下
            input.SendKeys(Keys.Enter);
            Assert.AreEqual(@"https://qiita.com/search?q=selenium", chromeDriver.Url);

            // 検索結果をスクリーンショットに出力
            File.WriteAllBytes("QiitaSearchTest_result_0.png", chromeDriver.GetScreenshot().AsByteArray);

            // 次ページへのリンクを押下
            chromeDriver.FindElementByClassName("js-next-page-link").Click();
            Assert.AreEqual(@"https://qiita.com/search?page=2&q=selenium", chromeDriver.Url);

            // 検索結果をスクリーンショットに出力
            File.WriteAllBytes("QiitaSearchTest_result_1.png", chromeDriver.GetScreenshot().AsByteArray);
        }
    }
}
