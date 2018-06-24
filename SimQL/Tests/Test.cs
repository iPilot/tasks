
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

    public class SimModel
    {
        public string Data { get; set; }
        public string[] Queris { get; set; }
    }

    public class JsonSearcher
    {
        public string[] Search()
        {
            return new string[0];
        }
    }
}
