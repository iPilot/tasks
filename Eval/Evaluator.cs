using System;
using System.CodeDom.Compiler;
using System.Text;

namespace EvalTask
{
	public class Evaluator : IEvaluator
	{
		readonly CodeDomProvider csharpProvider;
		readonly CompilerParameters compilerParameters;
		readonly string baseCodeBegin;
		readonly string baseCodeEnd;

		public Evaluator()
		{
			compilerParameters = new CompilerParameters {GenerateInMemory = true};
			compilerParameters.ReferencedAssemblies.Add("mscorlib.dll");

			csharpProvider = CodeDomProvider.CreateProvider("c#");

			baseCodeBegin =
				"using System; public static class DynamicExpression { public static double Eval() { return \r\n";
			baseCodeEnd = "\r\n; } }";
		}

		public double Evaluate(string expression)
		{
			//expression.Replace('\'', )

			CompilerResults result = csharpProvider.CompileAssemblyFromSource(compilerParameters,
				string.Concat(baseCodeBegin, expression, baseCodeEnd));
			if (result.Errors.HasErrors)
			{
				StringBuilder strB = new StringBuilder();
				foreach (CompilerError error in result.Errors)
					strB.AppendLine(string.Format("[pos: {0}] - {1}", error.Column, error.ErrorText));
				throw new ArgumentException($"Ошибка в выражении:\r\n{strB}", "expression");
			}

			return (double)result.CompiledAssembly.GetType("DynamicExpression").GetMethod("Eval").Invoke(null, null);
		}
	}

}
