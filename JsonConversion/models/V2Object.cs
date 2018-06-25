using System.Collections.Generic;

namespace JsonConversion
{
	internal class V2Object
	{
		public string Version { get; set; }
		public Dictionary<string, V2Product> Products { get; set; } 
		public Dictionary<string, string> Constants { get; set; }
	}
}
