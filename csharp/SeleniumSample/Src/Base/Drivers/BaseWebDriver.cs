
using System.Diagnostics;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Threading;
using SeleniumExtras.WaitHelpers;
using Common;

using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace Base.Drivers
{
    class BaseWebDriver
    {

        // コンストラクタを定義します。
        public BaseWebDriver()
        {
            this.driver = this.StartDriver();
        }

        public ChromeDriver driver;

        public ChromeDriver StartDriver()
        {
            // TODO:自動更新のためのコード。ただしダウンロード先が実行フォルダ直下になるためコメントアウト中
            // new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);

            var options = new ChromeOptions();
            options.AddArguments("--lang=ja-JP");
            // options.AddAdditionalChromeOption("detach", true);
            ChromeDriver driver = new ChromeDriver(options);
            driver.Manage().Window.Maximize();
            return driver;
        }

        public void StopDriver()
        {
            this.driver.Quit();
        }

        public void LogCurrentUrl()
        {
            Debug.WriteLine("current URL: " + this.driver.Url);
        }

        public void GetScreenshot(string id = "temp", string prefix = "", string suffix = "")
        {
            var now = DateTime.Today;
            var timestamp = now.ToString("yyyyMMddHHmmssfff");
            prefix = !string.IsNullOrEmpty(prefix) ? $"{prefix}_" : "";
            suffix = !string.IsNullOrEmpty(prefix) ? $"_{suffix}" : "";
            var fileName = $"{id}_{prefix}{timestamp}{suffix}.png";
            var dirPath = $"{CommonFile.GetCwd()}/screenshots/";
            var filePath = $"{dirPath}${fileName}";

            if (!CommonFile.ExistsPath(dirPath)){
                CommonFile.CreateDirectory(dirPath);
            }

            Console.WriteLine($"screenshot:{filePath}");
            Screenshot screenshot = (driver as ITakesScreenshot).GetScreenshot();
            screenshot.SaveAsFile(filePath, ScreenshotImageFormat.Png);
        }


        public void SwitchToWindow(int index)
        {
            var windows = this.driver.WindowHandles;
            if (index == -1)
            {
                index = windows.Count - 1;
            }
            this.driver.SwitchTo().Window(windows[index]);
        }

        public IWebElement FindWaitClickableElement(By selector, double waitSeconds = 10)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(waitSeconds));
            IWebElement firstResult = wait.Until(ExpectedConditions.ElementToBeClickable((selector)));
            IWebElement elm = (IWebElement)firstResult;
            return elm;
        }

        public IWebElement FindWaitLocatedElement(By selector, double waitSeconds = 10)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(waitSeconds));
            var firstResult = wait.Until(ExpectedConditions.ElementExists((selector)));
            IWebElement elm = (IWebElement)firstResult;
            return elm;
        }

        public void WaitLoading(By selector, double waitSeconds = 10)
        {
            // 判定用のエレメントが読込されるまで待機する
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(waitSeconds));
            IWebElement firstResult = wait.Until(e => e.FindElement(selector));
        }

        public void Wait(double waitSeconds = 1)
        {
            Thread.Sleep((int)(waitSeconds * 1000));
        }

        public void ClearText(IWebElement elm)
        {
            elm.Clear();
        }

        public void InputText(IWebElement elm, string? value)
        {
            this.ClearText(elm);
            elm.SendKeys(value);
        }

        public void InputTextAdd(IWebElement elm, string? value)
        {
            elm.SendKeys(value);
        }

        public void InputCheckbox(IWebElement elm, bool isCheck)
        {
            var isChecked = elm.Selected;
            if (isChecked == isCheck)
            {
                return;
            }
            elm.Click();
        }

        public void InputSelect(IWebElement elm, string? value)
        {
            var select = new SelectElement(elm);
            select.SelectByValue(value);
        }

        public void InputRadio(List<IWebElement> elmList, string? value)
        {
            IWebElement elm;
            foreach (IWebElement x in elmList)
            {
                if (x.GetAttribute("value") == value)
                {
                    elm = x;
                    elm.Click();
                }
            }
        }
        public void InputRadio(string name, string? value)
        {
            var elmList = this.driver.FindElements(By.Name(name)).ToList<IWebElement>();
            this.InputRadio(elmList, value);
        }

        public void SelectRadio(IWebElement elm)
        {
            elm.Click();
        }

    }
}