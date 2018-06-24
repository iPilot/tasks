using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using EvalTask;
using FluentAssertions;
using NUnit.Framework.Internal;

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
			var expected = new V3Object
			{
				version = "3",
				products = { new V3Product
				{
					id = 1,
					count = 100,
					price = 10,
					name = "product-name"
				}}

			};
			v3obj.Should().BeEquivalentTo(expected);
		}

		[Test]
		public void Test2()
		{
			var v2json = File.ReadAllText(@"C:\Users\User3\Downloads\JsonSamples1\1.v2.json");
			var v2obj = JsonConvert.DeserializeObject<V2Object>(v2json);
			var v3obj = new VersionConverter().Convert(v2obj);
			var expteted = new V3Object
			{
				version = "3",
				products =
				{
					new V3Product
					{
						price = 12,
						id = 1,
						count = 100,
						name = "Pen"
					},
					new V3Product
					{
						id = 2,
						name = "Pencil",
						price = 8,
						count = 1000
					},
					new V3Product
					{
						id = 3,
						name = "Box",
						price = 12.1,
						count = 50
					},
				}
			};
			expteted.Should().BeEquivalentTo(v3obj);
		}
	}
}
