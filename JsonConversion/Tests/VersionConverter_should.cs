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
				Version = "3",
				Products = { new V3Product
				{
					Id = 1,
					Count = 100,
					Price = 10,
					Name = "product-name"
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
				Version = "3",
				Products =
				{
					new V3Product
					{
						Price = 12,
						Id = 1,
						Count = 100,
						Name = "Pen"
					},
					new V3Product
					{
						Id = 2,
						Name = "Pencil",
						Price = 8,
						Count = 1000
					},
					new V3Product
					{
						Id = 3,
						Name = "Box",
						Price = 12.1,
						Count = 50
					},
				}
			};
			expteted.Should().BeEquivalentTo(v3obj);
			Console.WriteLine(JsonConvert.SerializeObject(v3obj, Formatting.Indented, new JsonSerializerSettings{NullValueHandling = NullValueHandling.Ignore}));
		}
	}
}
