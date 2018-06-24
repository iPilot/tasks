using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SimQLTask
{
	class SimQLProgram
	{
		static void Main(string[] args)
		{
			var json = Console.In.ReadToEnd();
			//var json = "{\"data\":{\"empty\":{},\"ab\":0,\"x1\":1,\"x2\":2,\"y1\":{\"y2\":{\"y3\":3}}},\"queries\":[\"empty\",\"xyz\",\"x1.x2\",\"y1.y2.z\",\"empty.foobar\"]}";

			foreach (var result in ExecuteQueries(json))
				Console.WriteLine(result);
		}

		public static IEnumerable<string> ExecuteQueries(string json)
		{
			var jObject = JObject.Parse(json);
			var data = (JObject)jObject["data"];
			var queries = jObject["queries"].ToObject<string[]>();

		    foreach (var query in queries)
		    {
		        yield return FormatOutput(query, GetPathValue(query, data));
		    }
		}

	    public static string GetPathValue(string query, JObject data)
	    {
	        var queryParts = query.Split('.');
	        JToken currentPart = null;

	        foreach (var part in queryParts)
	        {
				if (currentPart != null && currentPart.Type != JTokenType.Object)
					return string.Empty;

	            currentPart = currentPart?[part] ?? data[part];

				if(currentPart == null)
				{
					return string.Empty;
				}
	        }

			return ConvertValue(currentPart);
	    }

	    public static string FormatOutput(string query, string queryValue)
	    {
	        return $"{query} = {queryValue}";
	    }

		public static string ConvertValue(JToken token)
		{
			try
			{
				return token.Value<double>().ToString(CultureInfo.InvariantCulture);
			}
			catch (InvalidCastException)
			{
				return string.Empty;
			}
		}
	}
}
