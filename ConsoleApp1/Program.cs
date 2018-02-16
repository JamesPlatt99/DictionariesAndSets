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
            BloomFilter bloomFilter = GetBloomFilter();
            Menu(bloomFilter);
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
                    while (true)
                    {
                        Console.WriteLine(bloomFilter.CheckWord(RandomWord ()));
                    }
                default:
                    Menu(bloomFilter);
                    break;
            }
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
