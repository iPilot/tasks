using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EvalTask
{
	public interface IStringConverter
	{
		string ConvertString(string expression, string json);
	}

	public class StringConverter : IStringConverter
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
