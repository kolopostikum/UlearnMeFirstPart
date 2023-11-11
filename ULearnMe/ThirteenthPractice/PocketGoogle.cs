using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketGoogle
{
    public class Indexer : IIndexer
    {
		public Dictionary<string, Dictionary<int, List<int>>> ActualDictionary
		{ get; set; }


        public void Add(int id, string documentText)
        {
            if (ActualDictionary == null)
                ActualDictionary = new Dictionary<string, Dictionary<int, List<int>>>();

            var words = documentText.Split(new char[]
            { ' ', '.', ',', '!', '?', ':', '-','\r','\n' });

            var position = 0;

            for (int i = 0; i < words.Length; i++)
            {
                AddWord(id, words[i], position);
                position += words[i].Length + 1;
            }
        }

        private void AddWord(int id, string word, int position)
        {
            if (ActualDictionary.ContainsKey(word))
                if (ActualDictionary[word].ContainsKey(id))
                    ActualDictionary[word][id].Add(position);
                else
                {
                    ActualDictionary[word].Add(id, new List<int>());
                    ActualDictionary[word][id].Add(position);
                }
            else
            {
                ActualDictionary.Add(word, new Dictionary<int, List<int>>());
                ActualDictionary[word].Add(id, new List<int>());
                ActualDictionary[word][id].Add(position);
            }
        }

        public List<int> GetIds(string word)
        {
            var ids = new List<int>();
            
            if (ActualDictionary.ContainsKey(word))
            {
                foreach (var document in ActualDictionary[word])
                {
                    ids.Add(document.Key);
                }
            }
            return ids;
        }

        public List<int> GetPositions(int id, string word)
        {
            var positions = new List<int>();

            if (ActualDictionary.ContainsKey(word))
            {
                if (ActualDictionary[word].ContainsKey(id))
                {
                    positions = ActualDictionary[word][id];
                    return positions;
                }
            }
            return positions;
        }

        public void Remove(int id)
        {
            foreach (var word in ActualDictionary)
            {
                if (word.Value.ContainsKey(id))
                {
                    word.Value.Remove(id);
                }
            }
        }
    }
}
