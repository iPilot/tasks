namespace SimQLTask
{
	public class SimQuery
	{
		public string[] Path { get; }
		public string Func { get; }

		public SimQuery(string[] path, string func = null)
		{
			Path = path;
			Func = func;
		}

		public bool IsFuncQuery => Func != null;
	}
}
