using System;
using System.CodeDom.Compiler;
using System.Text;

namespace EvalTask
{
	public class Evaluator
	{
		readonly CodeDomProvider csharpProvider;
		readonly CompilerParameters compilerParameters;
		readonly string codeFormat;

		public Evaluator()
		{
			compilerParameters = new CompilerParameters {GenerateInMemory = true};
			compilerParameters.ReferencedAssemblies.Add("mscorlib.dll");

			csharpProvider = CodeDomProvider.CreateProvider("c#");

			codeFormat = 
				@"using System;
public static class DynamicExpression 
{
	public static double Eval() 
	{ 
		return {0};
	} 
}";
		}

		public double Evaluate(string expression)
		{
			expression = expression.Replace(',', '.');
			expression = expression.Replace("'", "");
			expression = expression.Replace(" ", "");
			expression = expression.Replace("max", "Math.Max");
			expression = expression.Replace("min", "Math.Min");
			expression = expression.Replace("sqrt", "Math.Sqrt");

			CompilerResults result = csharpProvider
				.CompileAssemblyFromSource(compilerParameters, string.Format(codeFormat, expression));
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
