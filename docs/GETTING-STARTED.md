# Getting Started

## Requirements
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/)
- [.net 6.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)

## How to use it
1. Clone this repository:
```
git clone https://github.com/ERNI-Academy/net6-automation-testware.git
```
2. Open the solution file *(TestWare.sln)* with Visual Studio 2022.
3. Build the solution.
4. Set proper values at the **TestConfiguration.[Sample].json** configuration files of 
    - **TestWare.Samples.API**
    
        No extra configuration is needed.

    - **TestWare.Samples.Appium.Mobile**
        - AppiumUrl: 
        - AppPath:
        - DeviceName:
        - PlatformName:

    - **TestWare.Samples.Selenium.Web**
        - Path:
        - Driver:

    - **TestWare.Samples.WinAppDriver.Desktop**
        - ApplicationPath:
        - ApplicationName:
        - WinAppDriverUrl

4. Run any of the tests from the Sample Projects.