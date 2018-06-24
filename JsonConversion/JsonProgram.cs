using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;

namespace JsonConversion
{

	class JsonProgram
	{ 
		static void Main()
		{
			string json = Console.In.ReadToEnd();
			var v2obj = JsonConvert.DeserializeObject<V2Object>(json);
			var v3obj = new VersionConverter().Convert(v2obj);
			Console.Write(JsonConvert.SerializeObject(v3obj, Formatting.Indented));
		}
	}
}
