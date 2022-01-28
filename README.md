# About 
<!-- ALL-CONTRIBUTORS-BADGE:START - Do not remove or modify this section -->
[![All Contributors](https://img.shields.io/badge/all_contributors-2-orange.svg?style=flat-square)](#contributors-)
<!-- ALL-CONTRIBUTORS-BADGE:END -->
Testware is a flexible solution that implement the interaction with different automation engines. Providing a robust and scalable core that could be shared by different automation projects

By using this testware any automation project abstract the automation implementation. The automation project that uses the testware only need to focus on its business needs.

This solution comes from the need to standarize and reuse the common usage and extension of the different automation engines. With this action the maintenance decrease and the robustness increase.

## Built With
 - .net 6.0
 - Selenium 4.+
 - appium 5.+
 - restsharp 107.+


# Features
Testware provides capabilities to automate:
 - Websites (using selenium)
 - Mobile Applications (using appium)
 - Windows Desktop applications (using winappdriver)
 - Apis Rest (using restsharp)

Testware provide capabilities to report:
 - HTML (using extent report)

Evidence collection:
 - Screenshots after each step (for web, mobile applications and windows desktop applications)

# Getting Started

1. Clone this repository
2. Open solution with visual studio
3. Build the solution

## Start automation project

1. Create a test project using any desired runner.
2. Implement a class for handle the execution life cycle

```cs
 class LifeCycle : AutomationLifeCycleBase
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

- it should inherit from AutomationLifeCycleBase
- GetTestWareComponentAssemblies: return the list of the assemblies that contains the testwarecomponents
- GetTestWareEngines: Return the instances of the engines that will be used by this automation project
- GetConfiguration: Return a configuration object for the whole project

3. Add calls to the lifecycle class on the execution life cycles according
- BeginTestExecution: Once at the very begining of execution. It initialize the core.
- BeginTestSuite: Once at the begining of the test suite/feature (optional)
- BeginTestCase: Once at the begining of a test case. It Initialize the Engines
- BeginTestStep: Once at the begining of test step (optional)
- EndTestStep: Once at the end of test step. It generate evidences of execution (optional)
- EndTestCase: Once at the end of test case. It dispose the engine
- EndTestSuite: Once at the end of test suite.
- EndTestExecution: Once at the end of the execution

4. Implement business automation objects (i.e pages)

```cs
 public interface IBusinessPage : ITestWareComponent
    {
        void BusinessAction();
        void BusinessAssertion();
    }
```
- Components should inherit from ITestWareComponent in order to be registered

5. Design test cases. At any place is it possible to resolve to business objects
```cs
        using (var scope = ContainerManager.Container.BeginLifetimeScope())
        {
            businessPage = scope.Resolve<IBusinessPage>();
            businessPage.BusinessAction();
        }
```


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
            "Options": {
                "fullReset": false,
                "noReset": true,
                "appActivity": "com.swaglabsmobileapp.MainActivity",
                "unicodeKeyboard": true,
                "resetKeyboard": true,
                "autoAcceptAlerts": true,
                "autoGrantPermissions": true,
                "newCommandTimeout": 500
            }
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

# Contributing

Please see our [Contribution Guide](CONTRIBUTING.md) to learn how to contribute.

# License

[MIT](LICENSE) © 2022 [ERNI - Swiss Software Engineering](https://www.betterask.erni)

**Contact:** 

Erni Services  - [@ERNI](https://twitter.com/ERNI) - esp-services@betterask.erni

## Contributors ✨


<!-- ALL-CONTRIBUTORS-LIST:START - Do not remove or modify this section -->
<!-- prettier-ignore-start -->
<!-- markdownlint-disable -->
<table>
  <tr>
    <td align="center"><a href="https://github.com/LopezMDidac"><img src="https://avatars.githubusercontent.com/u/20030140?v=4?s=100" width="100px;" alt=""/><br /><sub><b>Didac Lopez</b></sub></a><br /><a href="https://github.com/ERNI-Academy/net6-automation-testware/commits?author=LopezMDidac" title="Code">💻</a> <a href="#content-LopezMDidac" title="Content">🖋</a> <a href="https://github.com/ERNI-Academy/net6-automation-testware/commits?author=LopezMDidac" title="Documentation">📖</a> <a href="#design-LopezMDidac" title="Design">🎨</a> <a href="#ideas-LopezMDidac" title="Ideas, Planning, & Feedback">🤔</a> <a href="#maintenance-LopezMDidac" title="Maintenance">🚧</a> <a href="https://github.com/ERNI-Academy/net6-automation-testware/commits?author=LopezMDidac" title="Tests">⚠️</a> <a href="#example-LopezMDidac" title="Examples">💡</a> <a href="https://github.com/ERNI-Academy/net6-automation-testware/pulls?q=is%3Apr+reviewed-by%3ALopezMDidac" title="Reviewed Pull Requests">👀</a></td>
    <td align="center"><a href="https://github.com/mg-diego"><img src="https://avatars.githubusercontent.com/u/39908763?v=4?s=100" width="100px;" alt=""/><br /><sub><b>mg-diego</b></sub></a><br /><a href="https://github.com/ERNI-Academy/net6-automation-testware/commits?author=mg-diego" title="Code">💻</a></td>
  </tr>
</table>

<!-- markdownlint-restore -->
<!-- prettier-ignore-end -->

<!-- ALL-CONTRIBUTORS-LIST:END -->

Thanks goes to these wonderful people ([emoji key](https://allcontributors.org/docs/en/emoji-key)):



<!-- ALL-CONTRIBUTORS-LIST:START - Do not remove or modify this section -->
<!-- ALL-CONTRIBUTORS-LIST:END -->
This project follows the [all-contributors](https://github.com/all-contributors/all-contributors) specification. Contributions of any kind welcome!