
namespace Zetbox.API.Migration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API.Server;

    public class OutputStream : IDisposable
    {
        private readonly ISchemaProvider _provider;
        private readonly TableRef _tbl;
        private long _writtenRecords = 0;

        public OutputStream(TableRef tbl, ISchemaProvider provider)
        {
            if (tbl == null) throw new ArgumentNullException("tbl");
            if (provider == null) throw new ArgumentNullException("provider");
            this._provider = provider;
            this._tbl = tbl;
        }

        /// <summary>
        /// The number of records written to this OutputStream
        /// </summary>
        public long WrittenRecords
        {
            get { return _writtenRecords; }
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
            _writtenRecords += 1;
        }
    }
}
