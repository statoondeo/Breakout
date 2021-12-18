using System;
using System.IO;

namespace GameNameSpace
{
	public class FileLogger : BaseLogger
	{
		protected string FileName;
		
		public FileLogger(string fileName) 
			: base() 
		{
			FileName = fileName;
		}

		public override string Get()
		{
			return (File.ReadAllText(FileName));
		}

		public override void Log(string message)
		{
			File.AppendAllText(FileName, message + Environment.NewLine);
		}

		public override void Reset()
		{
			File.Delete(FileName);
		}
	}
}