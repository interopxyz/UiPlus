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
using Ldf = LiveCharts.Defaults;

namespace UiPlus.Elements
{
    public class UiChartScatter : UiDataVis
    {

        #region Members

        public enum Markers { Circle, Square, Diamond };
        protected Markers marker = Markers.Circle;

        #endregion

        #region Constructors

        public UiChartScatter() : base()
        {
            SetInputs();
        }

        public UiChartScatter(UiChartScatter uiControl) : base(uiControl)
        {
            this.control = uiControl.Control;
        }

        #endregion

        #region Properties

        public virtual Markers Marker
        {
            get { return marker; }
            set
            {
                marker = value;
                SetMarkers();
            }
        }

        #endregion

        #region Methods

        public void SetMarkers()
        {
            for (int i = 0; i < chart.Series.Count; i++)
            {
                switch (marker)
                {
                    default:
                        ((Lch.ScatterSeries)chart.Series[i]).PointGeometry = Lch.DefaultGeometries.Circle;
                        break;
                    case Markers.Square:
                        ((Lch.ScatterSeries)chart.Series[i]).PointGeometry = Lch.DefaultGeometries.Square;
                        break;
                    case Markers.Diamond:
                        ((Lch.ScatterSeries)chart.Series[i]).PointGeometry = Lch.DefaultGeometries.Diamond;
                        break;
                }
            }
        }

        protected override void SetData()
        {
            chart.Series.Clear();

            if (dataSets.Count > 0)
            {
                List<Lch.ScatterSeries> seriesSet = new List<Lch.ScatterSeries>();

                foreach (UiDataSet dataSet in dataSets)
                {
                    Lch.ScatterSeries series = new Lch.ScatterSeries();
                    series.MinPointShapeDiameter = 2;

                    List<Ldf.ScatterPoint> points = new List<Ldf.ScatterPoint>();
                    foreach (Rg.Point3d point in dataSet.PointItems)
                    {
                        points.Add(new Ldf.ScatterPoint(point.X * 10.0, point.Y * 10.0, point.Z * 10.0));
                    }

                    series.Title = dataSet.Name;

                    if (dataSet.HasPrimaryColor) series.Fill = dataSet.PrimaryColor.ToSolidColorBrush();
                    series.Stroke = dataSet.SecondaryColor.ToSolidColorBrush();
                    if (dataSet.HasWeight) series.StrokeThickness = dataSet.Weight;

                    series.DataLabels = dataSet.HasLabel;
                    if (dataSet.HasLabelColor) series.Foreground = dataSet.LabelColor.ToSolidColorBrush();

                    series.Values = new LiveCharts.ChartValues<Ldf.ScatterPoint>(points);

                    series.LabelPoint = val => dataSet.LabelPrefix + val.Y + dataSet.LabelSuffix;

                    //if (dataSet.HasPointGraphics) {
                    //    var doubleMapperWithMonthColors = new LiveCharts.Configurations.CartesianMapper<double>()
                    //        .X((value, index) => index)
                    //        .Y((value) => value);
                    //    if(dataSet.HasFillColors)doubleMapperWithMonthColors.Fill((v, i) => { return dataSet.FillColors[i].ToSolidColorBrush(); });
                    //    if (dataSet.HasStrokeColors) doubleMapperWithMonthColors.Stroke((v, i) => { return dataSet.StrokeColors[i].ToSolidColorBrush(); });
                    //    LiveCharts.Charting.For<double>(doubleMapperWithMonthColors, LiveCharts.SeriesOrientation.Horizontal);
                    //}

                    seriesSet.Add(series);

                }

                chart.Series.AddRange(seriesSet);

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

            chart.Update(false, true);

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
            return "Ui Scatter Chart | " + this.Name;
        }

        #endregion

    }
}