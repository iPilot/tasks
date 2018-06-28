using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace EvalTask
{
	class EvalProgram
	{
		private const string errorString = "error";

		static void Main(string[] args)
		{
			var reader = args.Length == 0 ? Console.In : new StreamReader(args[0]);
			string input = reader.ReadLine();
			string json = reader.ReadToEnd();
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
				output = errorString;
			}
			Console.WriteLine(output);
		}
	}
}
