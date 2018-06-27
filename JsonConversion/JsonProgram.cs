using Newtonsoft.Json;
using System;
using EvalTask;

namespace JsonConversion
{
	class JsonProgram
	{ 
		static void Main()
		{
			string json = Console.In.ReadToEnd();

			var v2obj = JsonConvert.DeserializeObject<V2Object>(json);
			var evaluator = new Evaluator(new StringConverter());
			var v3obj = VersionConverter.Convert(v2obj, evaluator);

			Console.Write(JsonConvert.SerializeObject(v3obj, Formatting.Indented));
		}
	}
}
