// <autogenerated/>

namespace Zetbox.App.Base
{
    using System;
    using System.Collections.Generic;

    using Zetbox.API;

    /// <summary>
    /// Logentry for exceptions
    /// </summary>
    [Zetbox.API.DefinitionGuid("72d9934d-aee9-4512-ad1e-1a30af1d353e")]
    public interface ExceptionLogEntry : IDataObject 
    {

        /// <summary>
        /// 
        /// </summary>
        [Zetbox.API.DefinitionGuid("e093679e-1939-4a7d-97db-e48b5103062a")]
        DateTime Date {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [Zetbox.API.DefinitionGuid("203915b1-5bbf-49ff-864b-b66098b39481")]
        string Exception {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [Zetbox.API.DefinitionGuid("f8becad3-4bb8-47b2-b30c-1c7fc94eac12")]
        string Level {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [Zetbox.API.DefinitionGuid("fdf063e0-66f9-421d-9e60-e6aa9fa73d8e")]
        string Logger {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [Zetbox.API.DefinitionGuid("d80639ab-d40f-41db-a51f-c1a71440654e")]
        string Message {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        [Zetbox.API.DefinitionGuid("24ab22e0-7dd3-4535-9abd-a94102f9ca80")]
        string Thread {
            get;
            set;
        }
    }
}
