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
    public class UiChartLine : UiDataVis
    {

        #region Members

        private bool isHorizontal = true;
        double smoothness = 0;

        #endregion

        #region Constructors

        public UiChartLine() : base()
        {
            SetInputs();
        }

        public UiChartLine(UiChartLine uiControl) : base(uiControl)
        {
            this.control = uiControl.Control;
            this.smoothness = uiControl.smoothness;
        }

        #endregion

        #region Properties

        public virtual double Smoothness
        {
            get { return smoothness; }
            set {
                smoothness = value;
                SetSmoothness();
            }
        }

        public virtual bool IsHorizontal
        {
            get { return this.isHorizontal; }
            set
            {
                this.isHorizontal = value;
                SetData();
            }
        }

        #endregion

        #region Methods

        private void SetSmoothness()
        {
            for (int i = 0; i < chart.Series.Count; i++)
            {
                ((Lch.LineSeries)chart.Series[i]).LineSmoothness = smoothness;
            }
        }

        protected override void SetData()
        {
            chart.Series.Clear();

            //List<Sd.Color> fillColors = new List<Sd.Color>();

            if (dataSets.Count > 0)
            {
                List<Lch.Series> seriesSet = new List<Lch.Series>();

                foreach (UiDataSet dataSet in dataSets)
                {
                    Lch.Series series = new Lch.LineSeries();

                    if (IsHorizontal)
                    {
                        Lch.LineSeries hSeries = new Lch.LineSeries();
                        hSeries.LineSmoothness = smoothness;
                        series = hSeries;
                    }
                    else
                    {
                        Lch.VerticalLineSeries vSeries = new Lch.VerticalLineSeries();
                        vSeries.LineSmoothness = smoothness;
                        series = vSeries;
                    }

                    series.Title = dataSet.Name;

                    series.PointGeometry = dataSet.GetMarkerGeometry();

                    if (dataSet.HasPrimaryColor) series.Stroke = dataSet.PrimaryColor.ToSolidColorBrush();
                    series.Fill = dataSet.SecondaryColor.ToSolidColorBrush();
                    if (dataSet.HasWeight) series.StrokeThickness = dataSet.Weight;

                    series.DataLabels = dataSet.HasLabel;
                    if (dataSet.HasLabelColor) series.Foreground = dataSet.LabelColor.ToSolidColorBrush();

                    //if (dataSet.HasFillColors) fillColors.AddRange(dataSet.FillColors);

                    series.Values = new LiveCharts.ChartValues<double>(dataSet.NumberItems.ToArray());

                    series.LabelPoint = val => dataSet.LabelPrefix + val.Y + dataSet.LabelSuffix;

                    seriesSet.Add(series);
                }

                chart.Series.AddRange(seriesSet);

                    //if (fillColors.Count>0) { 
                    //    var doubleMapperWithMonthColors = new LiveCharts.Configurations.CartesianMapper<double>()
                    //        .X((value, index) => index)
                    //        .Y((value) => value)
                    //        .Fill((v, i) => { return fillColors[i].ToSolidColorBrush(); })
                    //        .Stroke((v, i) => { return fillColors[i].ToSolidColorBrush(); });
                    //LiveCharts.Charting.For<double>(doubleMapperWithMonthColors, LiveCharts.SeriesOrientation.Horizontal);
                    //}

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
            return "Ui Line Chart | " + this.Name;
        }

        #endregion

    }
}