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

			string output = new Evaluator().Evaluate(input).ToString();
			Console.WriteLine(output, CultureInfo.InvariantCulture);
		}

	}

}
