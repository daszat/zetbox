using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API.Client;
using Kistl.API.Configuration;
using Kistl.App.GUI;
using Kistl.GUI.DB;
using Kistl.GUI.Renderer;

namespace Kistl.Client
{
    public class GuiApplicationContext : ClientApiContext
    {
        public static new GuiApplicationContext Current { get; private set; }

        public GuiApplicationContext(KistlConfig config, Toolkit tk)
            : base(config)
        {
            GuiApplicationContext.Current = this;
            SetCustomActionsManager(new CustomActionsManagerClient());
            this.Renderer = KistlGUIContext.CreateRenderer(tk);
        }

        public Kistl.GUI.Renderer.IRenderer Renderer { get; private set; }
    }
}
