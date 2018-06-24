using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

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
		public double AnswerIs4_WhenSomething(string input)
		{
			return Evaluator.Evaluate(input);
		}
	}
}
