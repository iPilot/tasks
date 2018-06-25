using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Linq;
using System;

namespace SimQLTask
{
	public class SimModel
    {
		public JToken Data { get; }
        public SimQuery[] Queries { get; }

		private SimModel(JToken data, SimQuery[] queries)
		{
			Data = data;
			Queries = queries;
		}

		public static bool TryParse(string input, out SimModel result)
		{
			try
			{
				var obj = JObject.Parse(input);
				var data = obj["data"];
				if (data == null || !TryParseQueries(obj["queries"], out SimQuery[] queries))
				{
					result = null;
					return false;
				}
				result = new SimModel(data, queries);
				return true;
			}
			catch (Exception)
			{
				result = null;
				return false;
			}
		}

		private static bool TryParseQueries(JToken queries, out SimQuery[] parsedQueries)
		{

		}
    }
}
