using System.Collections.Generic;
using EvalTask;

namespace JsonConversion
{
	public class PriceCalculator
	{
		private readonly Evaluator evaluator;

		public PriceCalculator(Evaluator evaluator)
		{
			this.evaluator = evaluator;
		}

		public double GetPrice(string expression, Dictionary<string, string> constants)
		{
			return evaluator.Evaluate(ReplaceConstants(expression, constants));
		}

		private string ReplaceConstants(string input, Dictionary<string, string> constants)
		{
			foreach (var constant in constants)
			{
				input = input.Replace(constant.Key, constant.Value);
			}

			return input;
		}
	}
}
