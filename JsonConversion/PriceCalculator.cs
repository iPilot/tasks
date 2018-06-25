using System.Collections.Generic;
using EvalTask;

namespace JsonConversion
{
	public class PriceCalculator
	{
		private readonly IEvaluator _evaluator;

		public PriceCalculator(IEvaluator evaluator)
		{
			_evaluator = evaluator;
		}

		public void PrepareJson(V3Object v3Object, Dictionary<string, string> constants)
		{
			if (constants == null)
				return;
			foreach (var product in v3Object.products)
			{
				product.price = _evaluator.Evaluate(ChangeSymbols(product.price, constants)).ToString();
			}
		}

		private string ChangeSymbols(string input, Dictionary<string, string> constants)
		{
			foreach (var constant in constants)
			{
				input = input.Replace(constant.Key, constant.Value);
			}

			return input;
		}
	}
}
