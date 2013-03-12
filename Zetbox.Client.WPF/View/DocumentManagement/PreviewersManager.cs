// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.
// http://www.codeproject.com/KB/WPF/wpf_vista_preview_handler.aspx

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.Windows;
using System.Runtime.InteropServices.ComTypes;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Security;
using Zetbox.API.Utils;

namespace Zetbox.Client.WPF.View.DocumentManagement
{
    // Some more infos: http://www.brad-smith.info/blog/archives/183
    class PreviewersManager : IDisposable
    {
        [DllImport("shell32.dll", CharSet = CharSet.Unicode, PreserveSig = false)]
        static extern void SHCreateItemFromParsingName(
            [In][MarshalAs(UnmanagedType.LPWStr)] string pszPath,
            [In] IntPtr pbc, [In][MarshalAs(UnmanagedType.LPStruct)] Guid riid,
            [Out][MarshalAs(UnmanagedType.Interface, IidParameterIndex = 2)] out IShellItem ppv
        );

        private IPreviewHandler pHandler;
        private COMStream stream;

        public void InvalidateAttachedPreview(IntPtr handler, Rect viewRect)
        {
            if (pHandler != null)
            {
                RECT r = new RECT(viewRect);
                pHandler.SetRect(ref r);
            }
        }

        public void AttachPreview(IntPtr handler, string fileName, Rect viewRect)
        {
            Release();

            try
            {
                string CLSID = "8895b1c6-b41f-4c1c-a562-0d564250836f";
                Guid g = new Guid(CLSID);
                string[] exts = fileName.Split('.');
                string ext = exts[exts.Length - 1];
                var regKey = string.Format(@".{0}\ShellEx\{1:B}", ext, g);
                using (RegistryKey hk = Registry.ClassesRoot.OpenSubKey(regKey))
                {
                    if (hk == null)
                    {
                        Logging.Log.WarnOnce("Unable to initialize IPreviewHandler - Registry Key not found: " + regKey);
                        return;
                    }
                    var objValue = hk.GetValue("");
                    if (objValue == null)
                    {
                        Logging.Log.WarnOnce("Unable to initialize IPreviewHandler - no handler registrated in Registry");
                        return;
                    }
                    if (!Guid.TryParse(objValue.ToString(), out g))
                    {
                        Logging.Log.WarnOnce("Unable to initialize IPreviewHandler - cannot parse guid from registry: " + objValue);
                        return;
                    }

                    Type type = Type.GetTypeFromCLSID(g, false);
                    if (type == null)
                    {
                        Logging.Log.WarnOnce(string.Format("Unable to initialize IPreviewHandler - could not load COM Object, Type.GetTypeFromCLSID({0}) returend false", g));
                        return;
                    }
                    object comObj = Activator.CreateInstance(type);

                    IInitializeWithFile fileInit = comObj as IInitializeWithFile;
                    IInitializeWithStream streamInit = comObj as IInitializeWithStream;
                    IInitializeWithItem itemInit = comObj as IInitializeWithItem;

                    bool isInitialized = false;
                    if (fileInit != null)
                    {
                        fileInit.Initialize(fileName, 0); // 0 = STGM_READ
                        isInitialized = true;
                    }
                    else if (streamInit != null)
                    {
                        stream = new COMStream(File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read));
                        streamInit.Initialize((IStream)stream, 0); // 0 = STGM_READ
                        isInitialized = true;
                    }
                    else if (itemInit != null)
                    {
                        IShellItem shellItem;
                        SHCreateItemFromParsingName(fileName, IntPtr.Zero, typeof(IShellItem).GUID, out shellItem);
                        itemInit.Initialize(shellItem, 0); // 0 = STGM_READ
                        isInitialized = true;
                    }

                    pHandler = comObj as IPreviewHandler;
                    if (isInitialized && pHandler != null)
                    {
                        RECT r = new RECT(viewRect);

                        pHandler.SetWindow(handler, ref r);
                        pHandler.SetRect(ref r);

                        pHandler.DoPreview();
                    }
                    else
                    {
                        Release();
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.Log.Warn("Unable to initialize IPreviewHandler", ex);
                Release();
            }
        }

        public void Dispose()
        {
            Release();
        }

        private void Release()
        {
            if (pHandler != null)
            {
                try
                {
                    pHandler.Unload();
                }
                catch { }
                Marshal.FinalReleaseComObject(pHandler);
                pHandler = null;
            }

            if (stream != null)
            {
                stream.Dispose();
                stream = null;
            }
        }
    }

    public class Previewer
    {
        public Guid CLSID { get; set; }
        public string Title { get; set; }
    }

    #region COM Interfaces
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("8895b1c6-b41f-4c1c-a562-0d564250836f")]
    interface IPreviewHandler
    {
        void SetWindow(IntPtr hwnd, ref RECT rect);
        void SetRect(ref RECT rect);
        void DoPreview();
        void Unload();
        void SetFocus();
        void QueryFocus(out IntPtr phwnd);
        [PreserveSig]
        uint TranslateAccelerator(ref MSG pmsg);
    }

    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("b7d14566-0509-4cce-a71f-0a554233bd9b")]
    interface IInitializeWithFile
    {
        void Initialize([MarshalAs(UnmanagedType.LPWStr)] string pszFilePath, uint grfMode);
    }

    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("b824b49d-22ac-4161-ac8a-9916e8fa3f7f")]
    interface IInitializeWithStream
    {
        void Initialize(IStream pstream, uint grfMode);
    }

    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("7F73BE3F-FB79-493C-A6C7-7EE14E245841")]
    interface IInitializeWithItem
    {
        void Initialize(IShellItem pstream, uint grfMode);
    }

    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("43826d1e-e718-42ee-bc55-a1e261c37bfe")]
    interface IShellItem
    {
        void BindToHandler(IntPtr pbc,
            [MarshalAs(UnmanagedType.LPStruct)]Guid bhid,
            [MarshalAs(UnmanagedType.LPStruct)]Guid riid,
            out IntPtr ppv);

        void GetParent(out IShellItem ppsi);

        void GetDisplayName(uint sigdnName, out IntPtr ppszName);

        void GetAttributes(uint sfgaoMask, out uint psfgaoAttribs);

        void Compare(IShellItem psi, uint hint, out int piOrder);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int left;
        public int top;
        public int right;
        public int bottom;

        public RECT(Rect rect)
        {
            this.top = (int)rect.Top;
            this.bottom = (int)rect.Bottom;
            this.left = (int)rect.Left;
            this.right = (int)rect.Right;
        }
    }

    public sealed class COMStream : IStream, IDisposable
    {
        Stream _stream;

        ~COMStream()
        {
            if (_stream != null)
            {
                _stream.Close();
                _stream.Dispose();
                _stream = null;
            }
        }

        private COMStream() { }

        public COMStream(Stream sourceStream)
        {
            _stream = sourceStream;
        }

        #region IStream Members

        public void Clone(out IStream ppstm)
        {
            throw new NotSupportedException();
        }

        public void Commit(int grfCommitFlags)
        {
            throw new NotSupportedException();
        }

        public void CopyTo(IStream pstm, long cb, IntPtr pcbRead, IntPtr pcbWritten)
        {
            throw new NotSupportedException();
        }

        public void LockRegion(long libOffset, long cb, int dwLockType)
        {
            throw new NotSupportedException();
        }

        [SecurityCritical]
        public void Read(byte[] pv, int cb, IntPtr pcbRead)
        {
            int count = this._stream.Read(pv, 0, cb);

            // destination expects an ULONG (32 bit?), therefore we must guard against negative values
            if (pcbRead != IntPtr.Zero)
            {
                Marshal.WriteInt32(pcbRead, count >= 0 ? count : 0);
            }
        }

        public void Revert()
        {
            throw new NotSupportedException();
        }

        [SecurityCritical]
        public void Seek(long dlibMove, int dwOrigin, IntPtr plibNewPosition)
        {
            SeekOrigin origin = (SeekOrigin)dwOrigin;
            long pos = this._stream.Seek(dlibMove, origin);
            if (plibNewPosition != IntPtr.Zero)
            {
                Marshal.WriteInt64(plibNewPosition, pos);
            }
        }

        public void SetSize(long libNewSize)
        {
            this._stream.SetLength(libNewSize);
        }

        public void Stat(out System.Runtime.InteropServices.ComTypes.STATSTG pstatstg, int grfStatFlag)
        {
            pstatstg = new System.Runtime.InteropServices.ComTypes.STATSTG();
            pstatstg.type = 2; // STGTY_STREAM
            pstatstg.cbSize = this._stream.Length;
            pstatstg.grfMode = 0;
            if (this._stream.CanRead && this._stream.CanWrite)
            {
                pstatstg.grfMode |= 2;
            }
            else if (this._stream.CanRead && !_stream.CanWrite)
            {
                pstatstg.grfMode |= 1;
            }
            else
            {
                throw new IOException();
            }

        }

        public void UnlockRegion(long libOffset, long cb, int dwLockType)
        {
            throw new NotSupportedException();
        }

        [SecurityCritical]
        public void Write(byte[] pv, int cb, IntPtr pcbWritten)
        {
            this._stream.Write(pv, 0, cb);
            if (pcbWritten != IntPtr.Zero)
            {
                Marshal.WriteInt32(pcbWritten, cb);
            }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            if (this._stream != null)
            {
                this._stream.Close();
                this._stream.Dispose();
                this._stream = null;
            }
        }

        #endregion
    }
    #endregion
}
