using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometryTasks;
using System.Drawing;

namespace GeometryPainting
{
    public static class SegmentExtensions
    {
        public static Dictionary<Segment, Color> SegmentColors = new Dictionary<Segment, Color>();

        public static void SetColor(this Segment segment, Color color)
        {
            if (!SegmentColors.ContainsKey(segment))
                SegmentColors.Add(segment, color);
            else
                SegmentColors[segment] = color;
        }

        public static Color GetColor(this Segment segment)
        {
            if (SegmentColors.ContainsKey(segment))
                return SegmentColors[segment];
            else
                return Color.Black;
        }
    }
}