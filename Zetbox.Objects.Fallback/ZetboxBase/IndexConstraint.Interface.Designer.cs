// <autogenerated/>

namespace Zetbox.App.Base
{
    using System;
    using System.Collections.Generic;

    using Zetbox.API;

    /// <summary>
    /// 
    /// </summary>
    [Zetbox.API.DefinitionGuid("1d5a58e9-fba6-4ef8-b3b7-9966a4dcba83")]
    public interface IndexConstraint : Zetbox.App.Base.InstanceConstraint 
    {

        /// <summary>
        /// Index is created as a Unique Index
        /// </summary>
        [Zetbox.API.DefinitionGuid("2cc6e028-e01f-4879-bda8-78d459c0eaf4")]
        bool IsUnique {
            get;
            set;
        }


        /// <summary>
        /// 
        /// </summary>

        [Zetbox.API.DefinitionGuid("3e4bfd37-1037-472b-a5d7-2c20a777e6fd")]
        [System.Runtime.Serialization.IgnoreDataMember]
        ICollection<Zetbox.App.Base.Property> Properties { get; }

        System.Threading.Tasks.Task<ICollection<Zetbox.App.Base.Property>> GetProp_Properties();
    }
}
