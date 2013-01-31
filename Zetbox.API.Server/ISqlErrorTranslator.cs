using System;
using System.Data.Common;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zetbox.API.Server
{
    public interface ISqlErrorTranslator
    {
        Exception Translate(Exception ex);
    }
}
