namespace SimQLTask
{
	public class SimQuery
	{
		public string[] Path { get; }
		public SimQueryFunc Func { get; }

		public SimQuery(string path, SimQueryFunc func = SimQueryFunc.NoFunc)
		{
			Path = path.Split('.');
			Func = func;
		}

		public override string ToString()
		{
			var path = string.Join(".", Path);
			return Func == SimQueryFunc.NoFunc
				? path
				: $"{Func.ToString().ToLower()}({path})";
		}
	}
}
