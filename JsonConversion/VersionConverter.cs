using EvalTask;

namespace JsonConversion
{
	internal class VersionConverter : IConverter
	{
		public V3Object Convert(V2Object obj)
		{
			var cacl = new PriceCalculator(new Evaluator());
			var result = new V3Object
			{
				version = "3",
			};
			foreach (var item in obj.Products)
			{
				result.products.Add(new V3Product
				{
					id = long.Parse(item.Key),
					name = item.Value.Name,
					price = cacl.PrepateJson(item.Value.Price, obj.constants),
					count = item.Value.Count
				});
			}
			return result;
		}
	}
}
