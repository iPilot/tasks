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

		public void PrepareJson(V3Object v3Object, Dictionary<string, string> constants)
		{
			if (constants == null)
				return;
			foreach (var product in v3Object.products)
			{
				product.Price = evaluator.Evaluate(ChangeSymbols(product.Price, constants)).ToString();
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
