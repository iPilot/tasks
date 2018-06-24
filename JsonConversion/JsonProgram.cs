using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using EvalTask;

namespace JsonConversion
{

	class JsonProgram
	{ 
		static void Main()
		{
			string json = Console.In.ReadToEnd();
			var v2obj = JsonConvert.DeserializeObject<V2Object>(json);
			var v3obj = new VersionConverter().Convert(v2obj);
			
			Console.Write(JsonConvert.SerializeObject(v3obj, Formatting.Indented));
		}
	}

	public class PriceCalculator
	{
		private readonly IEvaluator _evaluator;

		public PriceCalculator(IEvaluator evaluator)
		{
			_evaluator = evaluator;
		}

		public void PrepateJson(V3Object v3Object, Dictionary<string, string> constants)
		{
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
