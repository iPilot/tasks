using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using EvalTask;

namespace JsonConversion
{
	[TestFixture]
	public class VersionConverter_should
	{
		[Test]
		[Ignore("")]
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

	[TestFixture]
	public class VersionConverter_should_Should
	{
		[Test]
		public void DoSomething_WhenSomething()
		{
			var v2json = File.ReadAllText(@"C:\Users\User3\Downloads\JsonSamples2\2.v2.json");
			var v2obj = JsonConvert.DeserializeObject<V2Object>(v2json);
			var v3obj = new VersionConverter().Convert(v2obj);
			Assert.AreEqual(v3obj.version, "3");
			Assert.AreEqual(v3obj.products.First().price, "10");
			Assert.AreEqual(v3obj.products.First().count, "100");
			Assert.AreEqual(v3obj.products.First().id, "1");
			Assert.AreEqual(v3obj.products.First().name, "product-name");
		}

		[Test]
		public void Test2()
		{
			var v2json = File.ReadAllText(@"C:\Users\User3\Downloads\JsonSamples1\1.v2.json");
			var v2obj = JsonConvert.DeserializeObject<V2Object>(v2json);
			var v3obj = new VersionConverter().Convert(v2obj);
			Assert.AreEqual(v3obj.version, "3");
			Assert.AreEqual(v3obj.products.First().price, "10");
			Assert.AreEqual(v3obj.products.First().count, 100);
			Assert.AreEqual(v3obj.products.First().id, 1);
			Assert.AreEqual(v3obj.products.First().name, "product-name");
		}
	}
}
