using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using System.IO;
using System.Diagnostics;

namespace Test4A.EdgeDriverTest
{
    [TestClass]
    public class EdgeDriverTest
    {
        // In order to run the below test(s), 
        // please follow the instructions from http://go.microsoft.com/fwlink/?LinkId=619687
        // to install Microsoft WebDriver.

        private EdgeDriver _driver;
        Process _process;

        [TestInitialize]
        public void EdgeDriverInitialize()
        {
            // Initialize edge driver 
            var options = new EdgeOptions
            {
                PageLoadStrategy = PageLoadStrategy.Normal
            };
            string driverdir = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "..", "..", ".."));
            string projectdir = Path.GetFullPath(Path.Combine(driverdir,  "..", "Test4A"));
            _process = Process.Start(new ProcessStartInfo
            {
                FileName = Path.Combine(projectdir, "bin", "Debug", "net5.0", "Test4A.exe"),
                UseShellExecute = true,
                WorkingDirectory = projectdir,
            });
            EdgeDriverService service = EdgeDriverService.CreateDefaultService(driverdir, "msedgedriver.exe");
            _driver = new EdgeDriver(service, options);
        }

        [TestMethod]
        public void VerifyPageTitle()
        {
            // Replace with your own test logic
            _driver.Url = "http://localhost:5000";
            Assert.AreEqual("The Star Wars films - Test4A", _driver.Title);
        }

        [TestCleanup]
        public void EdgeDriverCleanup()
        {
            _driver.Quit();
            try
            {
                _process.Kill();
            }
            catch {/*ignore exception*/}
            _process.Dispose();
        }
    }
}
