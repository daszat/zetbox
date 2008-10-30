using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API.Client;
using Kistl.App.GUI;
using Kistl.GUI.DB;
using Kistl.GUI.Renderer;

namespace Kistl.Client
{
    public class GuiApplicationContext : ClientApiContext
    {
        public static new GuiApplicationContext Current { get; private set; }

        public GuiApplicationContext(string configFile, Toolkit tk)
            : base(configFile)
        {
            GuiApplicationContext.Current = this;
            SetCustomActionsManager(new CustomActionsManagerClient());
            this.Renderer = KistlGUIContext.CreateRenderer(tk);
        }

        public Kistl.GUI.Renderer.IRenderer Renderer { get; private set; }
    }
}
