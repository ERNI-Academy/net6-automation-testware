[![Continuous-Integration](https://github.com/ERNI-Academy/net6-automation-testware/actions/workflows/CI.yml/badge.svg)](https://github.com/ERNI-Academy/net6-automation-testware/actions/workflows/CI.yml)

[![SonarQube Scanner](https://github.com/ERNI-Academy/net6-automation-testware/actions/workflows/Sonar_Scanner.yml/badge.svg?branch=main)](https://github.com/ERNI-Academy/net6-automation-testware/actions/workflows/Sonar_Scanner.yml)

<!-- ALL-CONTRIBUTORS-BADGE:START - Do not remove or modify this section -->
[![All Contributors](https://img.shields.io/badge/all_contributors-4-orange.svg?style=flat-square)](#contributors-)
<!-- ALL-CONTRIBUTORS-BADGE:END -->

# About

[Automation Testware](https://github.com/ERNI-Academy/net6-automation-testware) is a flexible solution that implements the interaction and management of the main automation engines for Web _(Selenium)_, Mobile _(Appium)_ and Desktop _(WinAppDriver)_ environments.

TestWare provides a robust and scalable core that can be reused by any automation project in order to abstract the core automation implementation focusing only on its business needs.

This solution comes from the need to standarize and reuse the common usage and extension of the different automation engines.

With this action the maintenance decreases and the robustness increases.

## Built With

- [.net 6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- [Selenium 4.+](https://www.selenium.dev/documentation/webdriver/getting_started/upgrade_to_selenium_4/)
- [Appium 5.+](https://appium.io/)
- [Restsharp 107.+](https://restsharp.dev/v107/#restsharp-v107)

# Features

Testware provides capabilities to automate:

- **Websites** (using Selenium)

  - Supported Browsers:  
        <img src="https://github.com/devicons/devicon/blob/master/icons/chrome/chrome-original-wordmark.svg" title="Chrome" alt="Chrome" width="40" height="40"/>
        <img src="https://github.com/devicons/devicon/blob/master/icons/firefox/firefox-original-wordmark.svg" title="Firefox" alt="Firefox" width="40" height="40"/>
        <img src="https://github.com/devicons/devicon/blob/master/icons/ie10/ie10-original.svg" title="IE" alt="IE" width="40" height="40"/>
        <img src="https://upload.wikimedia.org/wikipedia/commons/9/98/Microsoft_Edge_logo_%282019%29.svg" title="Edge" alt="Edge" width="40" height="40"/>

- **Mobile Applications** (using Appium)
- **Windows Desktop applications** (using WinAppDriver)
- **API Rest** (using Restsharp)

Testware provides capabilities to report:

- HTML (using extent report)

Evidence collection:

- Screenshots after each step (for web, mobile applications and windows desktop applications)

# Getting Started

1. Clone this repository
2. Open solution with visual studio
3. Build the solution

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

## Contributing

Please see our [Contribution Guide](CONTRIBUTING.md) to learn how to contribute.

## License

![MIT](https://img.shields.io/badge/License-MIT-blue.svg)

Copyright © 2022 [ERNI - Swiss Software Engineering](https://www.betterask.erni)

## Code of conduct

Please see our [Code of Conduct](CODE_OF_CONDUCT.md)

## Stats

![https://repobeats.axiom.co/api/embed/7ebe11822a109c9c80ee0470d57be9997ac78837.svg](https://repobeats.axiom.co/api/embed/7ebe11822a109c9c80ee0470d57be9997ac78837.svg)

## Follow us

[![Twitter Follow](https://img.shields.io/twitter/follow/ERNI?style=social)](https://www.twitter.com/ERNI)
[![Twitch Status](https://img.shields.io/twitch/status/erni_academy?label=Twitch%20Erni%20Academy&style=social)](https://www.twitch.tv/erni_academy)
[![YouTube Channel Views](https://img.shields.io/youtube/channel/views/UCkdDcxjml85-Ydn7Dc577WQ?label=Youtube%20Erni%20Academy&style=social)](https://www.youtube.com/channel/UCkdDcxjml85-Ydn7Dc577WQ)
[![Linkedin](https://img.shields.io/badge/linkedin-31k-green?style=social&logo=Linkedin)](https://www.linkedin.com/company/erni)

## Contact

📧 [esp-services@betterask.erni](mailto:esp-services@betterask.erni)

## Contributors ✨

<!-- ALL-CONTRIBUTORS-LIST:START - Do not remove or modify this section -->
<!-- prettier-ignore-start -->
<!-- markdownlint-disable -->
<table>
  <tr>
    <td align="center"><a href="https://github.com/LopezMDidac"><img src="https://avatars.githubusercontent.com/u/20030140?v=4?s=100" width="100px;" alt=""/><br /><sub><b>Didac Lopez</b></sub></a><br /><a href="https://github.com/ERNI-Academy/net6-automation-testware/commits?author=LopezMDidac" title="Code">💻</a> <a href="#content-LopezMDidac" title="Content">🖋</a> <a href="https://github.com/ERNI-Academy/net6-automation-testware/commits?author=LopezMDidac" title="Documentation">📖</a> <a href="#design-LopezMDidac" title="Design">🎨</a> <a href="#ideas-LopezMDidac" title="Ideas, Planning, & Feedback">🤔</a> <a href="#maintenance-LopezMDidac" title="Maintenance">🚧</a> <a href="https://github.com/ERNI-Academy/net6-automation-testware/commits?author=LopezMDidac" title="Tests">⚠️</a> <a href="#example-LopezMDidac" title="Examples">💡</a> <a href="https://github.com/ERNI-Academy/net6-automation-testware/pulls?q=is%3Apr+reviewed-by%3ALopezMDidac" title="Reviewed Pull Requests">👀</a></td>
    <td align="center"><a href="https://github.com/mg-diego"><img src="https://avatars.githubusercontent.com/u/39908763?v=4?s=100" width="100px;" alt=""/><br /><sub><b>mg-diego</b></sub></a><br /><a href="https://github.com/ERNI-Academy/net6-automation-testware/commits?author=mg-diego" title="Code">💻</a> <a href="#content-mg-diego" title="Content">🖋</a> <a href="https://github.com/ERNI-Academy/net6-automation-testware/commits?author=mg-diego" title="Documentation">📖</a> <a href="#design-mg-diego" title="Design">🎨</a> <a href="#ideas-mg-diego" title="Ideas, Planning, & Feedback">🤔</a> <a href="#maintenance-mg-diego" title="Maintenance">🚧</a> <a href="https://github.com/ERNI-Academy/net6-automation-testware/commits?author=mg-diego" title="Tests">⚠️</a> <a href="#example-mg-diego" title="Examples">💡</a> <a href="https://github.com/ERNI-Academy/net6-automation-testware/pulls?q=is%3Apr+reviewed-by%3Amg-diego" title="Reviewed Pull Requests">👀</a></td>
    <td align="center"><a href="https://github.com/Rabosa616"><img src="https://avatars.githubusercontent.com/u/12774781?v=4?s=100" width="100px;" alt=""/><br /><sub><b>Rabosa616</b></sub></a><br /><a href="https://github.com/ERNI-Academy/net6-automation-testware/commits?author=Rabosa616" title="Code">💻</a> <a href="#content-Rabosa616" title="Content">🖋</a> <a href="https://github.com/ERNI-Academy/net6-automation-testware/commits?author=Rabosa616" title="Documentation">📖</a> <a href="#design-Rabosa616" title="Design">🎨</a> <a href="#ideas-Rabosa616" title="Ideas, Planning, & Feedback">🤔</a> <a href="#maintenance-Rabosa616" title="Maintenance">🚧</a> <a href="https://github.com/ERNI-Academy/net6-automation-testware/commits?author=Rabosa616" title="Tests">⚠️</a> <a href="#example-Rabosa616" title="Examples">💡</a> <a href="https://github.com/ERNI-Academy/net6-automation-testware/pulls?q=is%3Apr+reviewed-by%3ARabosa616" title="Reviewed Pull Requests">👀</a></td>
    <td align="center"><a href="https://github.com/carmenavram"><img src="https://avatars.githubusercontent.com/u/39376956?v=4?s=100" width="100px;" alt=""/><br /><sub><b>Carmen Avram</b></sub></a><br /><a href="https://github.com/ERNI-Academy/net6-automation-testware/commits?author=carmenavram" title="Code">💻</a> <a href="#content-carmenavram" title="Content">🖋</a> <a href="https://github.com/ERNI-Academy/net6-automation-testware/commits?author=carmenavram" title="Documentation">📖</a> <a href="#design-carmenavram" title="Design">🎨</a> <a href="#ideas-carmenavram" title="Ideas, Planning, & Feedback">🤔</a> <a href="#maintenance-carmenavram" title="Maintenance">🚧</a> <a href="https://github.com/ERNI-Academy/net6-automation-testware/commits?author=carmenavram" title="Tests">⚠️</a> <a href="#example-carmenavram" title="Examples">💡</a> <a href="https://github.com/ERNI-Academy/net6-automation-testware/pulls?q=is%3Apr+reviewed-by%3Acarmenavram" title="Reviewed Pull Requests">👀</a></td>
  </tr>
</table>

<!-- markdownlint-restore -->
<!-- prettier-ignore-end -->

<!-- ALL-CONTRIBUTORS-LIST:END -->

Thanks goes to these wonderful people ([emoji key](https://allcontributors.org/docs/en/emoji-key)):

<!-- ALL-CONTRIBUTORS-LIST:START - Do not remove or modify this section -->
<!-- ALL-CONTRIBUTORS-LIST:END -->
This project follows the [all-contributors](https://github.com/all-contributors/all-contributors) specification. Contributions of any kind welcome!
