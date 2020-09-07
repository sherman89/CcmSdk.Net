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

## Misc notes
- I'm using `.NET Standard 2.1` so I can take advantage of the automatic `UTF-8` marshaling using `UnmanagedType.LPUTF8Str`.
- By looking at some sample code from Citrix, I noticed a comment next to the `CCMChar` typedef that said `UTF-8`, which is why I'm assuming that's the correct encoding to use.
- Tested with version `20.8.0.24` of CCMSDK64.dll.

## Contributions
I'm no P/Invoke expert, so if you notice some mistakes, please do let me know! I also haven't had the resources to test all the functions and I'm only using Citrix very little, so any testing is also welcome. All I ever wanted was to fetch the client version programmatically, but I got a little excited and implemented the whole API...
