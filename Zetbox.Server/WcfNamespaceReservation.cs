
namespace Zetbox.Server
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Security.Principal;
    using System.Text;
    using System.Xml;

    /// <summary>
    /// Utility class to hold everything neccessary to modify the HTTP ACLs before starting the WCF server
    /// </summary>
    /// Originally from http://www.pluralsight.com/community/blogs/keith/archive/2005/10/17/15632.aspx
    public static class WcfNamespaceReservation
    {
        /// <summary>
        /// Adds or removes an entry in the HTTP ACLs for a given prefix.
        /// </summary>
        /// <param name="urlPrefix">the prefix whose ACL should be modified</param>
        /// <param name="accountName">the account name to be added or removed from the ACL</param>
        /// <param name="removeReservation">whether access should be granted or denied</param>
        public static void ModifyReservation(
            string urlPrefix,
            string accountName,
            bool removeReservation)
        {
            string sddl = CreateSddl(accountName);

            HTTP_SERVICE_CONFIG_URLACL_SET configInfo;
            configInfo.Key.UrlPrefix = urlPrefix;
            configInfo.Param.Sddl = sddl;

            HTTPAPI_VERSION httpApiVersion = new HTTPAPI_VERSION(1, 0);

            int errorCode = HttpInitialize(httpApiVersion, HTTP_INITIALIZE_CONFIG, IntPtr.Zero);

            CheckResult("HttpInitialize", errorCode);

            try
            {
                // do our best to delete any existing ACL
                errorCode = HttpDeleteServiceConfigurationAcl(
                    IntPtr.Zero,
                    HttpServiceConfigUrlAclInfo,
                    ref configInfo,
                    Marshal.SizeOf(typeof(HTTP_SERVICE_CONFIG_URLACL_SET)),
                    IntPtr.Zero);
                if (removeReservation)
                {
                    CheckResult(
                           "HttpDeleteServiceConfigurationAcl",
                           errorCode);
                    return;
                }
                errorCode = HttpSetServiceConfigurationAcl(
                    IntPtr.Zero,
                    HttpServiceConfigUrlAclInfo,
                    ref configInfo,
                    Marshal.SizeOf(typeof(HTTP_SERVICE_CONFIG_URLACL_SET)),
                    IntPtr.Zero);
                CheckResult(
                    "HttpSetServiceConfigurationAcl",
                    errorCode);
            }
            finally
            {
                errorCode = HttpTerminate(
                    HTTP_INITIALIZE_CONFIG,
                    IntPtr.Zero);
                CheckResult(
                   "HttpTerminate",
                   errorCode);
            }
        }

        /// <summary>
        /// Checks whether the <paramref name="errorCode"/> is zero. If not, an <see cref="ApplicationException"/> is thrown.
        /// </summary>
        /// <param name="fcn">the function name</param>
        /// <param name="errorCode">the return value of the function</param>
        private static void CheckResult(string fcn, int errorCode)
        {
            if (errorCode != 0)
            {
                throw new ApplicationException(string.Format(
                        "{0} failed: {1}",
                        fcn,
                        GetWin32ErrorMessage(errorCode)));
            }
        }

        /// <summary>
        /// Creates a SDDL string representation of the ACL that Allows Generic eXecute for the specified user.
        /// </summary>
        /// <param name="account">the name of the account</param>
        /// <returns>a SDDL string that Allows Generic eXecute for the specified user.</returns>
        private static string CreateSddl(string account)
        {
            string sid = new NTAccount(account).Translate(typeof(SecurityIdentifier)).ToString();

            // DACL that Allows Generic eXecute for the user
            // specified by account
            // see help for HTTP_SERVICE_CONFIG_URLACL_PARAM
            // for details on what this means
            return string.Format("D:(A;;GX;;;{0})", sid);
        }

        /// <summary>
        /// Retrieves the Win32 error message from an error code.
        /// </summary>
        /// <param name="errorCode">the code to process</param>
        /// <returns>an error message</returns>
        private static string GetWin32ErrorMessage(int errorCode)
        {
            int hr = HRESULT_FROM_WIN32(errorCode);
            Exception x = Marshal.GetExceptionForHR(hr);
            return x.Message;
        }

        /// <summary>
        /// Transforms an error code into a HRESULT
        /// </summary>
        /// <param name="errorCode">the error code</param>
        /// <returns>the HRESULT</returns>
        private static int HRESULT_FROM_WIN32(int errorCode)
        {
            if (errorCode <= 0)
            {
                return errorCode;
            }

            return (int)((0x0000FFFFU & ((uint)errorCode)) | (7U << 16) | 0x80000000U);
        }

        #region P/Invoke stubs from http.h

        const int HttpServiceConfigUrlAclInfo = 2;
        const int HTTP_INITIALIZE_CONFIG = 2;
        [StructLayout(LayoutKind.Sequential)]
        struct HTTPAPI_VERSION
        {
            public HTTPAPI_VERSION(short maj, short min)
            {
                Major = maj;
                Minor = min;
            }
            public short Major;
            public short Minor;
        }
        [StructLayout(LayoutKind.Sequential)]
        struct HTTP_SERVICE_CONFIG_URLACL_KEY
        {
            [MarshalAs(UnmanagedType.LPWStr)]
            public string UrlPrefix;
        }
        [StructLayout(LayoutKind.Sequential)]
        struct HTTP_SERVICE_CONFIG_URLACL_PARAM
        {
            [MarshalAs(UnmanagedType.LPWStr)]
            public string Sddl;
        }
        [StructLayout(LayoutKind.Sequential)]
        struct HTTP_SERVICE_CONFIG_URLACL_SET
        {
            public HTTP_SERVICE_CONFIG_URLACL_KEY Key;
            public HTTP_SERVICE_CONFIG_URLACL_PARAM Param;
        }
        [DllImport("httpapi.dll", ExactSpelling = true,
                EntryPoint = "HttpSetServiceConfiguration")]
        static extern int HttpSetServiceConfigurationAcl(
            IntPtr mustBeZero, int configID,
            [In] ref HTTP_SERVICE_CONFIG_URLACL_SET configInfo,
            int configInfoLength, IntPtr mustBeZero2);
        [DllImport("httpapi.dll", ExactSpelling = true,
                EntryPoint = "HttpDeleteServiceConfiguration")]
        static extern int HttpDeleteServiceConfigurationAcl(
            IntPtr mustBeZero, int configID,
            [In] ref HTTP_SERVICE_CONFIG_URLACL_SET configInfo,
            int configInfoLength, IntPtr mustBeZero2);
        [DllImport("httpapi.dll")]
        static extern int HttpInitialize(
            HTTPAPI_VERSION version,
            int flags, IntPtr mustBeZero);
        [DllImport("httpapi.dll")]
        static extern int HttpTerminate(int flags,
            IntPtr mustBeZero);

        #endregion
    }
}
