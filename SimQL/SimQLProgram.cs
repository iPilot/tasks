using System;

namespace SimQLTask
{
	partial class SimQLProgram
	{
		static void Main(string[] args)
		{
			var json = Console.In.ReadToEnd();
			var parsed = SimModel.TryParse(json, out SimModel data);
			if (!parsed)
				return;
			var exec = new SimQueryExecutor(data);

			foreach (var query in exec.ExecuteQueries())
				Console.WriteLine(query);
		}
	}
}
