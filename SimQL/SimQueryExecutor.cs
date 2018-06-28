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
				double r = 0;
				var values = GetValuesByPath(Data.Data, query.Path, 0);
				if (values == null || !values.Any() && query.Func != SimQueryFunc.Sum)
				{
					yield return query.ToString();
					continue;
				}
				switch (query.Func)
				{
					case SimQueryFunc.Min: r = values.Min(); break;
					case SimQueryFunc.Max: r = values.Max(); break;
					case SimQueryFunc.Sum: r = values.Sum(); break;
					default: r = values.First(); break;
				}
				yield return $"{query} = {r.ToString(CultureInfo.InvariantCulture)}";
			}
		}

		private IEnumerable<double> GetValuesByPath(JToken data, string[] path, int index)
		{
			if (index > path.Length || data == null)
				return null;
			if (data.Type == JTokenType.Object && index < path.Length)
				return GetValuesByPath(data[path[index]], path, index + 1);
			else if (data.Type == JTokenType.Array)
			{
				IEnumerable<double> result = Enumerable.Empty<double>();
				var found = false;
				foreach (var item in data)
				{
					var next = GetValuesByPath(item, path, index);
					found = found || next != null;
					result = result.Concat(next ?? new double[0]);
				}
				return found || index == path.Length ? result : null;
			}
			else if ((data.Type == JTokenType.Float || data.Type == JTokenType.Integer) && index == path.Length)
				return new[] { data.Value<double>() };
			return null;
		}
	}
}
