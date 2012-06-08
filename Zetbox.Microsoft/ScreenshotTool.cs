
namespace Zetbox.Microsoft
{
    using System;
    using System.Drawing;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API.Client;
    using System.Runtime.InteropServices;

    public class ScreenshotTool : IScreenshotTool
    {
        public Bitmap GetScreenshot()
        {
            RECT windowRectangle;
            GetWindowRect((System.IntPtr)GetForegroundWindow(), out windowRectangle);
            return CreateScreenshot(windowRectangle.Left, windowRectangle.Top, windowRectangle.Right - windowRectangle.Left, windowRectangle.Bottom - windowRectangle.Top);
        }

        private struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("user32.dll")]
        private static extern int GetForegroundWindow();

        private Bitmap CreateScreenshot(int left, int top, int width, int height)
        {
            Bitmap bmp = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bmp);
            g.CopyFromScreen(left, top, 0, 0, new Size(width, height));
            g.Dispose();
            return bmp;
        }
    }
}
