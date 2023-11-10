using System;
namespace FirstPractic;
public static class Calculater
{
    /// <summary>
    /// Метод для расчета сумму на момент окончания вклада.
    /// </summary>        
    /// <param name="userInput">
    /// Три числа через пробел: исходная сумма,
    /// процентная ставка (в процентах) и срок вклада в месяцах
    /// </param>
    /// <returns>double сумма на момент окончания вклада</returns>
    public static double Calculate(string userInput)
    {
	    var userData = userInput.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
        var initialAmount = double.Parse(userData[0], System.Globalization.CultureInfo.InvariantCulture);
        var interestRate = double.Parse(userData[1]);
        var depositTerm = double.Parse(userData[2]);
        var interestMonth = (double)interestRate / (100 * 12);
        double summ = initialAmount * Math.Pow((1 + (interestMonth)),depositTerm);
	    return summ;
    }
}