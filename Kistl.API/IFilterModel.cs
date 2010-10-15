namespace Kistl.API
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Collections;

    public interface IFilterModel
    {
        IQueryable GetQuery(IQueryable src);
        IEnumerable GetResult(IEnumerable src);
        bool IsServerSideFilter { get; }

        IFilterValueSource ValueSource { get; set; }

        bool Enabled { get; }
        bool Required { get; }
    }

    public interface IFilterValueSource
    {
        string Expression { get; }
    }
}
