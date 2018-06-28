using System;

namespace SimQLTask
{
	partial class SimQLProgram
	{
		static void Main(string[] args)
		{
			//var json = Console.In.ReadToEnd();
			//var json = "{\"data\":{\"empty\":{},\"ab\":0,\"x1\":1,\"x2\":2,\"y1\":{\"y2\":{\"y3\":3}}},\"queries\":[\"empty\",\"xyz\",\"x1.x2\",\"y1.y2.z\",\"empty.foobar\"]}";
			var json = @"{
	'data': {'a':{'x':3.14, 'b':[{'c':15}, {'c':9}]}, 'z':[2.65, 35]},
	'queries': [
		'sum(a.b.c)',
		'min(z)',
		'max(a.x)',
		'z.s',
		'max(a.z)',
		'z',
		'a.b.c'
	]
}";
			//var json = "{\"data\":{\"empty\":[],\"x\":[0.1,0.2,0.3],\"a\":[{\"b\":10,\"c\":[1,2,3]},{\"b\":30,\"c\":[4]},{\"d\":500}]},\"queries\":[\"sum(empty)\",\"sum(a.b)\",\"sum(a.c)\",\"sum(a.d)\",\"sum(x)\"]}";
			var parsed = SimModel.TryParse(json, out SimModel data);
			if (!parsed)
				return;
			var exec = new SimQueryExecutor(data);

			foreach (var query in exec.ExecuteQueries())
				Console.WriteLine(query);
		}
	}
}
