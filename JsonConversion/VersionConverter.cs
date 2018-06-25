using System.Linq;
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
				version = "3"
			};
			foreach (var item in obj.Products)
				result.products.Add(new V3Product
				{
					id = long.Parse(item.Key),
					name = item.Value.Name,
					price = cacl.PrepateJson(item.Value.Price, obj.constants),
					count = item.Value.Count,
					dimensions = GetDimensions(item.Value.size)
				});
			return result;
		}

		private Dimensions GetDimensions(double[] arr)
		{
			if (arr == null || arr.Any())
				return null;
			return new Dimensions {w = arr[0], l = arr[2], h = arr[1]};
		}
	}
}