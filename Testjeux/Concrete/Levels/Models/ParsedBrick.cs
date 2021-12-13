namespace GameNameSpace
{
	public class ParsedBrick
    {
		public int Type { get; set; }
        public ParsedVector2 Position { get; set; }
        public ParsedVector2 Size { get; set; }
        public ParsedVector2 Center { get; set; }
        public int Radius { get; set; }
        public float Angle { get; set; }
        public float AngleSpeed { get; set; }

		public ParsedBrick() { }
	}
}