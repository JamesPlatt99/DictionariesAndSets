using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Linq;

namespace DictionariesAndSets
{
    public class Program
    {
        static void Main(string[] args)
        {
            Program program = new Program();
            program.Start();
        }

        #region Public Methods
        public String RandomWord()
        {
            Random random = new Random();
            StringBuilder sb = new StringBuilder();
            int length = random.Next(1, 12);
            for (int i = 0; i < length; i++)
            {
                sb.Append((char)(random.Next(65, 91)));
            }
            return sb.ToString();
        }

        public BloomFilter GetBloomFilter(int arrayStart = 1)
        {
            BloomFilter bloomFilter = new BloomFilter(arrayStart);
            IEnumerable<String> words = GetWords();
            foreach(String word in words)
            {
                bloomFilter.AddWord(word);
            }
            return bloomFilter;
        }                

        public void Start()
        {
            BloomFilter bloomFilter = GetBloomFilter();
            Menu(bloomFilter);
        }
        #endregion

        #region Helper Methods
        private IEnumerable<String> _words;
        private IEnumerable<String> GetWords()
        {
            if(_words == null)
            {
                var client = new WebClient();
                return client.DownloadString("http://codekata.com/data/wordlist.txt").Split("\n");
            }
            return _words;
        }

        private void Menu(BloomFilter bloomFilter)
        {
            Console.WriteLine("Word looker upper");
            Console.WriteLine("----------------------------------");
            Console.WriteLine("  1. User entered words");
            Console.WriteLine("  2. Generated words");
            switch (Console.ReadLine())
            {
                case "1":
                    while (true)
                    {
                        Console.Write(" :");
                        Console.WriteLine(bloomFilter.CheckWord(Console.ReadLine()));
                    }
                case "2":
                    bloomFilter = GetBloomFilter();
                    for (int i = 0; i < 100; i++)
                    {
                        bloomFilter.StartPrimeIndex = i;
                        bloomFilter.Collisions = new List<string>();
                        for (int j = 0; j < 1000000; j++)
                        {
                            bloomFilter.CheckCollision(RandomWord());
                        }
                        Console.WriteLine("{0} - {1}", i, bloomFilter.Collisions.Distinct().Count());
                    }
                    bloomFilter.WriteCollisions();
                    break;
                default:
                    Menu(bloomFilter);
                    break;
            }
            Console.WriteLine(bloomFilter.Collisions.Count);
            Console.ReadLine();
        }
        #endregion
    }
}