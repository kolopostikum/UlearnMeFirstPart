using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Autocomplete
{
    internal class AutocompleteTask
    {
        /// <returns>
        /// Возвращает первую фразу словаря, начинающуюся с prefix.
        /// </returns>
        /// <remarks>
        /// Эта функция уже реализована, она заработает, 
        /// как только вы выполните задачу в файле LeftBorderTask
        /// </remarks>
        public static string FindFirstByPrefix(IReadOnlyList<string> phrases, string prefix)
        {
            var index = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) + 1;
            if (index < phrases.Count && phrases[index].StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                return phrases[index];
            
            return null;
        }

        /// <returns>
        /// Возвращает первые в лексикографическом порядке count (или меньше, если их меньше count) 
        /// элементов словаря, начинающихся с prefix.
        /// </returns>
        /// <remarks>Эта функция должна работать за O(log(n) + count)</remarks>
        public static string[] GetTopByPrefix(IReadOnlyList<string> phrases, string prefix, int count)
        {
            var index = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) + 1;
            var results = new string[count];
            var resultLength = 0;

            for (int i = 0; i < count; i++)
            {
                if (index + i < phrases.Count)
                    if (phrases[index + i].StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                    { 
                        results[i] = phrases[index + i];
                        resultLength++;
                    }
                    else break;
            }

            var subResults = new string[resultLength];

            for (int i = 0; i < resultLength; i++)
            {
                subResults[i] = results[i];
            }

            return subResults;
        }

        /// <returns>
        /// Возвращает количество фраз, начинающихся с заданного префикса
        /// </returns>
        public static int GetCountByPrefix(IReadOnlyList<string> phrases, string prefix)
        {
            if (RightBorderTask.GetRightBorderIndex(phrases, prefix, -1, phrases.Count)
                != LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) + 1)

                return RightBorderTask.GetRightBorderIndex(phrases, prefix, -1, phrases.Count)
                        - LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) - 1;
            else return 0;
        }
    }

    [TestFixture]
    public class AutocompleteTests
    {
        [Test]
        public void TopByPrefix_IsEmpty_WhenNoPhrases()
        {
            //var actualTopWords = Autocomplete.AutocompleteTask.GetTopByPrefix(new string[]{}, "", 0);
            //CollectionAssert.IsEmpty(actualTopWords);
        }

        //[TestCase(new string[] { "a", "ab" }, new string[] { "a", "ab", "ab", "bc" }, "a", 2)]

        [TestCase(new string[] { "a", "ab" }, new string[] { "a", "ab"}, "a", 2)]


        public void Test1(string[] expectedResult, IReadOnlyList<string> phrases, string prefix, int count)
        {
            var actualResult = Autocomplete.AutocompleteTask.GetTopByPrefix(
                                            phrases, prefix, count);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void CountByPrefix_IsTotalCount_WhenEmptyPrefix()
        {
            // ...
            //Assert.AreEqual(expectedCount, actualCount);
        }

        // ...
    }
}
