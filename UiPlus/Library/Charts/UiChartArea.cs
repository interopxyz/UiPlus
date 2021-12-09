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
    public class UiChartArea : UiDataVis
    {

        #region Members

        public enum GraphTypes { Stacked, Stretch }

        protected GraphTypes graphType = GraphTypes.Stacked;
        protected bool isHorizontal = false;

        double smoothness = 0;

        #endregion

        #region Constructors

        public UiChartArea() : base()
        {
            SetInputs();
        }

        public UiChartArea(UiChartArea uiControl) : base(uiControl)
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

        public virtual GraphTypes GraphType
        {
            get { return this.graphType; }
            set
            {
                this.graphType = value;
                SetData();
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
            for (int i = 0; i < ((Lch.CartesianChart)control).Series.Count; i++)
            {
                if (this.isHorizontal)
                {
                    ((Lch.StackedAreaSeries)chart.Series[i]).LineSmoothness = smoothness;
                }
                else
                {
                    ((Lch.VerticalStackedAreaSeries)chart.Series[i]).LineSmoothness = smoothness;
                }
            }
        }
        protected override void SetData()
        {
            chart.Series.Clear();

            if (dataSets.Count > 0)
            {
                List<Lch.Series> seriesSet = new List<Lch.Series>();

                foreach (UiDataSet dataSet in dataSets)
                {

                    Lch.Series series = null;

                    if (this.isHorizontal)
                    {
                        switch (graphType)
                        {
                            case GraphTypes.Stacked:
                                Lch.StackedAreaSeries stack = new Lch.StackedAreaSeries();
                                stack.StackMode = LiveCharts.StackMode.Values;
                                stack.LineSmoothness = smoothness;
                                series = stack;
                                break;
                            case GraphTypes.Stretch:
                                Lch.StackedAreaSeries stretch = new Lch.StackedAreaSeries();
                                stretch.StackMode = LiveCharts.StackMode.Percentage;
                                stretch.LineSmoothness = smoothness;
                                series = stretch;
                                break;
                        }
                    }
                    else
                    {
                        switch (graphType)
                        {
                            case GraphTypes.Stacked:
                                Lch.VerticalStackedAreaSeries stack = new Lch.VerticalStackedAreaSeries();
                                stack.StackMode = LiveCharts.StackMode.Values;
                                stack.LineSmoothness = smoothness;
                                series = stack;
                                break;
                            case GraphTypes.Stretch:
                                Lch.VerticalStackedAreaSeries stretch = new Lch.VerticalStackedAreaSeries();
                                stretch.StackMode = LiveCharts.StackMode.Percentage;
                                stretch.LineSmoothness = smoothness;
                                series = stretch;
                                break;
                        }
                    }

                    series.Title = dataSet.Name;

                    if (dataSet.HasPrimaryColor) series.Fill = dataSet.PrimaryColor.ToSolidColorBrush();
                    series.Stroke = dataSet.PrimaryColor.ToSolidColorBrush();
                    if (dataSet.HasWeight) series.StrokeThickness = dataSet.Weight;

                    series.DataLabels = dataSet.HasLabel;
                    if (dataSet.HasLabelColor) series.Foreground = dataSet.LabelColor.ToSolidColorBrush();

                    series.PointGeometry = dataSet.GetMarkerGeometry();
                    
                    series.Values = new LiveCharts.ChartValues<double>(dataSet.NumberItems.ToArray());

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
            return "Ui Area Chart | " + this.Name;
        }

        #endregion

    }
}