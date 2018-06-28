using System;

namespace SimQLTask
{
	public class SimQLProgram
	{
		static void Main(string[] args)
		{
			//var json = Console.In.ReadToEnd();
			var json = @"{""data"":{""empty"":[],""x"":[0.1,0.2,0.3],""a"":[{""b"":10,""c"":[1,2,3]},{""b"":30,""c"":[4]},{""d"":500}]},""queries"":[""sum(empty)"",""sum(a.b)"",""sum(a.c)"",""sum(a.d)"",""sum(x)""]}";
			var parsed = SimModel.TryParse(json, out SimModel data);
			if (!parsed)
				return;
			var exec = new SimQueryExecutor(data);

			foreach (var query in exec.ExecuteQueries())
				Console.WriteLine(query);
		}
	}
}
