using NUnit.Framework;
using System;
using FluentAssertions;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace EvalTask
{
	[TestFixture]
	public class Evaluator_Should
	{
		private Evaluator evaluator;
		private StringConverter stringConverter;
		private Dictionary<string, double> contants;

		[SetUp]
		public void SetUp()
		{
			stringConverter = new StringConverter();
			evaluator = new Evaluator(stringConverter);
			var projectPath = Directory.GetParent(Assembly.GetExecutingAssembly().Location).Parent.Parent.FullName;
			var sampleJson = File.ReadAllText($"{projectPath}\\Tests\\sample.json");
			contants = JsonConvert.DeserializeObject<Dictionary<string, double>>(sampleJson);
		}

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
		[TestCase("-2*3", ExpectedResult = -6)]
		[TestCase("sqrt(sqrt(5*5.0)+sqrt(10+6))", ExpectedResult = 3)]
		[TestCase("sqrt (25.0)", ExpectedResult = 5)]
		[TestCase("12%", ExpectedResult = 0.12)]
		public double AnswerIs4_WhenSomething(string input)
		{
			return evaluator.Evaluate(input);
		}

		[Test]
		[TestCase("12 12")]
		[TestCase("     ")]
		[TestCase(" 1/0 ")]
		public void ThrowArgumentException_WhenIncorrecpInput(string input)
		{
			Action action = () => evaluator.Evaluate(input);
			action.Should().Throw<ArgumentException>();
		}

		[TestCase("a+b", ExpectedResult = 3)]
		[TestCase("a+a+b", ExpectedResult = 4)]
		[TestCase("a+a+b+2", ExpectedResult = 6)]
		[TestCase("max(a; b)*3+min(a; b)*5-sqrt(50*b)", ExpectedResult = 1)]
		public double ReplaceConstantsCorrectly(string input)
		{
			return evaluator.Evaluate(input, contants);
		}
	}
}
