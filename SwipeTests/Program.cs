using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.MultiTouch;
using OpenQA.Selenium.Appium.Service;

AndroidDriver<AppiumWebElement> _driver;
AppiumLocalService _appiumLocalService;

ClassInitialize();

TouchActionTest();

void TouchActionTest()
{
    int lastx = 100;
    int lasty = 500;
    Random r = new Random();
    for(int i = 0; i < 20; i++)
    {
        int nextx = lastx + r.Next(0,50);
        int nexty = lasty + r.Next(-75, 75);
        do
        {
            nexty = lasty + r.Next(-50, 50);
        } while (nexty < 400 || nexty > 600);
        
        new TouchAction(_driver).LongPress(lastx, lasty).MoveTo(nextx, nexty).Release().Perform();
        lasty = nexty;
        lastx = nextx;
    }

    //new TouchAction(_driver).LongPress(100, 500).MoveTo(600,600).Release().Perform();

    //new TouchAction(_driver).LongPress(600, 600).MoveTo(600, 900).Release().Perform();
    Thread.Sleep(1000);
}

void ClassInitialize()
{
    try
    {
        _appiumLocalService = new AppiumServiceBuilder()
            .WithAppiumJS(new FileInfo("C:\\Users\\User\\AppData\\Roaming\\npm\\node_modules\\appium\\build\\lib\\main.js"))
            .WithLogFile(new FileInfo("appiumlog.txt"))
            .WithIPAddress("127.0.0.1")
            .UsingPort(4723)
            //.UsingAnyFreePort()
            .WithStartUpTimeOut(new TimeSpan(200000000))
            .Build();
        _appiumLocalService.Start();
        var appiumOptions = new AppiumOptions();
        appiumOptions.AddAdditionalCapability(MobileCapabilityType.PlatformName, "Android");
        _driver = new AndroidDriver<AppiumWebElement>(_appiumLocalService, appiumOptions);
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
        ClassInitialize();
    }
}