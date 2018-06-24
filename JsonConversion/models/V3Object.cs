using System.Collections.Generic;

namespace JsonConversion
{
	public class V3Object
	{
		public string version { get; set; }
		public List<V3Product> products { get; } = new List<V3Product>();
	}
}
