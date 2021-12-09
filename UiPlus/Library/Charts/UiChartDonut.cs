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
using System.Drawing;

namespace UiPlus.Elements
{
    public class UiChartDonut : UiDataVis
    {

        #region Members

        Lch.PieChart donut = new Lch.PieChart();

        #endregion

        #region Constructors

        public UiChartDonut() : base()
        {
            SetInputs();
        }

        public UiChartDonut(UiChartDonut uiControl) : base(uiControl)
        {
            this.control = uiControl.Control;
        }

        #endregion

        #region Properties

        public virtual double InnerRadius
        {
            get { return donut.InnerRadius; }
            set { donut.InnerRadius = value; }
        }

        #endregion

        #region Methods

        protected override void SetData()
        {
            donut.Series.Clear();

            int total = 0;
            if (dataSets.Count > 0)
            {
                //find the longest list of data
                foreach (UiDataSet dataSet in dataSets)
                {
                    if (total < dataSet.Total) total = dataSet.Total;
                }

                List<Lch.PieSeries> seriesSet = new List<Lch.PieSeries>();
                foreach (UiDataSet dataSet in dataSets)
                {
                    Lch.PieSeries series = new Lch.PieSeries();

                    series.Title = dataSet.Name;

                    if (dataSet.HasPrimaryColor) series.Fill = dataSet.PrimaryColor.ToSolidColorBrush();
                    series.Stroke = dataSet.SecondaryColor.ToSolidColorBrush();
                    if (dataSet.HasWeight) series.StrokeThickness = dataSet.Weight;

                    series.DataLabels = dataSet.HasLabel;
                    if (dataSet.HasLabelColor) series.Foreground = dataSet.LabelColor.ToSolidColorBrush();

                    series.PointGeometry = Lch.DefaultGeometries.Circle;

                    series.Values = new LiveCharts.ChartValues<double>(dataSet.NumberItems.ToArray());
                    
                    seriesSet.Add(series);

                    series.LabelPoint = val => dataSet.LabelPrefix + val.Y + dataSet.LabelSuffix;
                }

                donut.Series.AddRange(seriesSet);
                donut.Update(false, true);

            }
        }

        #endregion

        #region Overrides

        public override void SetInputs()
        {

            donut.DisableAnimations = true;
            donut.Hoverable = false;
            donut.DataTooltip = null;

            donut.InnerRadius = 50;
            donut.HorizontalAlignment = Sw.HorizontalAlignment.Stretch;
            donut.MinHeight = 300;

            SetData();

            this.control = donut;
        }

        public override void SetTooltip(Tooltips format, bool show = true)
        {
            if (format != Tooltips.None)
            {
                Lch.DefaultTooltip tooltip = new Lch.DefaultTooltip();
                tooltip.SelectionMode = (LiveCharts.TooltipSelectionMode)format;
                tooltip.ShowSeries = show;
                tooltip.ShowTitle = show;

                donut.DataTooltip = tooltip;
            }
            else
            {
                donut.DataTooltip = null;
            }
        }

        public override void SetLegend(Locations location)
        {
            donut.LegendLocation = (LiveCharts.LegendLocation)location;
        }

        public override void SetAxisX(string label)
        {
        }

        public override void SetAxisY(string label)
        {
        }

        public override void SetLabelsX(List<string> labels)
        {
        }

        public override void SetLabelsY(List<string> labels)
        {
        }

        public override void SetPrimaryColors(Color color)
        {
            if (donut.ChartLegend != null) donut.ChartLegend.Foreground = color.ToSolidColorBrush();
        }

        public override void SetStrokeColor(Color color)
        {
        }

        public override void SetStrokeWidth(double width)
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
            return "Ui Donut Chart | " + this.Name;
        }

        #endregion

    }
}