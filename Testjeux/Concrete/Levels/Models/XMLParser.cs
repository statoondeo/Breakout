using System.IO;
using System.Reflection;
using System.Xml.Serialization;

namespace GameNameSpace
{
	public class XMLParser : IParser
	{
		public ParsedLevel ReadEmbeddedResource(string resourceName)
		{
			XmlSerializer xmlSerializer = new XmlSerializer(typeof(ParsedLevel));
			object result;
			using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
			using (StreamReader reader = new StreamReader(stream))
			{
				result = xmlSerializer.Deserialize(reader);
			}
			return (result as ParsedLevel);
		}
	}
}