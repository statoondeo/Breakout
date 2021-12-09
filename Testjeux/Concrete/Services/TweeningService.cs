using System.Collections.Generic;

namespace GameNameSpace
{

	public class TweeningService : ITweeningService
	{
		protected IDictionary<TweeningName, ITweening> TweeningsDictionary;

		public TweeningService()
		{
			TweeningsDictionary = new Dictionary<TweeningName, ITweening>();
			TweeningsDictionary.Add(TweeningName.BouceOut, new BounceOutTweening());
			TweeningsDictionary.Add(TweeningName.ElasticOut, new ElasticOutTweening());
			TweeningsDictionary.Add(TweeningName.Linear, new LinearTweening());
			TweeningsDictionary.Add(TweeningName.QuintOut, new QuintOutTweening());
		}

		public ITweening Get(TweeningName name)
		{
			return (TweeningsDictionary[name]);
		}
	}
}
