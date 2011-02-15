namespace Kistl.Unix
{
	using System;
	using System.Collections.Generic;
	using Kistl.API;
	using Mono.Unix;
	
	public class PosixIdentitySource: IIdentitySource
    {
        public IEnumerable<IdentitySourceItem> GetAllIdentities()
        {
			foreach(var unixUser in UnixUserInfo.GetLocalUsers()) 
			{
            	yield return new IdentitySourceItem() { DisplayName = unixUser.RealName, UserName= unixUser.UserName };
			}
        }
    }
}
