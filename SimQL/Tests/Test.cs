using NUnit.Framework;

namespace SimQLTask.Tests
{
	[TestFixture]
    public class Test
    {
        [Test]
        [TestCase("")]
        public void SearchTest(string fileName)
        {
        }
    }

	[TestFixture]
	public class SimModel_should
	{
		[Test]
		[TestCase("sum(a.x")]
		[TestCase("su(a)")]
		[TestCase("a.x)")]
		[TestCase("sum)a.x(")]
		[TestCase("sum(a.x)")]
		public void ThrownSmth_WhenInvalidQueries(string query)
		{

		}
	}

    public class JsonSearcher
    {
        public string[] Search()
        {
            return new string[0];
        }
    }
}
