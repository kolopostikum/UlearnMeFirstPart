using System;
using System.Collections.Generic;

namespace TextAnalysis
{
    static class SentencesParserTask
    {
        public static List<List<string>> ParseSentences(string text)
        {
            var sentences = text.Split(new char[]{'.','?','!', ':', ';', '(', ')', '"'},
                                                  StringSplitOptions.RemoveEmptyEntries);

            var words = new string[sentences.Length][];

            var punctuationMarks = new char[] {
                ' ',' ', ',', '/', '—', '“', '”',
                '‘', '…', '^', '#', '$', '-', '+',
                '1', '=', '\t', '\n', '\r', '\"',
                '2','3','4','5','6','7','8','9','0', '*'};

            for (int i = 0; i < sentences.Length; i++)
            {
                words[i] = sentences[i].Split(punctuationMarks,
                                              StringSplitOptions.RemoveEmptyEntries);
            }

            var sentencesList = MakeSentencesList(sentences, words);

            return sentencesList;
        }

		private static List<List<string>> MakeSentencesList(string[] sentences,
 															string[][] words)
        {
            var sentencesList = new List<List<string>>();
            var wordsList = new List<string>();

            for (int i = 0; i < sentences.Length; i++)
            {
                wordsList = new List<string>();

                for (int j = 0; j < words[i].Length; j++)
                {
                    words[i][j].Replace(" ", "");
                    wordsList.Add(words[i][j].ToLower());
                }
                if (wordsList.Count != 0)
                {
                    sentencesList.Add(wordsList);
                }
            }

            return sentencesList;
        }
    }
}