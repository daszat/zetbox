using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API.Server;

namespace Kistl.API.Migration
{
    public class OutputStream : IDisposable
    {
        private readonly ISchemaProvider _provider;
        private readonly TableRef _tbl;

        public OutputStream(TableRef tbl, ISchemaProvider provider)
        {
            if (tbl == null) throw new ArgumentNullException("tbl");
            if (provider == null) throw new ArgumentNullException("provider");
            this._provider = provider;
            this._tbl = tbl;
        }

        #region IDisposable Members

        public void Dispose()
        {
            
        }

        #endregion

        public Record NewRecord()
        {
            return new Record();
        }

        public void Write(Record outputRecord)
        {
            if (outputRecord == null) throw new ArgumentNullException("outputRecord");
            _provider.WriteTableData(_tbl, outputRecord.GetFieldNames(), outputRecord.GetFields());
        }
    }
}
