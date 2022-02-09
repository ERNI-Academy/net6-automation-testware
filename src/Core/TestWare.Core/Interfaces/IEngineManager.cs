using TestWare.Core.Configuration;

namespace TestWare.Core.Interfaces;

public interface IEngineManager : ITestWareComponent
{
    string GetEngineName();
    void Initialize(IEnumerable<string> tags, TestConfiguration testConfiguration);
    void Destroy();
    string CollectEvidence(string destinationPath, string evidenceName);
}
