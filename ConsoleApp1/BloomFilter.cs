using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace DictionariesAndSets
{
    class BloomFilter
    {
        //private Dictionary<String, String> _lookup = new Dictionary<String, String>();
        private const int _arSize = 21474836;
        private bool[] _lookup = new bool[_arSize];
        private Dictionary<String, String> _collisionsFound = new Dictionary<String, String>();
        private string _primesStr = "2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97, 101, 103, 107, 109, 113, 127, 131, 137, 139, 149, 151, 157, 163, 167, 173, 179, 181, 191, 193, 197, 199, 211, 223, 227, 229, 233, 239, 241, 251, 257, 263, 269, 271, 277, 281, 283, 293, 307, 311, 313, 317, 331, 337, 347, 349, 353, 359, 367, 373, 379, 383, 389, 397, 401, 409, 419, 421, 431, 433, 439, 443, 449, 457, 461, 463, 467, 479, 487, 491, 499, 503, 509, 521, 523, 541, 547, 557, 563, 569, 571, 577, 587, 593, 599, 601, 607, 613, 617, 619, 631, 641, 643, 647, 653, 659, 661, 673, 677, 683, 691, 701, 709, 719, 727, 733, 739, 743, 751, 757, 761, 769, 773, 787, 797, 809, 811, 821, 823, 827, 829, 839, 853, 857, 859, 863, 877, 881, 883, 887, 907, 911, 919, 929, 937, 941, 947, 953, 967, 971, 977, 983, 991, 997";
        private int[] _primes;
        private int[] Primes
        {
            get
            {
                if(_primes == null)
                {
                    string[] primesStr = _primesStr.Split(",");
                    _primes = new int[128];
                    for(int i = 0; i <128; i++)
                    {
                        _primes[i] = Convert.ToInt32(primesStr[i]);
                    }
                }
                return _primes;
            }
        }
        public void AddWord(String word)
        {
            uint hash = GetHash(word);
            _lookup[hash] = true;
        }        
        public bool CheckWord(String word)
        {
            uint hash = GetHash(word);
            return _lookup[hash] == true;
        }
        private uint GetHash(String input)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input.ToLower());
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                uint output = 0;
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    output += (uint) (hashBytes[i] * Primes[i]);
                }
                return output % _arSize;
            }
        }
    }
}
