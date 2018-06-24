using EvalTask;

namespace JsonConversion
{
	internal class VersionConverter : IConverter
	{
		public V3Object Convert(V2Object obj)
		{
			var result = new V3Object
			{
				version = "3",
			};
			foreach (var item in obj.Products)
			{
				result.products.Add(new V3Product
				{
					id = item.Key,
					name = item.Value.Name,
					price = item.Value.Price,
					count = item.Value.Count
				});
			}
			new PriceCalculator(new Evaluator()).PrepateJson(result, obj.constants);
			return result;
		}
	}
}
