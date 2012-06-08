
namespace Zetbox.API
{
	using System;
	using System.Collections.Generic;
	
    public class IdentitySourceItem
    {
        public string UserName { get; set; }
        public string DisplayName { get; set; }
    }

	public interface IIdentitySource
    {
        IEnumerable<IdentitySourceItem> GetAllIdentities();
    }
}

