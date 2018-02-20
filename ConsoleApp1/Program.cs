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
                    String curWord;
                    for(int i = 0; i < 100; i++)
                    {
                        bloomFilter = GetBloomFilter(i);
                        for(int j = 0; j < 1000000; j++)
                        {
                            bloomFilter.CheckCollision(curWord = RandomWord());
                        }
                        Console.WriteLine("{0} - {1}", i, bloomFilter.Collisions.Count);
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

        private BloomFilter GetBloomFilter(int arrayStart = 1)
        {
            BloomFilter bloomFilter = new BloomFilter(arrayStart);
            IEnumerable<String> words = GetWords();
            foreach(String word in words)
            {
                bloomFilter.AddWord(word);
            }
            return bloomFilter;
        }                
    }
}
