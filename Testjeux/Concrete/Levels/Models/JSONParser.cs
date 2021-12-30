using System.IO;
using System.Reflection;
using System.Text.Json;

namespace GameNameSpace
{
	public class JSONParser : IParser
	{
		public ParsedLevel ConvertFromString(string stringLevel)
		{
			return (JsonSerializer.Deserialize(stringLevel, typeof(ParsedLevel)) as ParsedLevel);
		}

		public void SaveToLocalResource(ParsedLevel level, string resourceName)
		{
			File.WriteAllText(resourceName, JsonSerializer.Serialize<ParsedLevel>(level));
		}

		public ParsedLevel ReadLocalResource(string resourceName)
		{
			return (ConvertFromString(File.ReadAllText(resourceName)));
		}

		public ParsedLevel ReadEmbeddedResource(string resourceName)
		{
			string result;
			using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
			using (StreamReader reader = new StreamReader(stream))
			{
				result = reader.ReadToEnd();
			}

			return (ConvertFromString(result));
		}
	}
}