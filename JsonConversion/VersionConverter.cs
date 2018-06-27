using System.Collections.Generic;
using System.Linq;
using EvalTask;

namespace JsonConversion
{
	public static class VersionConverter
	{
		public static V3Object Convert(V2Object obj, IEvaluator evaluator)
		{
			var result = new V3Object {	Version = "3" };
			foreach (var item in obj.Products)
				result.Products.Add(new V3Product
				{
					Id = long.Parse(item.Key),
					Name = item.Value.Name,
					Price = item.Value.Price != null
						? evaluator.Evaluate(item.Value.Price, obj.Constants ?? new Dictionary<string, double>())
						: (double?)null,
					Count = item.Value.Count,
					Dimensions = GetDimensions(item.Value.Size)
				});
			return result;
		}

		private static Dimensions GetDimensions(double[] arr)
		{
			if (arr == null || !arr.Any())
				return null;
			return new Dimensions { Width = arr[0], Length = arr[2], Height = arr[1] };
		}
	}
}