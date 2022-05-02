using System.Runtime.InteropServices;

namespace TestWare.Engines.Appium.WinAppDriver.UniversalWindowsPlatform;

[ComImport, Guid("2e941141-7f97-4756-ba1d-9decde894a3d"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
interface IApplicationActivationManager
{
    // Activates the specified immersive application for the "Launch" contract, passing the provided arguments
    // string into the application.  Callers can obtain the process Id of the application instance fulfilling this contract.
    IntPtr ActivateApplication([In] String appUserModelId, [In] String arguments, [In] ActivateOptions options, [Out] out UInt32 processId);
    IntPtr ActivateForFile([In] String appUserModelId, [In] IntPtr /*IShellItemArray* */ itemArray, [In] String verb, [Out] out UInt32 processId);
    IntPtr ActivateForProtocol([In] String appUserModelId, [In] IntPtr /* IShellItemArray* */itemArray, [Out] out UInt32 processId);
}
