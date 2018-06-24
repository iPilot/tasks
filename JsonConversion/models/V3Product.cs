namespace JsonConversion
{
	public class V3Product
	{
		public long id { get; set; }
		public string name { get; set; }
		public double price { get; set; }
		public long count { get; set; }
		public Dimensions dimensions { get; set; }
	}

	public class Dimensions
	{
		public double l { get; set; }
		public double w { get; set; }
		public double h { get; set; }
	}
}