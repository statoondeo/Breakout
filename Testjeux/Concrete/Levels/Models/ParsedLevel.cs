using System.Collections.Generic;

namespace GameNameSpace
{
	public class ParsedLevel
	{
		public List<ParsedBrick> Bricks { get; set; }
		public List<ParsedBackground> Backgrounds { get; set; }
		public List<ParsedTrigger> Triggers { get; set; }

		public ParsedLevel() 
		{
			Bricks = new List<ParsedBrick>();
			Backgrounds = new List<ParsedBackground>();
			Triggers = new List<ParsedTrigger>();
		}
	}
}