using NUnit.Framework;
using System.IO;
using System.Reflection;

namespace EvalTask
{
	[TestFixture]
	public class Evaluator_Should
	{
		public Evaluator Evaluator { get; private set; }

		[SetUp]
		public void SetUp() => Evaluator = new Evaluator();

		[TestCase("2+2", ExpectedResult = 4)]
		[TestCase("2+2*2", ExpectedResult = 6)]
		[TestCase("(2+2)*2", ExpectedResult = 8)]
		[TestCase("3.5*3", ExpectedResult = 10.5)]
		[TestCase("2/2", ExpectedResult = 1)]
		[TestCase("2 + 2", ExpectedResult = 4)]
		[TestCase("2            + 2", ExpectedResult = 4)]
		[TestCase("2-5", ExpectedResult = -3)]
		[TestCase("2.0/0", ExpectedResult = double.PositiveInfinity)]
		[TestCase("0.0/0", ExpectedResult = double.NaN)]
		[TestCase("10'000", ExpectedResult = 10000)]
		[TestCase("max(10.0;6,0)+sqrt(4)", ExpectedResult = 12)]
		public double AnswerIs4_WhenSomething(string input)
		{
			return Evaluator.Evaluate(input);
		}
	}

	[TestFixture]
	public class StringConverter_Should
	{
		private StringConverter StringConverter;
		private string ProjectPath;
		private string SampleJson;

		[SetUp]
		public void SetUp()
		{
			StringConverter = new StringConverter();
			ProjectPath = Directory.GetParent(Assembly.GetExecutingAssembly().Location).Parent.Parent.FullName;
			SampleJson = File.ReadAllText($"{ProjectPath}\\Tests\\sample.txt");
		}


		[TestCase("a+b", ExpectedResult = "1+2")]
		[TestCase("a+a+b", ExpectedResult = "1+1+2")]
		[TestCase("a+a+b+2", ExpectedResult = "1+1+2+2")]
		public string ReplaceConstantsCorrectly(string input)
		{
			return StringConverter.ConvertString(input, SampleJson);
		}
	}
}
