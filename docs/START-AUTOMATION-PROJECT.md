## Start automation project

1. Create a test project using any desired runner.
2. Implement a **LifeCycle** handler class
    - It should inherit from **AutomationLifeCycleBase**.
    - **GetTestWareComponentAssemblies:** Returns the assemblies list that contains the TestWareComponents.
    - **GetTestWareEngines:** Returns the engines instances that will be used at the current Automation project.
    - **GetConfiguration:** Returns the [configuration](#configuration) object defined at .json file. 

```cs
 public class LifeCycle : AutomationLifeCycleBase
    {
        protected override IEnumerable<Assembly> GetTestWareComponentAssemblies()
        {
            IEnumerable<Assembly> assemblies = new[]
            {
                typeof(LifeCycle).Assembly
            };

            return assemblies;
        }

        protected override IEnumerable<IEngineManager> GetTestWareEngines()
        {
            IEnumerable<IEngineManager> engines = new[]
            {
                new SeleniumManager()
            };

            return engines;
        }

        protected override TestConfiguration GetConfiguration()
        {
            var configManager = new ConfigurationManager();
            return configManager.ReadConfigurationFile("TestConfiguration.Web.json");
        }
    }
```


3. Add calls to the lifecycle class on the execution life cycles according
    - **BeginTestExecution:** Once at the very begining of execution. It initializes the Core.
        - **BeginTestSuite _(optional)_:** Once at the begining of the test suite/feature. 
            - **BeginTestCase:** Once at the begining of a test case. It Initialize the Engines
                - **BeginTestStep _(optional)_:** Once at the begining of test step.
                - **EndTestStep _(optional)_:** Once at the end of test step. It generate evidences of execution (optional)
            - **EndTestCase:** Once at the end of test case. It dispose the engine
        - **EndTestSuite:** Once at the end of test suite.
    - **EndTestExecution:** Once at the end of the execution
    

4. Implement business automation objects _(i.e Pages)_
    - Components should inherit from ITestWareComponent in order to be registered
```cs
 public interface IBusinessPage : ITestWareComponent
    {
        void BusinessAction();
        void BusinessAssertion();
    }
```


5. Design test cases. _(Is it possible to access the business objects resolving from the ContainerManager)_
```cs
        using (var scope = ContainerManager.Container.BeginLifetimeScope())
        {
            businessPage = scope.Resolve<IBusinessPage>();
            businessPage.BusinessAction();
        }
```

<a name="configuration"></a>
## Configuration file example

```json
{
    "Configurations": [
        {
            "Tag": "API",
            "Capabilities": [
                {
                    "Name": "API",
                    "BaseUrl": "https://newton.vercel.app/api/v2/",
                    "Timeout": 3000
                }
            ]
        },
        {
            "Tag": "WebDriver",
            "Capabilities": [
                {
                    "Name": "WebDriver",
                    "Path": "C:\\workspace\\Drivers",
                    "Driver": "Chrome",
                    "CommandTimeOutInMinutes": 5,
                    "Arguments": [
                        "--start-maximized"
                    ]
                }
            ]
        },
        {
            "Name": "Appiumdriver",
            "AppiumUrl": "http://127.0.0.1:4723/wd/hub",
            "ApkUrl": "https://github.com/saucelabs/sample-app-mobile/releases/download/2.7.1/Android.SauceLabs.Mobile.Sample.app.2.7.1.apk",
            "ApkPath": "C:\\workspace\\services\\AutomationFramework\\AutomationFramework.DataEntities\\Binaries\\SwagLabs.apk",
            "CommandTimeOutInMinutes": 5,
            "DeviceName": "emulator-5554",
            "PlatformName": "Android",
            "Options": [
                {
                "Name": "fullReset",
                "Value": false
                },
                {
                "Name": "noReset",
                "Value": true
                },
                {
                "Name": "appActivity",
                "Value": "com.swaglabsmobileapp.MainActivity"
                },
                {
                "Name": "unicodeKeyboard",
                "Value": true
                },
                {
                "Name": "resetKeyboard",
                "Value": true
                },
                {
                "Name": "autoAcceptAlerts",
                "Value": true
                },
                {
                "Name": "autoGrantPermissions",
                "Value": true
                },
                {
                "Name": "newCommandTimeout",
                "Value": 500
                }
            ]
        },
        {
            "Tag": "WinAppDriver",
            "Capabilities": [

                {
                    "Name":  "Notepad",
                    "ApplicationPath": "C:\\Windows\\System32\\notepad.exe",
                    "ApplicationName": "Notepad",
                    "ApplicationClassName": "Notepad",
                    "WinAppDriverUrl": "http://127.0.0.1:4723/wd/hub",
                    "CommandTimeOutInMinutes": 5
                },
                {
                    "Name":  "Calculator",
                    "ApplicationPath": "C:\\Windows\\System32\\calc.exe",
                    "ApplicationName": "Calculator",
                    "ApplicationClassName": "",
                    "WinAppDriverUrl": "http://127.0.0.1:4723/wd/hub",
                    "CommandTimeOutInMinutes": 5
                }
            ]
        }    
    ],
    "TestResultPath": "C:\\workspace\\ERNI\\results\\"
}
```