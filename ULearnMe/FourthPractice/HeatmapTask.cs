using System;

namespace Names
{
    internal static class HeatmapTask
    {
        public static HeatmapData GetBirthsPerDateHeatmap(NameData[] names)
        {
            var days = NumberDays();

            var months = NumberMonhts();

            var birthsCounts = new double[30, 12];
            foreach (var name in names)
                if (name.BirthDate.Day != 1)
                {
                    birthsCounts[
                        name.BirthDate.Day - 2,
                        name.BirthDate.Month - 1]++;
                }

            return new HeatmapData(
                "Пример карты интенсивностей",
                birthsCounts,
                days,
                months);
        }

        private static string[] NumberMonhts()
        {
            var months = new string[12];
            for (int month = 1; month < 13; month++)
            {
                months[month - 1] = month.ToString();
            }

            return months;
        }

        private static string[] NumberDays()
        {
            var days = new string[30];

            for (int day = 2; day < 32; day++)
            {
                days[day - 2] = day.ToString();
            }

            return days;
        }
    }
}