using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.IO;
using System.Reflection;

namespace JsonConversion
{
	[TestFixture]
	public class VersionConverter_should
	{
		[Test]
		public void Test()
		{
			string v2json = File.ReadAllText(@"C:\Users\iPilot\Source\Repos\tasks\JsonConversion\1.v2.json");
			var v2obj = JsonConvert.DeserializeObject<V2Object>(v2json);
			var v3obj = new VersionConverter().Convert(v2obj);
			var v3json = File.ReadAllText(@"C:\Users\iPilot\Source\Repos\tasks\JsonConversion\1.v3.json");
			var result = JsonConvert.SerializeObject(v3obj, Formatting.Indented);
			Assert.AreEqual(v3json, result);
		}
	}
}
