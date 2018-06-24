using System.Collections.Generic;

namespace JsonConversion
{
	internal class V3Object
	{
		public string Version { get; set; }
		public List<V3Product> Products { get; } = new List<V3Product>();
	}
}
