using TestWare.Engines.Selenium.Factory;

namespace TestWare.Engines.Selenium.Configuration;

internal class Capabilities
{
    public string Name { get; set; }

    public string Path { get; set; }

    public string Driver { get; set; }
    public int CommandTimeOutInMinutes { get; set; }

<<<<<<< HEAD
=======
internal class Capabilities
{
    public string Name { get; set; }

    public string Path { get; set; }

    public string Driver { get; set; }

    public string BaseUrl { get; set; }
    public int CommandTimeOutInMinutes { get; set; }

>>>>>>> main
    public IEnumerable<string> Arguments { get; set; }  = Enumerable.Empty<string>();
    
    public SupportedBrowsers GetDriver()
    {
       return Enum.Parse<SupportedBrowsers>(Driver, true);
    }
}
