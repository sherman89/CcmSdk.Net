﻿using CcmSdk.Net.Structs;
using System;
using System.Runtime.InteropServices;

namespace CcmSdk.Net
{
    internal static class CcmSdk32
    {
        [DllImport("CCMSDK", CharSet = CharSet.Unicode, EntryPoint = "CCMInitialize", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        internal static extern int CCMInitialize([In] string pConnectionName);

        [DllImport("CCMSDK", CharSet = CharSet.Unicode, EntryPoint = "CCMUninitialize", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        internal static extern int CCMUninitialize();

        [DllImport("CCMSDK", CharSet = CharSet.Unicode, EntryPoint = "CCMGetCCMAttributes", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        internal static extern int CCMGetCCMAttributes([Out] out uint pCount, ref IntPtr ppAttributes);

        [DllImport("CCMSDK", CharSet = CharSet.Unicode, EntryPoint = "CCMEnumerateConnections", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        internal static extern int CCMEnumerateConnections([Out] out uint pCount, ref IntPtr ppConnections);

        [DllImport("CCMSDK", CharSet = CharSet.Unicode, EntryPoint = "CCMSetConnectionAttributes", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        internal static extern int CCMSetConnectionAttributes([In] int hConnection, [In] uint count, [In][MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] CcmNameValuePair[] pAttributes);

        [DllImport("CCMSDK", CharSet = CharSet.Unicode, EntryPoint = "CCMGetConnectionAttributes", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        internal static extern int CCMGetConnectionAttributes([In] int hConnection, [Out] out uint pCount, ref IntPtr ppAttributes);

        [DllImport("CCMSDK", CharSet = CharSet.Unicode, EntryPoint = "CCMDeleteConnectionAttribute", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        internal static extern int CCMDeleteConnectionAttribute([In] int hConnection, [In] CcmNameValuePair pAttribute);

        /// <summary>
        /// Launch application.
        /// </summary>
        /// <param name="count">Number of name-value pairs in <paramref name="pParams"/></param>
        /// <param name="pParams">Name-value pairs with session information</param>
        /// <param name="sessionId">Session ID</param>
        /// <returns>0 if success, otherwise error code</returns>
        [DllImport("CCMSDK", CharSet = CharSet.Unicode, EntryPoint = "CCMLaunchApplication", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        internal static extern int CCMLaunchApplication([In] uint count, [In] CcmNameValuePair[] pParams, [Out] out int hSession);

        [DllImport("CCMSDK", CharSet = CharSet.Unicode, EntryPoint = "CCMLaunchPublishedApplication", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        internal static extern int CCMLaunchPublishedApplication([In] int hSession, [In] string pAppName, [In] string pArguments);

        [DllImport("CCMSDK", CharSet = CharSet.Unicode, EntryPoint = "CCMEnumerateApplications", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        internal static extern int CCMEnumerateApplications([Out] out uint pCount, ref IntPtr ppICAApplications);

        [DllImport("CCMSDK", CharSet = CharSet.Unicode, EntryPoint = "CCMGetApplicationInfo", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        internal static extern int CCMGetApplicationInfo([In] int hApplication, ref IntPtr ppICAApplication);

        [DllImport("CCMSDK", CharSet = CharSet.Unicode, EntryPoint = "CCMTerminateApplication", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        internal static extern int CCMTerminateApplication([In] int hApplication);

        [DllImport("CCMSDK", CharSet = CharSet.Unicode, EntryPoint = "CCMSetSessionAttributes", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        internal static extern int CCMSetSessionAttributes([In] int hSession, [In] uint count, [In][MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] CcmNameValuePair[] pAttributes);

        [DllImport("CCMSDK", CharSet = CharSet.Unicode, EntryPoint = "CCMGetSessionAttributes", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        internal static extern int CCMGetSessionAttributes([In] int hSession, [Out] out uint pCount, ref IntPtr ppAttributes);

        [DllImport("CCMSDK", CharSet = CharSet.Unicode, EntryPoint = "CCMDeleteSessionAttribute", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        internal static extern int CCMDeleteSessionAttribute([In] int hSession, CcmNameValuePair pAttribute);

        [DllImport("CCMSDK", CharSet = CharSet.Unicode, EntryPoint = "CCMEnumerateSessions", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        internal static extern int CCMEnumerateSessions([Out] out uint pCount, ref IntPtr ppICASessions);

        [DllImport("CCMSDK", CharSet = CharSet.Unicode, EntryPoint = "CCMGetSessionInfo", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        internal static extern int CCMGetSessionInfo([In] int hSession, ref IntPtr ppICASession);

        [DllImport("CCMSDK", CharSet = CharSet.Unicode, EntryPoint = "CCMDisconnectSession", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        internal static extern int CCMDisconnectSession([In] int hSession);

        [DllImport("CCMSDK", CharSet = CharSet.Unicode, EntryPoint = "CCMLogoffSession", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        internal static extern int CCMLogoffSession([In] int hSession);

        [DllImport("CCMSDK", CharSet = CharSet.Unicode, EntryPoint = "CCMDisconnectAllSessions", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        internal static extern int CCMDisconnectAllSessions();

        [DllImport("CCMSDK", CharSet = CharSet.Unicode, EntryPoint = "CCMLogoffAllSessions", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        internal static extern int CCMLogoffAllSessions();

        [DllImport("CCMSDK", CharSet = CharSet.Unicode, EntryPoint = "CCMFullScreenSession", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        internal static extern int CCMFullScreenSession([In] int hSession);

        [DllImport("CCMSDK", CharSet = CharSet.Unicode, EntryPoint = "CCMForeGroundApplication", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        internal static extern int CCMForeGroundApplication([In] int hApplication);

        [DllImport("CCMSDK", CharSet = CharSet.Unicode, EntryPoint = "CCMGetErrorMessageStringForCode", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        internal static extern int CCMGetErrorMessageStringForCode([In] int hSession, [In] uint errorCode, ref IntPtr ppErrorString);

        [DllImport("CCMSDK", CharSet = CharSet.Unicode, EntryPoint = "CCMGetActiveSessionCount", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        internal static extern int CCMGetActiveSessionCount([Out] out uint pCount);

        [DllImport("CCMSDK", CharSet = CharSet.Unicode, EntryPoint = "CCMFreeNameValuePair", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        internal static extern int CCMFreeNameValuePair([In] uint count, [In] IntPtr pNVP);

        [DllImport("CCMSDK", CharSet = CharSet.Unicode, EntryPoint = "CCMFreeICASession", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        internal static extern int CCMFreeICASession([In] uint count, [In] IntPtr pSession);

        [DllImport("CCMSDK", CharSet = CharSet.Unicode, EntryPoint = "CCMFreeICAApplication", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        internal static extern int CCMFreeICAApplication([In] uint count, [In] IntPtr pApplication);

        [DllImport("CCMSDK", CharSet = CharSet.Unicode, EntryPoint = "CCMFreeMemory", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        internal static extern void CCMFreeMemory(IntPtr pVoid);

        [DllImport("CCMSDK", CharSet = CharSet.Unicode, EntryPoint = "CCMDisconnectUserSessions", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        internal static extern int CCMDisconnectUserSessions([In] string pUserName, [In] string pDomainName);

        [DllImport("CCMSDK", CharSet = CharSet.Unicode, EntryPoint = "CCMGetActiveSessionCountForUser", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        internal static extern int CCMGetActiveSessionCountForUser([In] string pUserName, [In] string pDomainName, [Out] out uint pCount);
    }
}
