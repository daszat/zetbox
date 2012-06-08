namespace Zetbox.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.App.GUI;
    using Zetbox.Client;

    [Implementor]
    public static class CalculatedObjectReferencePropertyActions
    {
        [Invocation]
        public static void NotifyCreated(Zetbox.App.Base.CalculatedObjectReferenceProperty obj)
        {
            // At creating time there is no way to discover if the navigator is a Reference or List
        }
    }
}
