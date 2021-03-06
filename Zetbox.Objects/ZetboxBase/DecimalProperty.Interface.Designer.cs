// <autogenerated/>

namespace Zetbox.App.Base
{
    using System;
    using System.Collections.Generic;

    using Zetbox.API;

    /// <summary>
    /// Metadefinition Object for Decimal Properties.
    /// </summary>
    [Zetbox.API.DefinitionGuid("7e44265e-8d41-4f5f-bb5c-5038b55be5b2")]
    public interface DecimalProperty : Zetbox.App.Base.ValueTypeProperty 
    {

        /// <summary>
        /// The maximum total number of decimal digits that can be stored, both to the left and to the right of the decimal point.
        /// </summary>
        [Zetbox.API.DefinitionGuid("35dd7765-0e26-4195-b687-ce814560ba34")]
        int Precision {
            get;
            set;
        }


        /// <summary>
        /// The maximum number of decimal digits that can be stored to the right of the decimal point.
        /// </summary>
        [Zetbox.API.DefinitionGuid("fba03086-8a2b-4c25-b83f-df63933b62fe")]
        int Scale {
            get;
            set;
        }

    }
}
