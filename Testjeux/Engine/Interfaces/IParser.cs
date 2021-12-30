namespace GameNameSpace
{
	public interface IParser
	{
		ParsedLevel ConvertFromString(string stringLevel);
		ParsedLevel ReadEmbeddedResource(string resourceName);
		ParsedLevel ReadLocalResource(string resourceName);
		void SaveToLocalResource(ParsedLevel level, string resourceName);
	}
}