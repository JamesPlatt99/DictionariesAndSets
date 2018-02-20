using DictionariesAndSets;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class NormalTests
    {
        private BloomFilter _bloomfilter;

        public BloomFilter BloomFilter {
            get
            {
                if (_bloomfilter == null)
                {
                    Program program = new Program();
                    _bloomfilter = program.GetBloomFilter();
                }
                return _bloomfilter;
            }
        }

        [TestMethod]
        public void SomeWords()
        {
            Program program = new Program();
            var bloomFilter = program.GetBloomFilter();
            Assert.IsTrue(BloomFilter.CheckWord("car"));
            Assert.IsTrue(BloomFilter.CheckWord("phone"));
            Assert.IsTrue(BloomFilter.CheckWord("agile"));
            Assert.IsTrue(BloomFilter.CheckWord("map"));
            Assert.IsTrue(BloomFilter.CheckWord("pneumonoultramicroscopicsilicovolcanoconiosis"));
        }

        [TestMethod]
        public void CaseInsensitivity()
        {            
            Assert.IsTrue(BloomFilter.CheckWord("cake"));
            Assert.IsTrue(BloomFilter.CheckWord("CAKE"));
            Assert.IsTrue(BloomFilter.CheckWord("CaKe"));

            Assert.IsTrue(BloomFilter.CheckWord("mouse"));
            Assert.IsTrue(BloomFilter.CheckWord("MOUSE"));
            Assert.IsTrue(BloomFilter.CheckWord("moUsE"));

            Assert.IsTrue(BloomFilter.CheckWord("carrot"));
            Assert.IsTrue(BloomFilter.CheckWord("CARROT"));
            Assert.IsTrue(BloomFilter.CheckWord("caRrOT"));
        }
    }
}
