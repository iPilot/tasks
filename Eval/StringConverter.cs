using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace EvalTask
{
	public class StringConverter
	{
		public string ConvertString(string expression, string json)
		{
			var jObject = JObject.Parse(json);

			foreach (var item in jObject)
			{
				expression = expression.Replace(item.Key, item.Value.ToString());
			}

			return expression;
		}
	}
}
