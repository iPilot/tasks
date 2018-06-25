using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
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
			compilerParameters.ReferencedAssemblies.AddRange(new[] { "mscorlib.dll", "System.Core.dll" });
			csharpProvider = CodeDomProvider.CreateProvider("c#");

			codeFormat = @"
				using System;
				using System.Linq;
				public static class DynamicExpression 
				{{
					public static double Eval() 
					{{ 
						return {0};
					}}
					public static double Sum(params double[] values)
					{{
						return values.Sum();
					}}
				}}";

			Funcs = new Dictionary<string, string>
			{
				{ "max", "Math.Max" },
				{ "min", "Math.Min" },
				{ "sqrt", "Math.Sqrt" },
				{ "sum", "Sum" }
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
			var code = string.Format(codeFormat, preparedExpression);
			CompilerResults result = csharpProvider
				.CompileAssemblyFromSource(compilerParameters, code );
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

		public static double Sum(params double[] values)
		{
			return values.Sum();
		}
	}
}
