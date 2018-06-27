using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EvalTask
{
	public class StringConverter : IExpressionFormatter
	{
		private readonly Dictionary<string, string> Funcs = new Dictionary<string, string>
			{
				{ "max(", "Math.Max(" },
				{ "min(", "Math.Min(" },
				{ "sqrt(", "Math.Sqrt(" },
				{ "sum(", "Sum(" }
			};

		public string Format(string expression)
		{
			var builder = new StringBuilder(expression);

			builder.Replace(',', '.')
				.Replace(";", ",")
				.Replace("'", "");

			foreach (var func in Funcs.OrderByDescending(f => f.Key))
				builder.Replace(func.Key, func.Value);

			return builder.ToString();
		}
	}
}
