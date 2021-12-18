namespace GameNameSpace
{
	public class ParsedBackground
    {
        public int Type { get; set; }
        public string Texture { get; set; }
        public ParsedVector2 Position { get; set; }
        public ParsedVector2 Velocity { get; set; }
        public float AngleSpeed { get; set; }

        public ParsedBackground() { }
    }
}