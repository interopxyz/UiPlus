using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sd = System.Drawing;
using Wm = System.Windows.Media;

namespace UiPlus
{
    public static class ColorConversions
    {

        public static Sd.Color ToDrawingColor(this Wm.Color input)
        {
            return Sd.Color.FromArgb(input.A, input.R, input.G, input.B);
        }

        public static Wm.Color ToMediaColor(this Sd.Color input)
        {
            return Wm.Color.FromArgb(input.A, input.R, input.G, input.B);
        }

    }
}
