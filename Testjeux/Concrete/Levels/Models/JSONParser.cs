using System.IO;
using System.Reflection;
using System.Text.Json;

namespace GameNameSpace
{
	public class JSONParser : IParser
	{
		public ParsedLevel ReadEmbeddedResource(string resourceName)
		{
			string result;
			using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
			using (StreamReader reader = new StreamReader(stream))
			{
				result = reader.ReadToEnd();
			}

			return (JsonSerializer.Deserialize(result, typeof(ParsedLevel)) as ParsedLevel);
		}
	}
}