[TestCase("text", new[] {"text"})]
[TestCase("hello world", new[] {"hello", "world"})]
[TestCase("", new string[] {})]
[TestCase("    text", new[] {"text"})]
[TestCase(@"""a 'b' 'c' d""", new[] {"a 'b' 'c' d"})]
[TestCase(@"'""1"" ""2"" ""3""'", new[] {@"""1"" ""2"" ""3"""})]
[TestCase(@"'x y'", new[] {"x y"})]
[TestCase(@"a""b", new[] {"a", "b"})]
[TestCase(@"""a \""c\""""", new[] {@"a ""c"""})]
[TestCase(@"""\\""", new[] {@"\"})]
[TestCase(@"'", new[] {@""})]
[TestCase("hello    world", new[] {"hello", "world"})]
[TestCase(@"'\''", new[] {@"'"})]
[TestCase(@"'abc' a", new[] {@"abc", "a"})]
[TestCase(@"'abc ", new[] {@"abc "})]

// Вставляйте сюда свои тесты
public static void RunTests(string input, string[] expectedOutput)
{
    // Тело метода изменять не нужно
    Test(input, expectedOutput);
}