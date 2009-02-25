using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.Server.Generators;

namespace Kistl.Server.Movables
{

    public class RelationEnd
    {
        public RelationEnd(RelationEndRole role)
        {
            Role = role;
            DebugCreationSite = "unknown";
        }

        public RelationEndRole Role { get; private set; }

        /// <summary>
        /// The ORP to navigate FROM this end of the relation. MAY be null.
        /// </summary>
        public Property Navigator { get; set; }

        /// <summary>
        /// Specifies which type this End of the relation has. MUST NOT be null.
        /// </summary>
        public TypeMoniker Type { get; set; }

        /// <summary>
        /// the type of the root class of this End. MUST NOT be null.
        /// </summary>
        /// == otherend.type
        public TypeMoniker RootType { get; set; }

        /// <summary>
        /// The Multiplicity of this end of the relation.
        /// </summary>
        public Multiplicity Multiplicity { get; set; }

        /// <summary>
        /// The role this End has in the relation.
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// The NewRelation that this End is part of
        /// </summary>
        public NewRelation Container { get; internal set; }

        /// <summary>
        /// The other end of Container
        /// </summary>
        public RelationEnd Other { get; internal set; }

        /// <summary>
        /// Whether or not this relation end has a persistant order of elements.
        /// </summary>
        public bool HasPersistentOrder { get; set; }

        /// <summary>
        /// a debug string attached to describe the site where this was NewRelation was created
        /// </summary>
        // TODO: do not migrate into data store, only relevant to prototype
        public string DebugCreationSite { get; set; }
    }

}
