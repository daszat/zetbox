using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kistl.API.Client
{
    public interface IScreenshotTool
    {
        System.Drawing.Bitmap GetScreenshot();
    }
}
