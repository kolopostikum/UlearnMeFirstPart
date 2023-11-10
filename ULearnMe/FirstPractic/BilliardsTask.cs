using System;

namespace Billiards
{
    public static class BilliardsTask
    {
        /// <summary>
        /// Метод для расчета угла отскока шарика от стены
        /// </summary>
        /// <param name="directionRadians">Угол направления движения шара</param>
        /// <param name="wallInclinationRadians">Угол</param>
        /// <returns>double угол в радианах</returns>
        public static double BounceWall(double directionRadians, double wallInclinationRadians)
        {
            double corner = 2 * wallInclinationRadians - directionRadians;
            return corner;
        }
    }
}