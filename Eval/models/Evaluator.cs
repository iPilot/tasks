using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace EvalTask
{
	public class Evaluator : IEvaluator
	{
		private static readonly CodeDomProvider csharpProvider;
		private static readonly CompilerParameters compilerParameters;
		private static readonly string codeFormat;

		protected IExpressionFormatter formatter;

		static Evaluator()
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
						{0}
						return {1};
					}}
				}}";
		}

		public Evaluator(IExpressionFormatter formatter)
		{
			this.formatter = formatter;
		}

		public double Evaluate(string expression, IDictionary<string, double> constants)
		{
			var code = string.Format(codeFormat,
				GetConstantsString(constants),
				formatter.Format(expression));
			CompilerResults result = csharpProvider.CompileAssemblyFromSource(compilerParameters, code);
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

		private string GetConstantsString(IDictionary<string, double> contants)
		{
			return string.Join("\n", 
				contants.Select(c => $"var {c.Key} = {c.Value.ToString(CultureInfo.InvariantCulture)};"));
		}

		public double Evaluate(string expression)
		{
			return Evaluate(expression, new Dictionary<string, double>());
		}
	}
}
