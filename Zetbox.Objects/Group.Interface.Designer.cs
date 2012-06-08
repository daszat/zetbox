// <autogenerated/>

namespace Zetbox.App.Base
{
    using System;
    using System.Collections.Generic;

    using Zetbox.API;

    /// <summary>
    /// 
    /// </summary>
    [Zetbox.API.DefinitionGuid("01a06aef-8fe4-4cb6-b348-ee4bcd11f5af")]
    public interface Group : IDataObject, Zetbox.App.Base.IExportable, Zetbox.App.Base.IModuleMember, Zetbox.App.Base.INamedObject 
    {

        /// <summary>
        /// Identities are member of this group
        /// </summary>

        [Zetbox.API.DefinitionGuid("f60308a5-a502-4641-aa19-f895e701778c")]
        ICollection<Zetbox.App.Base.Identity> Member { get; }

        /// <summary>
        /// 
        /// </summary>
        [Zetbox.API.DefinitionGuid("99c320b1-0003-4e2d-aa98-9a215d80988b")]
        string Name {
            get;
            set;
        }
    }
}