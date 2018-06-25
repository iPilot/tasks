using Newtonsoft.Json;

namespace JsonConversion
{
	public class V3Product
	{
		[JsonProperty("id")]
		public long Id { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("price", NullValueHandling = NullValueHandling.Ignore)]
		public double? Price { get; set; }

		[JsonProperty("count", NullValueHandling = NullValueHandling.Ignore)]
		public long? Count { get; set; }

		[JsonProperty("dimensions", NullValueHandling = NullValueHandling.Ignore)]
		public Dimensions Dimensions { get; set; }
	}
}