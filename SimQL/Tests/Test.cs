using NUnit.Framework;
using SimQLTask;

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

    public class JsonSearcher
    {
        public string[] Search()
        {
            return new string[0];
        }
    }
}
