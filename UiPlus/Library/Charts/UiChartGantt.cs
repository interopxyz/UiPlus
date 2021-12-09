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
    public class UiChartGantt : UiDataVis
    {

        #region Members



        #endregion

        #region Constructors

        public UiChartGantt() : base()
        {
            SetInputs();
        }

        public UiChartGantt(UiChartGantt uiControl) : base(uiControl)
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
                List<Lch.Series> seriesSet = new List<Lch.Series>();

                int i = 0;
                foreach (UiDataSet dataSet in dataSets)
                {
                    Lch.RowSeries series = new Lch.RowSeries();
                    List<Ldf.GanttPoint> points = new List<Ldf.GanttPoint>();
                    int j = 0;
                    foreach (Rg.Interval domain in dataSet.DomainItems)
                    {
                        points.Add(new Ldf.GanttPoint(domain.Min, domain.Max));
                        j++;
                    }
                    i++;

                    series.Title = dataSet.Name;

                    if (dataSet.HasPrimaryColor) series.Fill = dataSet.PrimaryColor.ToSolidColorBrush();
                    series.Stroke = dataSet.SecondaryColor.ToSolidColorBrush();
                    if (dataSet.HasWeight) series.StrokeThickness = dataSet.Weight;

                    series.DataLabels = dataSet.HasLabel;
                    if (dataSet.HasLabelColor) series.Foreground = dataSet.LabelColor.ToSolidColorBrush();

                    series.PointGeometry = Lch.DefaultGeometries.Circle;

                    series.Values = new LiveCharts.ChartValues<Ldf.GanttPoint>(points);

                    series.LabelPoint = val => dataSet.LabelPrefix + val.Y + dataSet.LabelSuffix;

                    seriesSet.Add(series);
                }

                chart.Series.AddRange(seriesSet);
                chart.Update(false, true);

            }
        }

        #endregion

        #region Overrides

        public override void SetInputs()
        {

            chart.DisableAnimations = true;
            chart.Hoverable = false;
            chart.DataTooltip = null;

            chart.HorizontalAlignment = Sw.HorizontalAlignment.Stretch;
            chart.MinHeight = 300;

            SetData();

            this.control = chart;
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
            return "Ui Gantt Chart | " + this.Name;
        }

        #endregion

    }
}