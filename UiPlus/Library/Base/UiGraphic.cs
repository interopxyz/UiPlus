using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiPlus
{
    public abstract class UiGraphic
    {
        protected Color fillColor = Color.Empty;
        protected Color strokeColor = Color.Empty;
        protected double strokeWeight = double.NaN;

        public UiGraphic()
        {

        }

        public UiGraphic(UiGraphic uiGraphic)
        {
            this.fillColor = uiGraphic.fillColor;
            this.strokeColor = uiGraphic.strokeColor;
            this.strokeWeight = uiGraphic.strokeWeight;
        }

    }
}
