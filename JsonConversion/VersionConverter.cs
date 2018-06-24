namespace JsonConversion
{
	internal class VersionConverter : IConverter
	{
		public V3Object Convert(V2Object obj)
		{
			var result = new V3Object {	Version = "3" };
			foreach (var item in obj.Products)
			{
				result.Products.Add(new V3Product
				{
					Id = long.Parse(item.Key),
					Name = item.Value.Name,
					Price = item.Value.Price,
					Count = item.Value.Count
				});
			}
			return result;
		}
	}
}
