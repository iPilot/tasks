using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Text;

namespace EvalTask
{
	public class Evaluator
	{
		private readonly Dictionary<string, string> Funcs;

		private readonly CodeDomProvider csharpProvider;
		private readonly CompilerParameters compilerParameters;
		private readonly string codeFormat;

		public Evaluator()
		{
			compilerParameters = new CompilerParameters { GenerateInMemory = true };
			compilerParameters.ReferencedAssemblies.Add("mscorlib.dll");
			csharpProvider = CodeDomProvider.CreateProvider("c#");

			codeFormat = @"
				using System;
				public static class DynamicExpression 
				{{
					public static double Eval() 
					{{ 
						return {0};
					}} 
				}}";

			Funcs = new Dictionary<string, string>
				{
					{ "max", "Math.Max" },
					{ "min", "Math.Min" },
					{ "sqrt", "Math.Sqrt" }
				};
		}

		private string PrepareExpression(string expression)
		{
			var builder = new StringBuilder(expression)
				.Replace(',', '.')
				.Replace(";", ",")
				.Replace("'", "");
			foreach (var func in Funcs)
				builder.Replace(func.Key, func.Value);
			return builder.ToString();
		}

		public double Evaluate(string expression)
		{
			var preparedExpression = PrepareExpression(expression);
			CompilerResults result = csharpProvider
				.CompileAssemblyFromSource(compilerParameters, string.Format(codeFormat, preparedExpression));
			if (result.Errors.HasErrors)
			{
				StringBuilder strB = new StringBuilder();
				foreach (CompilerError error in result.Errors)
					strB.AppendLine(string.Format("[pos: {0}] - {1}", error.Column, error.ErrorText));
				throw new ArgumentException($"Ошибка в выражении:\r\n{strB}", "expression");
			}

			return (double)result
				.CompiledAssembly
				.GetType("DynamicExpression")
				.GetMethod("Eval")
				.Invoke(null, null);
		}
	}
}
