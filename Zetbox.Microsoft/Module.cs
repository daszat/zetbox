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

namespace Zetbox.Microsoft
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using Autofac;
    using Zetbox.API;
    using Zetbox.API.Client;
    using Zetbox.API.Configuration;
    using Zetbox.API.Utils;
    using System.Globalization;

    [Feature]
    [Description("Modules that only run on Micorosoft Windows (namely a ScreenshotTool)")]
    public class Module : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder
                .Register<ScreenshotTool>(c => new ScreenshotTool())
                .As<IScreenshotTool>()
                .SingleInstance();
        }
    }
}
