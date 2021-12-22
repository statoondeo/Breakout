using System.Collections.Generic;

namespace GameNameSpace
{
	public interface IStateItem
	{
		IList<IStateItem> Transitions { get; }
		IStateContainer Container { get; }
	}
}