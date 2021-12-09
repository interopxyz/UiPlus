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
    public class UiChartBar: UiDataVis
    {

        #region Members

        public enum GraphTypes { Adjacent,Stacked, Stretch}

        protected GraphTypes graphType = GraphTypes.Adjacent;
        protected bool isHorizontal = false;

        #endregion

        #region Constructors

        public UiChartBar() : base()
        {
            SetInputs();
        }

        public UiChartBar(UiChartBar uiControl) : base(uiControl)
        {
            this.control = uiControl.Control;
        }

        #endregion

        #region Properties

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
                            default:
                                series = new Lch.RowSeries();
                                break;
                            case GraphTypes.Stacked:
                                Lch.StackedRowSeries stack = new Lch.StackedRowSeries();
                                stack.StackMode = LiveCharts.StackMode.Values;
                                series = stack;
                                break;
                            case GraphTypes.Stretch:
                                Lch.StackedRowSeries stretch = new Lch.StackedRowSeries();
                                stretch.StackMode = LiveCharts.StackMode.Percentage;
                                series = stretch;
                                break;
                        }
                    }
                    else
                    {
                        switch (graphType)
                        {
                            default:
                                series = new Lch.ColumnSeries();
                                break;
                            case GraphTypes.Stacked:
                                Lch.StackedColumnSeries stack = new Lch.StackedColumnSeries();
                                stack.StackMode = LiveCharts.StackMode.Values;
                                series = stack;
                                break;
                            case GraphTypes.Stretch:
                                Lch.StackedColumnSeries stretch = new Lch.StackedColumnSeries();
                                
                                stretch.StackMode = LiveCharts.StackMode.Percentage;
                                series = stretch;
                                break;
                        }
                    }
                    series.Title = dataSet.Name;

                    if (dataSet.HasPrimaryColor) series.Fill = dataSet.PrimaryColor.ToSolidColorBrush();
                    series.Stroke = dataSet.SecondaryColor.ToSolidColorBrush();
                    if (dataSet.HasWeight) series.StrokeThickness = dataSet.Weight;

                    series.DataLabels = dataSet.HasLabel;
                    if (dataSet.HasLabelColor) series.Foreground = dataSet.LabelColor.ToSolidColorBrush();

                    series.PointGeometry = Lch.DefaultGeometries.Circle;

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
            return "Ui Bar Chart | " + this.Name;
        }

        #endregion

    }
}