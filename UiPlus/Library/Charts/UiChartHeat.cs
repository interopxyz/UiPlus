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

using Ldf = LiveCharts.Defaults;
using Lch = LiveCharts.Wpf;

namespace UiPlus.Elements
{
    public class UiChartHeat : UiDataVis
    {

        #region Members

        public List<Sd.Color> Colors = new List<Sd.Color>();

        #endregion

        #region Constructors

        public UiChartHeat() : base()
        {
            SetInputs();
        }

        public UiChartHeat(UiChartHeat uiControl) : base(uiControl)
        {
            this.control = uiControl.Control;
        }

        #endregion

        #region Properties

        #endregion

        #region Methods

        protected override void SetData()
        {
            chart.Series.Clear();

            if (dataSets.Count > 0)
            {
                Lch.HeatSeries series = new Lch.HeatSeries();
                List<Ldf.HeatPoint> points = new List<Ldf.HeatPoint>();

                int i = 0;
                foreach (UiDataSet dataSet in dataSets)
                {
                    int j = 0;
                    foreach (double number in dataSet.NumberItems)
                    {
                        points.Add(new Ldf.HeatPoint(i, j, number));
                        j++;
                    }
                    i++;
                }
                if (Colors.Count > 1)
                {
                    List<Wm.GradientStop> stops = new List<Wm.GradientStop>();
                    for(int c =0;c< Colors.Count;c++)
                    {
                        stops.Add(new Wm.GradientStop(Colors[c].ToMediaColor(),1.0/Colors.Count*c));
                    }
                    series.Stroke = dataSets[0].SecondaryColor.ToSolidColorBrush();
                    series.StrokeThickness = dataSets[0].Weight;
                    series.GradientStopCollection = new Wm.GradientStopCollection(stops);
                }

                series.Values = new LiveCharts.ChartValues<Ldf.HeatPoint>(points);
                chart.Series.Add(series);

                chart.Update(false, true);

            }
        }

        public void SetColors(List<Sd.Color> colors)
        {
            
        }

        #endregion

        #region Overrides

        public override void SetInputs()
        {
            chart.Series.Clear();

            chart.DisableAnimations = true;

            chart.HorizontalAlignment = Sw.HorizontalAlignment.Stretch;
            chart.MinHeight = 300;

            SetData();

            this.control = chart;
        }

        public override void SetLegend(Locations location)
        {

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
            return "Ui Heat Chart | " + this.Name;
        }

        #endregion

    }
}