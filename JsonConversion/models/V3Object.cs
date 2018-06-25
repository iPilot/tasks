using Newtonsoft.Json;
using System.Collections.Generic;

namespace JsonConversion
{
	public class V3Object
	{
		[JsonProperty("version")]
		public string Version { get; set; }

		[JsonProperty("products")]
		public List<V3Product> Products { get; } = new List<V3Product>();
	}
}
