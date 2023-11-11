using System;
using System.Collections.Generic;

namespace TextAnalysis
{
    static class FrequencyAnalysisTask
    {
        public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
        {
            var result = new Dictionary<string, string>();
            var subResult = new Dictionary<string, string>();

            for (int i = 0; i < text.Count; i++)
            {
                subResult = GetDictonarySentence(text[i], subResult);
            }

            foreach (var bigram in subResult)
            {
                result.Add(bigram.Key, GetHighestOccuring(bigram.Value));
            }
            return result;
        }

        private static string GetHighestOccuring(string value)
        {
            var wordDictonary = new Dictionary<string, int>();
            string[] arrWords = value.Split(' ');

            foreach (string word in arrWords)
            {
                if (wordDictonary.ContainsKey(word))
                    wordDictonary[word] = wordDictonary[word] + 1;
                else
                    wordDictonary[word] = 1;
            }

            var highestOccuring = SearchHighestOccuring(wordDictonary);

            return highestOccuring;
        }

        private static string SearchHighestOccuring(Dictionary<string, int> wordDictonary)
        {
            var highestOccuring = " ";
            var maxOccuring = 0;

            foreach (var word in wordDictonary)
            {
                if (word.Value > maxOccuring)
                {
                    maxOccuring = word.Value;
                    highestOccuring = word.Key;
                }
                else
                {
                    if ((word.Value == maxOccuring)
                        && (String.CompareOrdinal(word.Key, highestOccuring) < 0))
                    {
                        highestOccuring = word.Key;
                    }
                }
            }

            return highestOccuring;
        }

        private static Dictionary<string, string> GetDictonarySentence(List<string> list,
                                                                       Dictionary<string, string> dictonarySentence)
        {
            dictonarySentence = GetBigramm(list, dictonarySentence);

            dictonarySentence = GetThreegramm(list, dictonarySentence);

            return dictonarySentence;
        }

        private static Dictionary<string, string> GetThreegramm(List<string> list,
            Dictionary<string, string> dictonarySentence)
        {
            for (int i = 0; i < list.Count - 2; i++)
            {
                if (dictonarySentence.ContainsKey(list[i] + " " + list[i + 1]))
                {
                    dictonarySentence[list[i] + " " + list[i + 1]] += " " + list[i + 2];
                }
                else
                {
                    dictonarySentence.Add(list[i] + " " + list[i + 1], list[i + 2]);
                }
            }

            return dictonarySentence;
        }

        private static Dictionary<string, string> GetBigramm(List<string> list,
            Dictionary<string, string> dictonarySentence)
        {
            for (int i = 0; i < list.Count - 1; i++)
            {
                if (dictonarySentence.ContainsKey(list[i]))
                {
                    dictonarySentence[list[i]] += " " + list[i + 1];
                }
                else
                {
                    dictonarySentence.Add(list[i], list[i + 1]);
                }
            }

            return dictonarySentence;
        }
    }
}