using System.Linq;
using EvalTask;

namespace JsonConversion
{
	internal class VersionConverter
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
					Id = long.Parse(item.Key),
					Name = item.Value.Name,
					Price = cacl.PrepareJson(item.Value.Price, obj.Constants),
					Count = item.Value.Count,
					Dimensions = GetDimensions(item.Value.Size)
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