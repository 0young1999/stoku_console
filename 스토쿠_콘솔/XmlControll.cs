using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace 스토쿠_콘솔
{
	class XmlControll
	{
		private static string GetSet(String name)
		{
			try
			{
				XmlDocument document = new XmlDocument();

				document.Load("Setting.xml");

				XmlNode node = document.SelectSingleNode("Setting");

				return node.Attributes[name].Value;
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		public static string getLastSetFileName()
		{
			try
			{
				return GetSet("LastSetFileName");
			}
			catch (Exception e)
			{
				throw e;
			}
		}

		public static bool getPrintState()
		{
			try
			{
				bool printState = false;
				string printStateString = GetSet("PrintState");

				if (printStateString.Equals("true")) printState = true;
				else printState = false;

				return printState;
			}
			catch (Exception e)
			{
				throw e;
			}
}
	}
}
