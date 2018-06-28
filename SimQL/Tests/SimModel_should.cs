using FluentAssertions;
using NUnit.Framework;
using System.IO;
using System.Reflection;

namespace SimQLTask.Tests
{
	[TestFixture]
	class SimModel_should
	{
		private string projectPath;

		[SetUp]
		public void SetUp()
		{
			projectPath = Directory.GetParent(Assembly.GetExecutingAssembly().Location).Parent.Parent.FullName;
		}

		[Test]
		[TestCase("123")]
		public void ReturnFalseAndNull_WhenParseFailed(string fileName)
		{
			var json = File.ReadAllText($"{projectPath}\\Tests\\data\\{fileName}");

			var result = SimModel.TryParse(json, out var data);

			result.Should().BeFalse();
			data.Should().BeNull();
		}
	}
}
