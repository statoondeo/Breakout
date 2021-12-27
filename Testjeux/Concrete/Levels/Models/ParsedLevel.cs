using System.Collections.Generic;

namespace GameNameSpace
{
	public class ParsedLevel
	{
		public ParsedMusic Music { get; set; }
		public List<ParsedBrick> Bricks { get; set; }
		public List<ParsedBackground> Backgrounds { get; set; }
		public List<ParsedTrigger> Triggers { get; set; }
		public List<ParsedText> Texts { get; set; }

		public ParsedLevel() 
		{
			Bricks = new List<ParsedBrick>();
			Backgrounds = new List<ParsedBackground>();
			Triggers = new List<ParsedTrigger>();
			Texts = new List<ParsedText>();
		}
	}
}