using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace TableParser
{
    [TestFixture]
    public class QuotedFieldTaskTests
    {
        [TestCase("''", 0, "", 2)]
        [TestCase("'a'", 0, "a", 3)]
        public void Test(string line, int startIndex, string expectedValue, int expectedLength)
        {
            var actualToken = QuotedFieldTask.ReadQuotedField(line, startIndex);
            Assert.AreEqual(new Token(expectedValue, startIndex, expectedLength), actualToken);
        }

        // Добавьте свои тесты
    }

    class QuotedFieldTask
    {
        public static Token ReadQuotedField(string line, int startIndex)
        {
            var actualIndex = startIndex + 1;
            var actualValue = "";

            while (actualIndex < line.Length)
            {
                if (line[actualIndex] == line[startIndex])
                    break;
                if (line[actualIndex] == '\\')
                    actualIndex++;
                actualValue += line[actualIndex];
                actualIndex++;
            }
            if ((actualIndex >= line.Length))
            {
                actualIndex--;
            }

            return new Token(actualValue, startIndex, actualIndex - startIndex + 1);
        }
    }
}