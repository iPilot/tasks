using Newtonsoft.Json;
using System.Collections.Generic;

namespace JsonConversion
{
	internal class V2Object
	{
		[JsonProperty("version")]
		public string Version { get; set; }

		[JsonProperty("products")]
		public Dictionary<string, V2Product> Products { get; set; }

		[JsonProperty("constants")]
		public Dictionary<string, string> Constants { get; set; }
	}
}
