namespace GameNameSpace
{
	public interface IParser
	{
		ParsedLevel ReadEmbeddedResource(string resourceName);
	}
}