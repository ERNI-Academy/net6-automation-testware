using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWare.Core.Configuration;

namespace TestWare.Core.Interfaces;

public interface ITestWareEngine
{ 
    void Initialize();
    void Dispose();
    string CollectEvidence(string destinationPath, string evidenceName);

    void StartRecordingEvidences();

    string StopRecordingEvidences(string destinationPath, string evidenceName);
}