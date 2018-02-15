using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace DictionariesAndSets
{
    class BloomFilter
    {
        private Dictionary<String, String> _lookup = new Dictionary<String, String>();
        private Dictionary<String, String> _collisionsFound = new Dictionary<String, String>();
        public void AddWord(String word)
        {
            String hash = GetHash(word);
            if (!_lookup.ContainsKey(hash))
            {
                _lookup.Add(hash, word);
            }
        }        
        public bool CheckWord(String word)
        {
            String hash = GetHash(word);
            return _lookup.ContainsKey(hash);
        }
        public bool IsCollision(String word)
        {
            String hash = GetHash(word);
            if(_lookup.ContainsKey(hash) && _lookup[hash].ToLower() != word.ToLower())
            {
                if (!_collisionsFound.ContainsKey(word))
                {
                    _collisionsFound.Add(word, _lookup[hash]);
                }
                return true;
            }
            return false;
        }
        public void WriteCollisions()
        {
            StreamWriter writer = new StreamWriter("Collisions!!!.txt");
            foreach(string key in _collisionsFound.Keys)
            {
                writer.WriteLine("{0} - {1}",key, _collisionsFound[key]);
            }
            writer.Close();
        }
        private String GetHash(String input)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input.ToLower());
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}
