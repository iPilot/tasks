using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SimQLTask
{
	partial class SimQLProgram
	{
		static void Main(string[] args)
		{
			//var json = Console.In.ReadToEnd();
			//var json = "{\"data\":{\"empty\":{},\"ab\":0,\"x1\":1,\"x2\":2,\"y1\":{\"y2\":{\"y3\":3}}},\"queries\":[\"empty\",\"xyz\",\"x1.x2\",\"y1.y2.z\",\"empty.foobar\"]}";
			var json = @"{
    'data': {'a':{'x':3.14, 'b':[{'c':15}, {'c':9}]}, 'z':[2.65, 35]},
    'queries': [
        'sum(a.b.c)',
        'min(z)',
        'max(a.x)'
    ]
}";
			var parsed = SimModel.TryParse(json, out SimModel data);
			if (!parsed)
				return;
			var exec = new SimQueryExecutor(data);

			foreach (var query in exec.ExecuteQueries())
				Console.WriteLine(query);
		}

		public static IEnumerable<string> ExecuteQueries(string json)
		{
			var jObject = ParseJson(json);

			if(jObject == null)
				yield break;

			var data = jObject["data"];
			var queries = jObject["queries"];

			if (data == null || queries == null)
				yield break;

		    foreach (var query in queries.ToObject<string[]>())
		    {
				var parsedQuery = ParseQuery(query);
				var value = GetPathValue(parsedQuery.Item2, data);
				var result = 0.0;
				if (!value.Any())
				{
					yield return FormatOutput(parsedQuery.Item2, "");
					continue;
				}
				switch (parsedQuery.Item1)
				{
					case "sum": result = value.Sum(); break;
					case "min": result = value.Min(); break;
					case "max": result = value.Max(); break;
					default:
						{
							var z = value.Any() ? value.First().ToString(CultureInfo.InvariantCulture) : "";
							yield return FormatOutput(query, z);
							continue;
						}
				}
		        yield return FormatOutput(query, result.ToString(CultureInfo.InvariantCulture));
		    }
		}
		 
		public static Tuple<string, string> ParseQuery(string query)
		{
			var validFuncs = new[] { "sum", "max", "min" };
			var openBracket = query.IndexOf('(');
			var closeBracket = query.IndexOf(')');
			var func = string.Empty;
			if (openBracket != -1)
				func = query.Substring(0, openBracket);
			if (openBracket != -1 && closeBracket != -1 && closeBracket == query.Length - 1 && validFuncs.Contains(func))
				return Tuple.Create(func, query.Substring(openBracket + 1, closeBracket - openBracket - 1));
			return Tuple.Create(string.Empty, query);
		}

	    public static IEnumerable<double> GetPathValue(string query, JToken data)
	    {
	        var queryParts = query.Split('.');
	        JToken currentPart = null;

	        foreach (var part in queryParts)
	        {
				if (currentPart != null && currentPart.Type != JTokenType.Object && currentPart.Type != JTokenType.Array)
					return Enumerable.Empty<double>();

				if (currentPart.Type == JTokenType.Array)
					return null;

				currentPart = currentPart?[part] ?? data[part];
				

				if(currentPart == null)
				{
					return Enumerable.Empty<double>();
				}
	        }

			return ConvertValue(currentPart);
	    }

	    public static string FormatOutput(string query, string queryValue)
	    {
	        return $"{query}" + (string.IsNullOrEmpty(queryValue) ? "" : $" = {queryValue}");
	    }

		public static double[] ConvertValue(JToken token)
		{
			try
			{
				if (token.Type == JTokenType.Array)
				{
					token.Values().Where(v => v != null).Select(v => v.Value<double>());
				}

				return token.Value<double[]>();
			}
			catch (InvalidCastException)
			{
				return new double[0];
			}
		}

		public static JObject ParseJson(string inputMessage)
		{
			try
			{
				return JObject.Parse(inputMessage);
			}
			catch (Exception e)
			{
				return null;
			}

		}

		public static string GetPath(string query)
		{
			var openBracketPos = query.IndexOf('(');
			var closeBrackerPos = query.IndexOf(')');

			if (openBracketPos == -1)
				return query;

			return query.Substring(openBracketPos, closeBrackerPos);
		}
	}
}
