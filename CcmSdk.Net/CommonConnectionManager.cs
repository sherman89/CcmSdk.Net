using CcmSdk.Net.DTO;
using CcmSdk.Net.Structs;
using System;
using System.Runtime.InteropServices;

namespace CcmSdk.Net
{
    /// <summary>
    /// .NET Wrapper around CCM SDK. If process is 64-bit, looks for CCMSDK64.dll, otherwise CCMSDK.dll.
    /// </summary>
    public class CommonConnectionManager : ICommonConnectionManager
    {
        public bool Initialized { get; private set; }

        public CcmErrorCode Initialize(string connectionName)
        {
            var result = Environment.Is64BitProcess ?
                CcmSdk64.CCMInitialize(connectionName) :
                CcmSdk32.CCMInitialize(connectionName);

            if (result == (int)CcmErrorCode.CCM_OK)
            {
                Initialized = true;
            }

            return (CcmErrorCode)result;
        }

        public CcmErrorCode Uninitialize()
        {
            var result = Environment.Is64BitProcess ?
                CcmSdk64.CCMUninitialize() : 
                CcmSdk32.CCMUninitialize();

            if (result == (int)CcmErrorCode.CCM_OK)
            {
                Initialized = false;
            }

            return (CcmErrorCode)result;
        }

        public CcmErrorCode GetCcmSdkAttributes(out CcmNameValuePair[] attributes)
        {
            var ptrSize = Marshal.SizeOf(typeof(CcmNameValuePair));
            var ptr = IntPtr.Zero;

            var result = Environment.Is64BitProcess ?
                CcmSdk64.CCMGetCCMAttributes(out uint count, ref ptr) :
                CcmSdk32.CCMGetCCMAttributes(out count, ref ptr);

            attributes = new CcmNameValuePair[count];
            for (int i = 0; i < count; i++)
            {
                var nPtr = IntPtr.Add(ptr, i * ptrSize);
                attributes[i] = Marshal.PtrToStructure<CcmNameValuePair>(nPtr);
            }

            _ = Environment.Is64BitProcess ?
                CcmSdk64.CCMFreeNameValuePair(count, ptr) :
                CcmSdk32.CCMFreeNameValuePair(count, ptr);

            return (CcmErrorCode)result;
        }

        public CcmErrorCode EnumerateConnections(out int[] connectionIds)
        {
            var ptrSize = Marshal.SizeOf(typeof(int));
            var ptr = IntPtr.Zero;

            var result = Environment.Is64BitProcess ?
                CcmSdk64.CCMEnumerateConnections(out var count, ref ptr) :
                CcmSdk32.CCMEnumerateConnections(out count, ref ptr);

            connectionIds = new int[count];
            for (int i = 0; i < count; i++)
            {
                var nPtr = IntPtr.Add(ptr, i * ptrSize);
                connectionIds[i] = Marshal.ReadInt32(nPtr);
            }

            if (Environment.Is64BitProcess)
            {
                CcmSdk64.CCMFreeMemory(ptr);
            }
            else
            {
                CcmSdk32.CCMFreeMemory(ptr);
            }

            return (CcmErrorCode)result;
        }

        public CcmErrorCode SetConnectionAttributes(int connectionId, CcmNameValuePair[] attributes)
        {
            var result = Environment.Is64BitProcess ?
                CcmSdk64.CCMSetConnectionAttributes(connectionId, (uint)attributes.Length, attributes) :
                CcmSdk32.CCMSetConnectionAttributes(connectionId, (uint)attributes.Length, attributes);

            return (CcmErrorCode)result;
        }

        public CcmErrorCode GetConnectionAttributes(int connectionId, out CcmNameValuePair[] attributes)
        {
            var ptrSize = Marshal.SizeOf(typeof(CcmNameValuePair));
            var ptr = IntPtr.Zero;

            var result = Environment.Is64BitProcess ?
                CcmSdk64.CCMGetConnectionAttributes(connectionId, out var count, ref ptr) :
                CcmSdk32.CCMGetConnectionAttributes(connectionId, out count, ref ptr);

            attributes = new CcmNameValuePair[count];
            for (int i = 0; i < count; i++)
            {
                var nPtr = IntPtr.Add(ptr, i * ptrSize);
                attributes[i] = Marshal.PtrToStructure<CcmNameValuePair>(nPtr);
            }

            _ = Environment.Is64BitProcess ?
                CcmSdk64.CCMFreeNameValuePair(count, ptr) :
                CcmSdk32.CCMFreeNameValuePair(count, ptr);

            return (CcmErrorCode)result;
        }

        public CcmErrorCode DeleteConnectionAttribute(int connectionId, CcmNameValuePair attribute)
        {
            var result = Environment.Is64BitProcess ?
                CcmSdk64.CCMDeleteConnectionAttribute(connectionId, attribute) :
                CcmSdk32.CCMDeleteConnectionAttribute(connectionId, attribute);

            return (CcmErrorCode)result;
        }

        /// <summary>
        /// Launch application. Example param: { Name: ICA_FILE_PATH, Value: [Path_To_Ica_File] }
        /// </summary>
        /// <param name="sessionParams">Session information, such as ICA file to load.</param>
        /// <param name="sessionId">Session ID</param>
        /// <returns><see cref="CcmErrorCode.CCM_OK"/> if success, otherwise error code.</returns>
        public CcmErrorCode LaunchApplication(CcmNameValuePair[] sessionParams, out int sessionId)
        {
            var result = Environment.Is64BitProcess ?
                CcmSdk64.CCMLaunchApplication((uint)sessionParams.Length, sessionParams, out sessionId) :
                CcmSdk32.CCMLaunchApplication((uint)sessionParams.Length, sessionParams, out sessionId);

            return (CcmErrorCode)result;
        }

        public CcmErrorCode LaunchPublishedApplication(int sessionId, string appName, string arguments)
        {
            var result = Environment.Is64BitProcess ?
                CcmSdk64.CCMLaunchPublishedApplication(sessionId, appName, arguments) :
                CcmSdk32.CCMLaunchPublishedApplication(sessionId, appName, arguments);

            return (CcmErrorCode)result;
        }

        public CcmErrorCode EnumerateApplications(out IcaApplication[] applications)
        {
            var ptrSize = Marshal.SizeOf(typeof(CcmIcaApplication));
            var ptr = IntPtr.Zero;

            var result = Environment.Is64BitProcess ?
                CcmSdk64.CCMEnumerateApplications(out var count, ref ptr) :
                CcmSdk32.CCMEnumerateApplications(out count, ref ptr);

            applications = new IcaApplication[count];
            for (int i = 0; i < count; i++)
            {
                var nPtr = IntPtr.Add(ptr, i * ptrSize);
                var appStruct = Marshal.PtrToStructure<CcmIcaApplication>(nPtr);

                var appDto = new IcaApplication()
                {
                    ApplicationID = appStruct.ApplicationID,
                    SessionID = appStruct.SessionID,
                    FriendlyName = appStruct.FriendlyName,
                    Title = appStruct.Title,
                    ClassName = appStruct.ClassName,
                    IconByteArray = PinvokeHelper.GetIconBytesFromIntPtr(appStruct.hIcon)
                };

                applications[i] = appDto;
            }

            _ = Environment.Is64BitProcess ? 
                CcmSdk64.CCMFreeICAApplication(count, ptr) : 
                CcmSdk32.CCMFreeICAApplication(count, ptr);

            return (CcmErrorCode)result;
        }

        public CcmErrorCode GetApplicationInfo(int applicationId, out IcaApplication application)
        {
            var ptr = IntPtr.Zero;

            var result = Environment.Is64BitProcess ?
                CcmSdk64.CCMGetApplicationInfo(applicationId, ref ptr) :
                CcmSdk32.CCMGetApplicationInfo(applicationId, ref ptr);

            var appStruct = Marshal.PtrToStructure<CcmIcaApplication>(ptr);

            application = new IcaApplication()
            {
                ApplicationID = appStruct.ApplicationID,
                SessionID = appStruct.SessionID,
                FriendlyName = appStruct.FriendlyName,
                Title = appStruct.Title,
                ClassName = appStruct.ClassName,
                IconByteArray = PinvokeHelper.GetIconBytesFromIntPtr(appStruct.hIcon)
            };

            _ = Environment.Is64BitProcess ?
                CcmSdk64.CCMFreeICAApplication(0U, ptr) :
                CcmSdk32.CCMFreeICAApplication(0U, ptr);

            return (CcmErrorCode)result;
        }

        public CcmErrorCode TerminateApplication(int applicationId)
        {
            var result = Environment.Is64BitProcess ?
                CcmSdk64.CCMTerminateApplication(applicationId) :
                CcmSdk32.CCMTerminateApplication(applicationId);

            return (CcmErrorCode)result;
        }

        public CcmErrorCode SetSessionAttributes(int sessionId, CcmNameValuePair[] attributes)
        {
            var result = Environment.Is64BitProcess ?
                CcmSdk64.CCMSetSessionAttributes(sessionId, (uint)attributes.Length, attributes) :
                CcmSdk32.CCMSetSessionAttributes(sessionId, (uint)attributes.Length, attributes);

            return (CcmErrorCode)result;
        }

        public CcmErrorCode GetSessionAttributes(int sessionId, out CcmNameValuePair[] attributes)
        {
            var ptrSize = Marshal.SizeOf(typeof(CcmNameValuePair));
            var ptr = IntPtr.Zero;

            var result = Environment.Is64BitProcess ?
                CcmSdk64.CCMGetSessionAttributes(sessionId, out var count, ref ptr) :
                CcmSdk32.CCMGetSessionAttributes(sessionId, out count, ref ptr);

            attributes = new CcmNameValuePair[count];
            for (int i = 0; i < count; i++)
            {
                var nPtr = IntPtr.Add(ptr, i * ptrSize);
                attributes[i] = Marshal.PtrToStructure<CcmNameValuePair>(nPtr);
            }

            _ = Environment.Is64BitProcess ?
                CcmSdk64.CCMFreeNameValuePair(count, ptr) :
                CcmSdk32.CCMFreeNameValuePair(count, ptr);

            return (CcmErrorCode)result;
        }
        
        public CcmErrorCode DeleteSessionAttribute(int sessionId, CcmNameValuePair attribute)
        {
            var result = Environment.Is64BitProcess ?
                CcmSdk64.CCMDeleteSessionAttribute(sessionId, attribute) :
                CcmSdk32.CCMDeleteSessionAttribute(sessionId, attribute);

            return (CcmErrorCode)result;
        }
        
        public CcmErrorCode EnumerateSessions(out CcmIcaSession[] sessions)
        {
            var ptrSize = Marshal.SizeOf(typeof(CcmIcaSession));
            var ptr = IntPtr.Zero;

            var result = Environment.Is64BitProcess ?
                CcmSdk64.CCMEnumerateSessions(out var count, ref ptr) :
                CcmSdk32.CCMEnumerateSessions(out count, ref ptr);

            sessions = new CcmIcaSession[count];
            for (int i = 0; i < count; i++)
            {
                var nPtr = IntPtr.Add(ptr, i * ptrSize);
                sessions[i] = Marshal.PtrToStructure<CcmIcaSession>(nPtr);
            }

            _ = Environment.Is64BitProcess ?
                CcmSdk64.CCMFreeICASession(count, ptr) :
                CcmSdk32.CCMFreeICASession(count, ptr);

            return (CcmErrorCode)result;
        }
        
        public CcmErrorCode GetSessionInfo(int sessionId, out CcmIcaSession sessionInfo)
        {
            var ptr = IntPtr.Zero;

            var result = Environment.Is64BitProcess ?
                CcmSdk64.CCMGetSessionInfo(sessionId, ref ptr) :
                CcmSdk32.CCMGetSessionInfo(sessionId, ref ptr);

            sessionInfo = Marshal.PtrToStructure<CcmIcaSession>(ptr);

            _ = Environment.Is64BitProcess ?
                CcmSdk64.CCMFreeICASession(0U, ptr) :
                CcmSdk32.CCMFreeICASession(0U, ptr);

            return (CcmErrorCode)result;
        }
        
        public CcmErrorCode DisconnectSession(int sessionId)
        {
            var result = Environment.Is64BitProcess ?
                CcmSdk64.CCMDisconnectSession(sessionId) :
                CcmSdk32.CCMDisconnectSession(sessionId);

            return (CcmErrorCode)result;
        }
        
        public CcmErrorCode LogoffSession(int sessionId)
        {
            var result = Environment.Is64BitProcess ?
                CcmSdk64.CCMLogoffSession(sessionId) :
                CcmSdk32.CCMLogoffSession(sessionId);

            return (CcmErrorCode)result;
        }
        
        public CcmErrorCode DisconnectAllSessions()
        {
            var result = Environment.Is64BitProcess ?
                CcmSdk64.CCMDisconnectAllSessions() :
                CcmSdk32.CCMDisconnectAllSessions();

            return (CcmErrorCode)result;
        }
        
        public CcmErrorCode LogoffAllSessions()
        {
            var result = Environment.Is64BitProcess ?
                CcmSdk64.CCMLogoffAllSessions() :
                CcmSdk32.CCMLogoffAllSessions();

            return (CcmErrorCode)result;
        }

        public CcmErrorCode FullScreenSession(int sessionId)
        {
            var result = Environment.Is64BitProcess ?
                CcmSdk64.CCMFullScreenSession(sessionId) :
                CcmSdk32.CCMFullScreenSession(sessionId);

            return (CcmErrorCode)result;
        }

        public CcmErrorCode ForeGroundApplication(int applicationId)
        {
            var result = Environment.Is64BitProcess ?
                CcmSdk64.CCMForeGroundApplication(applicationId) :
                CcmSdk32.CCMForeGroundApplication(applicationId);

            return (CcmErrorCode)result;
        }

        public CcmErrorCode GetErrorMessageStringForCode(int sessionId, uint errorCode, out string errorMessage)
        {
            var ptr = IntPtr.Zero;

            var result = Environment.Is64BitProcess ?
                CcmSdk64.CCMGetErrorMessageStringForCode(sessionId, errorCode, ref ptr) :
                CcmSdk32.CCMGetErrorMessageStringForCode(sessionId, errorCode, ref ptr);

            errorMessage = Marshal.PtrToStringUTF8(ptr);

            if (Environment.Is64BitProcess)
            {
                CcmSdk64.CCMFreeMemory(ptr);
            }
            else
            {
                CcmSdk32.CCMFreeMemory(ptr);
            }

            return (CcmErrorCode)result;
        }
        
        public CcmErrorCode GetActiveSessionCount(out uint activeSessions)
        {
            var result = Environment.Is64BitProcess ?
                CcmSdk64.CCMGetActiveSessionCount(out activeSessions) :
                CcmSdk32.CCMGetActiveSessionCount(out activeSessions);

            return (CcmErrorCode)result;
        }
        
        public CcmErrorCode DisconnectUserSessions(string userName, string domain)
        {
            var result = Environment.Is64BitProcess ?
                CcmSdk64.CCMDisconnectUserSessions(userName, domain) :
                CcmSdk32.CCMDisconnectUserSessions(userName, domain);

            return (CcmErrorCode)result;
        }
        
        public CcmErrorCode GetActiveSessionCountForUser(string userName, string domain, out uint activeUserSessions)
        {
            var result = Environment.Is64BitProcess ?
                CcmSdk64.CCMGetActiveSessionCountForUser(userName, domain, out activeUserSessions) :
                CcmSdk32.CCMGetActiveSessionCountForUser(userName, domain, out activeUserSessions);

            return (CcmErrorCode)result;
        }
    }
}
