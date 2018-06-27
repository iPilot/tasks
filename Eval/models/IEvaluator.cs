using System.Collections.Generic;

namespace EvalTask
{
	public interface IEvaluator
	{
		double Evaluate(string expression);
		double Evaluate(string expression, IDictionary<string, double> constants);
	}
}