using Newtonsoft.Json.Linq;
using System.Linq;
using System;
using System.Collections.Generic;

namespace SimQLTask
{
	public class SimModel
	{
		private static Dictionary<string, SimQueryFunc> validFuncs =
			new Dictionary<string, SimQueryFunc>
			{
				{ "min", SimQueryFunc.Min },
				{ "max", SimQueryFunc.Max },
				{ "sum", SimQueryFunc.Sum }
			};

		public JToken Data { get; }
		public SimQuery[] Queries { get; }

		private SimModel(JToken data, IEnumerable<SimQuery> queries)
		{
			Data = data;
			Queries = queries.ToArray();
		}

		public static bool TryParse(string input, out SimModel result)
		{
			try
			{
				var obj = JObject.Parse(input);
				var data = obj["data"];
				if (data == null)
				{
					result = null;
					return false;
				}
				result = new SimModel(data, ParseQueries(obj["queries"]));
				return true;
			}
			catch (Exception)
			{
				result = null;
				return false;
			}
		}

		private static IEnumerable<SimQuery> ParseQueries(JToken queries)
		{
			if (queries == null || queries.Type != JTokenType.Array)
				throw new ArgumentException("queries");
			foreach (var item in queries)
			{
				var q = item.ToString();
				var openBr = q.IndexOf('(');
				var closeBr = q.IndexOf(')');
				if (openBr == -1 && closeBr == -1)
				{
					yield return new SimQuery(q);
					continue;
				}
				if (openBr != -1 && closeBr != -1 && closeBr - openBr > 1 && closeBr == q.Length - 1)
				{
					var func = q.Substring(0, openBr);
					var path = q.Substring(openBr + 1, closeBr - openBr - 1);
					if (!validFuncs.ContainsKey(func))
						throw new ArgumentException("queries");
					yield return new SimQuery(path, validFuncs[func]);
					continue;
				}
				throw new ArgumentException("queries");
			}
		}
	}
}
