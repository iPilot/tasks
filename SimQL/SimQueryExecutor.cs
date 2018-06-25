using System;
using System.Collections.Generic;

namespace SimQLTask
{
	partial class SimQLProgram
	{
		public class SimQueryExecutor
		{
			public SimModel Data { get; }

			public SimQueryExecutor(SimModel dataModel)
			{
				Data = dataModel;
			}

			public IEnumerable<string> ExecuteQueries()
			{
				throw new NotImplementedException();
			}
		}
	}
}
