using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace EvalTask
{
	class EvalProgram
	{
		static void Main(string[] args)
		{
			string input = Console.ReadLine();
			string json = Console.In.ReadToEnd();
			string output;
			var evaluator = new Evaluator(new StringConverter());
			try
			{
				var constants = string.IsNullOrWhiteSpace(json)
					? new Dictionary<string, double>()
					: JsonConvert.DeserializeObject<Dictionary<string, double>>(json);
				output = evaluator
					.Evaluate(input, constants)
					.ToString(CultureInfo.InvariantCulture);
			}
			catch
			{
				output = "error";
			}
			Console.WriteLine(output);
		}
	}
}
