using System.Linq;

using Kistl.API;

namespace Kistl.App.TimeRecords
{

	public class TimeRecordsRepository
	{
		public TimeRecordsRepository(IKistlContext ctx)
		{
			this.Context = ctx;
		}
		
		public IKistlContext Context { get; private set; }
		
		/// <summary>List of all WorkEffortAccount</summary>
		/// An account of work efforts. May be used to limit the hours being expended.
		public IQueryable<WorkEffortAccount> WorkEffortAccounts
		{ 
			get
			{
				return Context.GetQuery<WorkEffortAccount>();
			}
		}
		
		/// <summary>List of all WorkEffort</summary>
		/// A defined work effort of an employee.
		public IQueryable<WorkEffort> WorkEfforts
		{ 
			get
			{
				return Context.GetQuery<WorkEffort>();
			}
		}
		
		/// <summary>List of all PresenceRecord</summary>
		/// 
		public IQueryable<PresenceRecord> PresenceRecords
		{ 
			get
			{
				return Context.GetQuery<PresenceRecord>();
			}
		}
		
	
	}
	
}