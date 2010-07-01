// <autogenerated/>

namespace ZBox.App.SchemaMigration
{
    using System;
    using System.Collections.Generic;

    using Kistl.API;

    /// <summary>
    /// Defines a Schema Migration Project
    /// </summary>
    public interface MigrationProject : IDataObject 
    {

        /// <summary>
        /// 
        /// </summary>
		string Description {
			get;
			set;
		}
        /// <summary>
        /// 
        /// </summary>
		Kistl.App.Base.Module DestinationModule {
			get;
			set;
		}
        /// <summary>
        /// [DestDatabase].[dbo].
        /// </summary>
		string EtlDestDatabasePrefix {
			get;
			set;
		}
        /// <summary>
        /// 
        /// </summary>
		string EtlQuotePrefix {
			get;
			set;
		}
        /// <summary>
        /// 
        /// </summary>
		string EtlQuoteSuffix {
			get;
			set;
		}
        /// <summary>
        /// [SrcDatabase].[dbo].
        /// </summary>
		string EtlSrcDatabasePrefix {
			get;
			set;
		}
        /// <summary>
        /// 
        /// </summary>

        ICollection<ZBox.App.SchemaMigration.SourceTable> SourceTables { get; }
        /// <summary>
        /// Connectionstring for source database. Providerspecific
        /// </summary>
		string SrcConnectionString {
			get;
			set;
		}
        /// <summary>
        /// Provider for source database. Can be one of: MSSQL, POSTGRES, OLEDB
        /// </summary>
		string SrcProvider {
			get;
			set;
		}
        /// <summary>
        /// 
        /// </summary>
		 void CreateEtlStatements() ;
        /// <summary>
        /// 
        /// </summary>
		 void UpdateFromSourceSchema() ;
    }
}