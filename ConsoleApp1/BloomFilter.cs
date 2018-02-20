using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace DictionariesAndSets
{
    public class BloomFilter
    {
        private const int ARRAYSIZE = 21474836;

        private int _startPrimeIndex = 0;
        private Dictionary<int, String> _collisionLookup = new Dictionary<int, String>();
        private bool[] _lookup = new bool[ARRAYSIZE];
        private Dictionary<String, String> _collisionsFound = new Dictionary<String, String>();
        private int[] _primes;
        private int[] Primes
        {
            get
            {
                if (_primes == null)
                {
                    StreamReader reader = new StreamReader(@"C:\LocalProjects\Training\AptemProjects\DictionariesAndSets\ConsoleApp1\lovelyListOfPrimes.txt");
                    string[] primesStr = reader.ReadToEnd().Split(",");
                    reader.Close();
                    _primes = new int[primesStr.Length];
                    for (int i = 0; i < primesStr.Length; i++)
                    {
                        _primes[i] = Convert.ToInt32(primesStr[i]);
                    }
                }
                return _primes;
            }
        }

        public BloomFilter(int startPrimeIndex)
        {
            _startPrimeIndex = startPrimeIndex;
        }

        public List<String> Collisions = new List<string>();
        public void AddWord(String word, bool checkCollisions = true)
        {
            int hash = GetHash(word);
            _lookup[hash] = true;

            if (!checkCollisions)
            {
                return;
            }

            if (!_collisionLookup.ContainsKey(hash))
            {
                _collisionLookup.Add(hash, word);
            }
        }
        public bool CheckCollision(String word)
        {
            int hash = GetHash(word);
            if (_lookup[hash])
            {
                if (_collisionLookup.ContainsKey(hash) && _collisionLookup[hash] != word)
                {
                    Collisions.Add(word);
                    return true;
                }
            }
            return false;
        }

        public void WriteCollisions()
        {
            StreamWriter writer = new StreamWriter("Collisions!!!.txt", false);
            foreach (string word in Collisions)
            {
                writer.WriteLine(word);
            }
            writer.Close();
        }

        public bool CheckWord(String word)
        {
            int hash = GetHash(word);
            return _lookup[hash] == true;
        }
        private int GetHash(String input)
        {
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input.ToLower());
            int output = 1;
            for (int i = 0; i < inputBytes.Length; i++)
            {
                output = output * (inputBytes[i] * Primes[(Primes[i] * inputBytes[i]) % Primes.Length]);
            }
            //Return magnitude of value
            return (int)Math.Abs(output % ARRAYSIZE);            
        }
    }
}
