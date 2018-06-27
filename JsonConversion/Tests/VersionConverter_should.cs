using Newtonsoft.Json;
using NUnit.Framework;
using System.IO;
using System.Reflection;
using FluentAssertions;
using NUnit.Framework.Internal;
using EvalTask;

namespace JsonConversion
{
	[TestFixture]
	public class VersionConverter_should
	{
		string projectPath;
		IEvaluator evaluator;

		[SetUp]
		public void SetUp()
		{
			projectPath = Directory
				.GetParent(Assembly.GetExecutingAssembly().Location)
				.Parent
				.Parent
				.FullName + "\\data\\";
			evaluator = new Evaluator(new StringConverter());
		}

		[Test]
		[TestCase("1.v2.json", "1.v3.json", TestName = "Json1Problem")]
		[TestCase("2.v2.json", "2.v3.json", TestName = "Json2Problem")]
		[TestCase("3.v2.json", "3.v3.json", TestName = "Json3Problem")]
		public void ConverterTests(string jsonV2File, string jsonV3File)
		{
			var v2json = File.ReadAllText(projectPath + jsonV2File);
			var v3json = File.ReadAllText(projectPath + jsonV3File);

			var v2obj = JsonConvert.DeserializeObject<V2Object>(v2json);
			var evaluator = new Evaluator(new StringConverter());
			var v3obj = VersionConverter.Convert(v2obj, evaluator);
			var v3obj_expected = JsonConvert.DeserializeObject<V3Object>(v3json);

			v3obj.Should().BeEquivalentTo(v3obj_expected);
		}
	}
}
