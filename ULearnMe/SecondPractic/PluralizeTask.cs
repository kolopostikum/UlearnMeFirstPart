namespace Pluralize
{
	public static class PluralizeTask
	{
        /// <summary>
        /// Метод, склоняющий существительное «рублей» следующее за указанным числительным
        /// </summary>
        /// <param name="count">Количество деняг</param>
        /// <returns>string существительное «рублей» в правильном склонении</returns>
		public static string PluralizeRubles(int count)
		{
			if (((count % 10) == 1) && ((count % 100) != 11)) 
				return "рубль";
			else if
				(
					((count % 10) > 1) 		
					&& ((count % 10) < 5)		
					&& ((count % 100 < 11) || (count % 100 > 14))	
				)
 				return "рубля";
			return "рублей";
		}
	}
}
