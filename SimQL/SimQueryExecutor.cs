using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace SimQLTask
{
	public class SimQueryExecutor
	{
		public SimModel Data { get; }

		public SimQueryExecutor(SimModel dataModel)
		{
			Data = dataModel;
		}

		public IEnumerable<string> ExecuteQueries()
		{
			foreach (var query in Data.Queries)
			{
				double result = 0;
				var values = GetValuesByPath(Data.Data, query.Path, 0);
				if (!values.Any())
				{
					yield return $"{query}";
					continue;
				}
				switch (query.Func)
				{
					case SimQueryFunc.Min: result = values.Min(); break;
					case SimQueryFunc.Max: result = values.Max(); break;
					case SimQueryFunc.Sum: result = values.Sum(); break;
					default: result = values.First(); break;
				}
				yield return $"{query} = {result.ToString(CultureInfo.InvariantCulture)}";
			}
		}

		private IEnumerable<double> GetValuesByPath(JToken data, string[] path, int index)
		{
			if (index > path.Length || data == null)
				return null;// Enumerable.Empty<double>();
			if (data.Type == JTokenType.Object && index < path.Length)
				return GetValuesByPath(data[path[index]], path, index + 1);
			else if (data.Type == JTokenType.Array)
			{
				IEnumerable<double> result = Enumerable.Empty<double>();
				foreach (var item in data)
					result = result.Concat(GetValuesByPath(item, path, index) ?? new double[0]);
				return result;
			}
			else if ((data.Type == JTokenType.Float || data.Type == JTokenType.Integer) && index == path.Length)
				return new[] { data.Value<double>() };
			return null;// Enumerable.Empty<double>();
		}
	}
}
