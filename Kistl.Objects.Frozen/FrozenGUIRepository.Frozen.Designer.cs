using System;
using System.Collections.Generic;
using System.Linq;

using Kistl.API;

namespace Kistl.App.GUI
{

	public static class FrozenGUIRepository
	{
		
		/// <summary>Frozen List of all Icon</summary>
		/// 
		public static IQueryable<Icon> Icons
		{ 
			get
			{
				return Icon__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<Icon>();
			}
		}
		

		internal static void CreateInstances()
		{
				Icon__Implementation__Frozen.CreateInstances();
		}


		internal static void FillDataStore()
		{
				Icon__Implementation__Frozen.FillDataStore();
		}
	}
	
}