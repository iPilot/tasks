using FluentAssertions;
using NUnit.Framework;
using System.IO;
using System.Reflection;

namespace SimQLTask.Tests
{
	[TestFixture]
	public class SimQueryExecutor_should
	{
		private string assemblyPath;

		[SetUp]
		public void SetUp()
		{
			assemblyPath = Directory.GetParent(Assembly.GetExecutingAssembly().Location).Parent.Parent.FullName;
		}

		[Test]
		[TestCase("1")]
		[TestCase("2")]
		[TestCase("3")]
		public void Work_Correctly(string fileName)
		{
			var input = File.ReadAllText($"{assemblyPath}\\Tests\\data\\{fileName}.in");
			var expectedOutput = File.ReadLines($"{assemblyPath}\\Tests\\data\\{fileName}.out");

			SimModel.TryParse(input, out var data);
			var result = new SimQueryExecutor(data).ExecuteQueries();

			result.Should().BeEquivalentTo(expectedOutput);
		}
	}
}
