using System;
using System.Globalization;

namespace EvalTask
{
	class EvalProgram
	{
		static void Main(string[] args)
		{
			string input = Console.ReadLine();
			string json = Console.In.ReadToEnd();

			if (!string.IsNullOrWhiteSpace(json))
			{
				input = new StringConverter().ConvertString(input, json);
			}
			var parsedString = input.Replace(',', '.');
			string output = new Evaluator().Evaluate(parsedString).ToString();
			Console.WriteLine(output, CultureInfo.InvariantCulture);
		}

	}

}
