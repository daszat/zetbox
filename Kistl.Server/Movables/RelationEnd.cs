using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.App.Base;
using Kistl.Server.Generators;

namespace Kistl.Server.Movables
{

    public class RelationEnd
    {
        public RelationEnd()
        {
            DebugCreationSite = "unknown";
        }

        /// <summary>
        /// The ORP to navigate FROM this end of the relation. MAY be null.
        /// </summary>
        public Property Navigator { get; set; }

        /// <summary>
        /// The Type referenced by this end of the relation. MUST NOT be null.
        /// </summary>
        public TypeMoniker Referenced { get; set; }

        /// <summary>
        /// The Multiplicity of this end of the relation
        /// </summary>
        public Multiplicity Multiplicity { get; set; }

        /// <summary>
        /// The role of the referenced ObjectClass in this relation
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// The NewRelation that this End is part of
        /// </summary>
        public NewRelation Container { get; internal set; }

        /// <summary>
        /// a debug string attached to describe the site where this was NewRelation was created
        /// </summary>
        public string DebugCreationSite { get; set; }
    }

}
