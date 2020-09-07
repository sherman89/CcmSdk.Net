# CcmSdk.Net
.NET wrapper for Citrix CCM SDK

## Setup
Copy the native CCM SDK libraries to the same directory where the wrapper resides. Use `CCMSDK64.dll` and `CCMProxy64.dll` for 64-bit, and `CCMSDK.dll` and `CCMProxy.dll`
for 32-bit version. Which DLL is loaded depends on whether the calling process is 64-bit or not.

If Citrix Workspace is installed, the DLL's can be found in `C:\Program Files (x86)\Citrix\ICA Client`

## Example usage
```csharp
// using CcmSdk.Net;
static void Main(string[] args)
{
    var ccm = new CommonConnectionManager();

    try
    {
        var initResult = ccm.Initialize("TestConnection");

        if (ccm.Initialized)
        {
            var enumerateResult = ccm.EnumerateApplications(out var activeApps);
            if (enumerateResult == CcmErrorCode.CCM_OK)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Active Applications:{Environment.NewLine}--------------------");

                for (var i = 0; i < activeApps.Length; i++)
                {
                    Console.WriteLine($"{i + 1}. {activeApps[i].FriendlyName}");
                }
            }
            else
            {
                Console.WriteLine($"Failed to enumerate active applications. CcmErrorCode: {enumerateResult}");
            }
        }
        else
        {
            Console.WriteLine($"Failed to initialize CCM SDK. CcmErrorCode: {initResult}");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Unexpected error occured: {ex.Message}");
    }
    finally
    {
        if (ccm.Initialized)
        {
            ccm.Uninitialize();
        }
    }
}
```

## CCM SDK API Reference
For developers, the API reference can be [found here](https://developer-docs.citrix.com/projects/workspace-app-for-windows-ccm-sdk/en/latest/reference/).

## Misc
I'm using `.NET Standard 2.1` so I can take advantage of the automatic `UTF-8` marshaling using `UnmanagedType.LPUTF8Str`. I'm planning on downgrading to 2.0 and
doing the marshaling manually for better .NET Framework compatibility.
