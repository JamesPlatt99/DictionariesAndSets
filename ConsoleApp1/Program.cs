using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace DictionariesAndSets
{
    class Program
    {
        static void Main(string[] args)
        {
            Program program = new Program();
            program.Start();
        }

        private void Start()
        {
            var bloomFilter = GetBloomFilter();
            string curWord;
            Console.WriteLine("Word looker upper");
            Console.WriteLine("----------------------------------");
            for(int i = 0;i<100000;i++)
            {
                if (bloomFilter.IsCollision(curWord = RandomWord()))
                {
                    Console.WriteLine(curWord);
                }
                //Console.Write(" :");
                //Console.WriteLine(bloomFilter.CheckWord(Console.ReadLine()));
            }
            bloomFilter.WriteCollisions();
        }

        private String RandomWord()
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

        private BloomFilter GetBloomFilter()
        {
            BloomFilter bloomFilter = new BloomFilter();
            IEnumerable<String> words = GetWords();
            foreach(String word in words)
            {
                bloomFilter.AddWord(word);
            }
            return bloomFilter;
        }
        private IEnumerable<String> GetWords()
        {
            var client = new WebClient();
            return client.DownloadString("http://codekata.com/data/wordlist.txt").Split("\n");
        }
        
    }
}
