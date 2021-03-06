using System.Collections.Generic;

namespace GameNameSpace
{

	public class TweeningService : ITweeningService
	{
		protected IDictionary<TweeningName, ITweening> TweeningsDictionary;

		public TweeningService()
		{
			TweeningsDictionary = new Dictionary<TweeningName, ITweening>
			{
				{ TweeningName.BouceOut, new BounceOutTweening() },
				{ TweeningName.ElasticOut, new ElasticOutTweening() },
				{ TweeningName.Linear, new LinearTweening() },
				{ TweeningName.QuintIn, new QuintInTweening() },
				{ TweeningName.QuintOut, new QuintOutTweening() },
				{ TweeningName.QuintInOutTweening, new QuintInOutTweening() }
			};
		}

		public ITweening Get(TweeningName name)
		{
			return (TweeningsDictionary[name]);
		}
	}
}
