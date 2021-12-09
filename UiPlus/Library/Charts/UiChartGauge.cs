using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Rg = Rhino.Geometry;
using Gk = Grasshopper.Kernel;

using Sw = System.Windows;
using Wm = System.Windows.Media;
using Sd = System.Drawing;

using Wpf = System.Windows.Controls;
using Mat = MaterialDesignThemes.Wpf;
using Mah = MahApps.Metro.Controls;
using Xcd = Xceed.Wpf.Toolkit;

using Lch = LiveCharts.Wpf;

namespace UiPlus.Elements
{
    public class UiChartGauge : UiDataVis
    {

        #region Members

        Lch.Gauge gauge = new Lch.Gauge();

        #endregion

        #region Constructors

        public UiChartGauge() : base()
        {
            SetInputs();
        }

        public UiChartGauge(UiChartGauge uiControl) : base(uiControl)
        {
            this.control = uiControl.Control;
        }

        #endregion

        #region Properties

        public virtual double InnerRadius
        {
            get { return (double)gauge.InnerRadius; }
            set { gauge.InnerRadius = value; }
        }

        public virtual double Minimum
        {
            get { return (double)gauge.From; }
            set { gauge.From= value; }
        }

        public virtual double Maximum
        {
            get { return (double)gauge.To; }
            set { gauge.To = value; }
        }

        public virtual double Value
        {
            get { return (double)gauge.Value; }
            set { gauge.Value = value; }
        }

        public virtual bool IsCircular
        {
            get { return gauge.Uses360Mode; }
            set { gauge.Uses360Mode = value; }
        }

        public virtual Sd.Color StartColor
        {
            get { return gauge.FromColor.ToDrawingColor(); }
            set { gauge.FromColor = value.ToMediaColor(); }
        }

        public virtual Sd.Color EndColor
        {
            get { return gauge.ToColor.ToDrawingColor(); }
            set { gauge.ToColor = value.ToMediaColor(); }
        }

        #endregion

        #region Methods



        #endregion

        #region Overrides

        public override void SetInputs()
        {
            gauge.DisableAnimations = true;

            Wm.Brush brush = new Wm.SolidColorBrush(Wm.Colors.DarkGray);
            brush.Opacity = 0.25;

            gauge.HorizontalAlignment = Sw.HorizontalAlignment.Stretch;
            gauge.MinHeight = 100;
            gauge.MinWidth = 100;
            gauge.GaugeBackground = brush;

            this.control = gauge;
        }

        public override void Update(Gk.GH_Component component)
        {

        }

        public override List<object> GetValues()
        {
            return new List<object> { null };
        }

        public override string ToString()
        {
            return "Ui Gauge Chart | " + this.Name;
        }

        #endregion

    }
}