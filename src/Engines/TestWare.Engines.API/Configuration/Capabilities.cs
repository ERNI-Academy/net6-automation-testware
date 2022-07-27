namespace TestWare.Engines.Restsharp.Configuration;

internal class Capabilities
{
    public string Name { get; set; }

    public string BaseUrl { get; set; }

    public int Timeout { get; set; }

    /// <summary>
    /// Gets or sets default query parameters used on every request.
    /// </summary>
    public IEnumerable<CapabilityParameter> QueryParameters { get; set; }

    /// <summary>
    /// Gets or sets default headers used on every request.
    /// </summary>
    public IEnumerable<CapabilityParameter> HeaderParameters { get; set; }

}
