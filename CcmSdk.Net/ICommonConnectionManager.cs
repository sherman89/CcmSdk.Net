using CcmSdk.Net.DTO;
using CcmSdk.Net.Structs;

namespace CcmSdk.Net
{
    public interface ICommonConnectionManager
    {
        CcmErrorCode Initialize(string connectionName);
        CcmErrorCode Uninitialize();
        CcmErrorCode GetCcmSdkAttributes(out CcmNameValuePair[] attributes);
        CcmErrorCode EnumerateConnections(out int[] connectionIds);
        CcmErrorCode SetConnectionAttributes(int connectionId, CcmNameValuePair[] attributes);
        CcmErrorCode GetConnectionAttributes(int connectionId, out CcmNameValuePair[] attributes);
        CcmErrorCode DeleteConnectionAttribute(int connectionId, CcmNameValuePair attribute);
        CcmErrorCode LaunchApplication(CcmNameValuePair[] sessionParams, out int sessionId);
        CcmErrorCode LaunchPublishedApplication(int sessionId, string appName, string arguments);
        CcmErrorCode EnumerateApplications(out IcaApplication[] applications);
        CcmErrorCode GetApplicationInfo(int applicationId, out IcaApplication application);
        CcmErrorCode TerminateApplication(int applicationId);
        CcmErrorCode SetSessionAttributes(int sessionId, CcmNameValuePair[] attributes);
        CcmErrorCode GetSessionAttributes(int sessionId, out CcmNameValuePair[] attributes);
        CcmErrorCode DeleteSessionAttribute(int sessionId, CcmNameValuePair attribute);
        CcmErrorCode EnumerateSessions(out CcmIcaSession[] sessions);
        CcmErrorCode GetSessionInfo(int sessionId, out CcmIcaSession sessionInfo);
        CcmErrorCode DisconnectSession(int sessionId);
        CcmErrorCode LogoffSession(int sessionId);
        CcmErrorCode DisconnectAllSessions();
        CcmErrorCode LogoffAllSessions();
        CcmErrorCode FullScreenSession(int sessionId);
        CcmErrorCode ForeGroundApplication(int applicationId);
        CcmErrorCode GetErrorMessageStringForCode(int sessionId, uint errorCode, out string errorMessage);
        CcmErrorCode GetActiveSessionCount(out uint activeSessions);
        CcmErrorCode DisconnectUserSessions(string userName, string domain);
        CcmErrorCode GetActiveSessionCountForUser(string userName, string domain, out uint activeUserSessions);
    }
}