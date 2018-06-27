using Newtonsoft.Json;
using System.Collections.Generic;

namespace EvalTask
{
	public class Formula
	{
		public string Expression { get; }
		public IDictionary<string, double> Constants { get; }

		public Formula(string expression, IDictionary<string, double> constants)
		{
			Expression = expression;
			Constants = constants;
		}

		public Formula(string expression, string jsonConstants = null)
		{
			Expression = expression;
			Constants = string.IsNullOrWhiteSpace(jsonConstants)
				? new Dictionary<string, double>()
				: JsonConvert.DeserializeObject<Dictionary<string, double>>(jsonConstants);
		}
	}
}
