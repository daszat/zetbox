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

namespace Zetbox.Client.WPF.Styles
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows;

    public static class Defaults
    {
        private class WpfDesignResourceKey : ResourceKey
        {
            public override System.Reflection.Assembly Assembly
            {
                get { return typeof(WpfDesignResourceKey).Assembly; }
            }
        }

        #region Main Color Set

        private static readonly ResourceKey _mainBackgroundKey = new WpfDesignResourceKey();
        public static ResourceKey MainBackgroundKey { get { return _mainBackgroundKey; } }

        private static readonly ResourceKey _mainForegroundKey = new WpfDesignResourceKey();
        public static ResourceKey MainForegroundKey { get { return _mainForegroundKey; } }

        private static readonly ResourceKey _mainAccentKey = new WpfDesignResourceKey();
        public static ResourceKey MainAccentKey { get { return _mainAccentKey; } }

        private static readonly ResourceKey _mainAccentForegroundKey = new WpfDesignResourceKey();
        public static ResourceKey MainAccentForegroundKey { get { return _mainAccentForegroundKey; } }

        private static readonly ResourceKey _mainHighlightKey = new WpfDesignResourceKey();
        public static ResourceKey MainHighlightKey { get { return _mainHighlightKey; } }

        private static readonly ResourceKey _mainHighlightForegroundKey = new WpfDesignResourceKey();
        public static ResourceKey MainHighlightForegroundKey { get { return _mainHighlightForegroundKey; } }

        private static readonly ResourceKey _mainBorderKey = new WpfDesignResourceKey();
        public static ResourceKey MainBorderKey { get { return _mainBorderKey; } }

        #endregion

        #region Secondary Color Set

        private static readonly ResourceKey _SecondaryBackgroundKey = new WpfDesignResourceKey();
        public static ResourceKey SecondaryBackgroundKey { get { return _SecondaryBackgroundKey; } }

        private static readonly ResourceKey _SecondaryForegroundKey = new WpfDesignResourceKey();
        public static ResourceKey SecondaryForegroundKey { get { return _SecondaryForegroundKey; } }

        private static readonly ResourceKey _SecondaryAccentKey = new WpfDesignResourceKey();
        public static ResourceKey SecondaryAccentKey { get { return _SecondaryAccentKey; } }

        private static readonly ResourceKey _SecondaryAccentForegroundKey = new WpfDesignResourceKey();
        public static ResourceKey SecondaryAccentForegroundKey { get { return _SecondaryAccentForegroundKey; } }

        private static readonly ResourceKey _SecondaryHighlightKey = new WpfDesignResourceKey();
        public static ResourceKey SecondaryHighlightKey { get { return _SecondaryHighlightKey; } }

        private static readonly ResourceKey _SecondaryHighlightForegroundKey = new WpfDesignResourceKey();
        public static ResourceKey SecondaryHighlightForegroundKey { get { return _SecondaryHighlightForegroundKey; } }

        private static readonly ResourceKey _SecondaryBorderKey = new WpfDesignResourceKey();
        public static ResourceKey SecondaryBorderKey { get { return _SecondaryBorderKey; } }

        #endregion

        #region Tertiary Color Set

        private static readonly ResourceKey _TertiaryBackgroundKey = new WpfDesignResourceKey();
        public static ResourceKey TertiaryBackgroundKey { get { return _TertiaryBackgroundKey; } }

        private static readonly ResourceKey _TertiaryForegroundKey = new WpfDesignResourceKey();
        public static ResourceKey TertiaryForegroundKey { get { return _TertiaryForegroundKey; } }

        private static readonly ResourceKey _TertiaryAccentKey = new WpfDesignResourceKey();
        public static ResourceKey TertiaryAccentKey { get { return _TertiaryAccentKey; } }

        private static readonly ResourceKey _TertiaryAccentForegroundKey = new WpfDesignResourceKey();
        public static ResourceKey TertiaryAccentForegroundKey { get { return _TertiaryAccentForegroundKey; } }

        private static readonly ResourceKey _TertiaryHighlightKey = new WpfDesignResourceKey();
        public static ResourceKey TertiaryHighlightKey { get { return _TertiaryHighlightKey; } }

        private static readonly ResourceKey _TertiaryHighlightForegroundKey = new WpfDesignResourceKey();
        public static ResourceKey TertiaryHighlightForegroundKey { get { return _TertiaryHighlightForegroundKey; } }

        private static readonly ResourceKey _TertiaryBorderKey = new WpfDesignResourceKey();
        public static ResourceKey TertiaryBorderKey { get { return _TertiaryBorderKey; } }

        #endregion
    }
}
