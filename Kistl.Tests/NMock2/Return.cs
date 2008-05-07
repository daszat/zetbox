using System;
using NMock2.Actions;

namespace NMock2
{
	public class Return
	{
		public static IAction Value(object result)
		{
            return new ReturnAction(result);
        }
		
		public static IAction CloneOf(ICloneable prototype)
		{
			return new ReturnCloneAction(prototype);
		}
	}
}
