using System;
using System.Globalization;

namespace EvalTask
{
	class EvalProgram
	{
		static void Main(string[] args)
		{
			string input = Console.In.ReadToEnd();
			var parsedString = input.Replace(',', '.');
			string output = new Evaluator().Evaluate(parsedString).ToString();
			Console.WriteLine(output, CultureInfo.InvariantCulture);
		}

	}

}
