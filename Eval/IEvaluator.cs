using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvalTask
{
	public interface IEvaluator
	{
		double Evaluate(string expression);
	}
}
