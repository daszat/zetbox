// <autogenerated/>

namespace Zetbox.App.LicenseManagement
{
    using System;
    using System.Collections.Generic;

    using Zetbox.API;

    /// <summary>
    /// A single License
    /// </summary>
    [Zetbox.API.DefinitionGuid("dba4beb1-acfb-45f5-9261-6486536016f6")]
    public interface License : IDataObject, Zetbox.App.Base.IChangedBy, Zetbox.App.Base.IExportable 
    {

        /// <summary>
        /// 
        /// </summary>
        [Zetbox.API.DefinitionGuid("bb043766-bd52-4e8f-b98a-6c35867db9e1")]
        string Description {
            get;
            set;
        }

        /// <summary>
        /// The licensee of this license
        /// </summary>
        [Zetbox.API.DefinitionGuid("86553c32-7f63-41f3-a3a2-28a5801935b9")]
        string Licensee {
            get;
            set;
        }

        /// <summary>
        /// A integer represeting the license subject
        /// </summary>
        [Zetbox.API.DefinitionGuid("fea12587-60b9-41c2-9123-6884dc7f51c6")]
        int? LicenseSubject {
            get;
            set;
        }

        /// <summary>
        /// Licensor of this License
        /// </summary>
        [Zetbox.API.DefinitionGuid("906bc74c-e8b8-4b51-83b3-597ca6062090")]
        string Licensor {
            get;
            set;
        }

        /// <summary>
        /// Encoded Signature
        /// </summary>
        [Zetbox.API.DefinitionGuid("e6c5b0a0-ed58-4ba1-bd23-ed24f315620d")]
        string Signature {
            get;
            set;
        }

        /// <summary>
        /// License is valid from
        /// </summary>
        [Zetbox.API.DefinitionGuid("b8d29d74-eb31-47d4-bca1-a4c4c0f93c1c")]
        DateTime ValidFrom {
            get;
            set;
        }

        /// <summary>
        /// License is valid thru
        /// </summary>
        [Zetbox.API.DefinitionGuid("75a9ef4f-b914-47e2-ad0a-f2f63fd8c60f")]
        DateTime ValidThru {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        bool Check(System.Object certificate);

        /// <summary>
        /// Checks, if the signature is valid
        /// </summary>
        bool IsSignatureValid(System.Object certificate);

        /// <summary>
        /// Checks, if the time range is valid
        /// </summary>
        bool IsValid();

        /// <summary>
        /// Signs this certificate
        /// </summary>
        void Sign(Zetbox.App.LicenseManagement.PrivateKey certificate);
    }
}
