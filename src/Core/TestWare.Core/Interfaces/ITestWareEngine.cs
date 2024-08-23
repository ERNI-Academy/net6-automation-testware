namespace TestWare.Core.Interfaces;

public interface ITestWareEngine
{ 
    void Initialize();
    void Dispose();
    string CollectEvidence(string destinationPath, string evidenceName);

    void StartRecordingEvidences();

    string StopRecordingEvidences(string destinationPath, string evidenceName);
}