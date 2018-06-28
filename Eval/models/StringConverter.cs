using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace EvalTask
{
	public class StringConverter : IExpressionFormatter
	{
		private readonly Dictionary<string, string> Funcs = new Dictionary<string, string>
			{
				{ "max", "Math.Max(" },
				{ "min", "Math.Min(" },
				{ "sqrt", "Math.Sqrt(" },
			};

		public string Format(string expression)
		{
			var builder = new StringBuilder(expression);
			

			builder.Replace(',', '.')
				.Replace(";", ",")
				.Replace("'", "")
				.Replace("%", "*0.01");
			expression = builder.ToString();
			foreach (var func in Funcs.OrderByDescending(f => f.Key))
			{
				var regex = new Regex($"(^|[^[:alnum:]]){func.Key}\\s*\\(");
				expression = regex.Replace(expression, func.Value);
			}
			return builder.ToString();
		}
	}
}
