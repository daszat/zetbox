using System.Linq;

using Kistl.API;

namespace Kistl.App.Zeiterfassung
{

	public class ZeiterfassungRepository
	{
		public ZeiterfassungRepository(IKistlContext ctx)
		{
			this.Context = ctx;
		}
		
		public IKistlContext Context { get; private set; }
		
		/// <summary>List of all Zeitkonto</summary>
		/// en:TimeAccount; Ein Konto für die Leistungserfassung. Es können nicht mehr als MaxStunden auf ein Konto gebucht werden.
		public IQueryable<Zeitkonto> Zeitkonten
		{ 
			get
			{
				return Context.GetQuery<Zeitkonto>();
			}
		}
		
		/// <summary>List of all LeistungsEintrag</summary>
		/// Eine definierte Leistung eines Mitarbeiters, die auf ein Zeitkonto gebucht worden ist.
		public IQueryable<LeistungsEintrag> LeistungsEinträge
		{ 
			get
			{
				return Context.GetQuery<LeistungsEintrag>();
			}
		}
		
		/// <summary>List of all ArbeitszeitEintrag</summary>
		/// 
		public IQueryable<ArbeitszeitEintrag> ArbeitszeitEinträge
		{ 
			get
			{
				return Context.GetQuery<ArbeitszeitEintrag>();
			}
		}
		
	
	}
	
}