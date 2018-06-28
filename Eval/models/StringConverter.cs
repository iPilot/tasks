using System.Text;

namespace EvalTask
{
	public class StringConverter : IExpressionFormatter
	{
		public string Format(string expression)
		{
			return new StringBuilder(expression)
				.Replace(',', '.')
				.Replace(";", ",")
				.Replace("'", "")
				.Replace("%", "*0.01")
				.ToString();
		}
	}
}
