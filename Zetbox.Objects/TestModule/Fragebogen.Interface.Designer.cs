// <autogenerated/>

namespace Zetbox.App.Test
{
    using System;
    using System.Collections.Generic;

    using Zetbox.API;

    /// <summary>
    /// 
    /// </summary>
    [Zetbox.API.DefinitionGuid("a78ff235-4511-431b-8437-939f7fecded4")]
    public interface Fragebogen : IDataObject 
    {

        /// <summary>
        /// 
        /// </summary>

        [Zetbox.API.DefinitionGuid("e8f20c02-abea-4c91-850f-c321adfd46f0")]
        [System.Runtime.Serialization.IgnoreDataMember]
        IList<Zetbox.App.Test.Antwort> Antworten { get; }

        /// <summary>
        /// 
        /// </summary>
        [Zetbox.API.DefinitionGuid("b65f1a91-e063-4054-a2e7-d5dc0292e3fc")]
        int? BogenNummer {
            get;
            set;
        }


        /// <summary>
        /// 
        /// </summary>

        [Zetbox.API.DefinitionGuid("3a91e745-0dd2-4f31-864e-eaf657ddb577")]
        [System.Runtime.Serialization.IgnoreDataMember]
        ICollection<Zetbox.App.Test.TestStudent> Student { get; }
    }
}
