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

		public double GetPrice(string expression)
		{
			return evaluator.Evaluate(expression);
		}
	}
}
