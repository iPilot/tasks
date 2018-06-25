using Newtonsoft.Json;

namespace JsonConversion
{
	public class Dimensions
	{
		[JsonProperty("l")]
		public double Length { get; set; }

		[JsonProperty("w")]
		public double Width { get; set; }

		[JsonProperty("h")]
		public double Height { get; set; }
	}
}