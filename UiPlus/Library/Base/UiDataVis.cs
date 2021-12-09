using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Gk = Grasshopper.Kernel;

using Sd = System.Drawing;
using Sw = System.Windows;

using Wpf = System.Windows.Controls;
using Mat = MaterialDesignThemes.Wpf;
using Mah = MahApps.Metro.Controls;
using Xcd = Xceed.Wpf.Toolkit;
using Swf = System.Windows.Forms.Integration;

using Lch = LiveCharts.Wpf;
using System.Drawing;

namespace UiPlus.Elements
{
    public class UiDataVis:UiElement
    {

        #region Members

        public enum Locations { None = 0, Top = 1, Bottom = 2, Left = 3, Right = 4 };
        public enum Tooltips { None = 0, Value = 1, ValuesX = 2, ValuesY = 3, SeriesX = 4, SeriesY = 5 };

        protected Lch.CartesianChart chart = new Lch.CartesianChart();

        protected List<UiDataSet> dataSets = new List<UiDataSet>();


        #endregion

        #region Constructors

        public UiDataVis(): base()
        {

        }

        public UiDataVis(UiDataVis dataVis) : base(dataVis)
        {

            this.dataSets = dataVis.dataSets;
        }

        #endregion

        #region Properties

        public virtual List<UiDataSet> DataSets
        {
            get { return this.dataSets; }
            set
            {
                this.dataSets.Clear();
                foreach (UiDataSet dataSet in value)
                {
                    this.dataSets.Add(new UiDataSet(dataSet));
                }

                SetData();
            }
        }

        #endregion

        #region Methods


        protected virtual void SetMarkers(UiDataSet dataSet)
        {
            for (int i = 0; i < chart.Series.Count; i++)
            {
                ((Lch.Series)chart.Series[i]).PointGeometry = dataSet.GetMarkerGeometry();
            }
        }

        public virtual void SetTooltip(Tooltips format, bool show = true)
        {
            if(format!= Tooltips.None)
            {
                Lch.DefaultTooltip tooltip = new Lch.DefaultTooltip();
                tooltip.SelectionMode = (LiveCharts.TooltipSelectionMode)format;
                tooltip.ShowSeries = show;
                tooltip.ShowTitle = show;

                chart.DataTooltip = tooltip;
            }
            else
            {
                chart.DataTooltip = null;
            }
        }

        public virtual void SetLegend(Locations location)
        {
            chart.LegendLocation = (LiveCharts.LegendLocation)location;
        }

        public virtual void SetAxisX(string label)
        {
            chart.AxisX[0].Title = label;
            chart.AxisX[0].FontSize = 12;
        }

        public virtual void SetAxisY(string label)
        {
            chart.AxisY[0].Title = label;
            chart.AxisY[0].FontSize = 12;
        }

        public virtual void SetLabelsX(List<string> labels)
        {
            chart.AxisX[0].Labels = labels;
            chart.AxisX[0].Separator.Step = chart.AxisY[0].MinValue + (1.0 / labels.Count) * (chart.AxisY[0].MaxValue - chart.AxisY[0].MinValue);
        }

        public virtual void SetLabelsY(List<string> labels)
        {
            chart.AxisY[0].Labels = labels;
            chart.AxisY[0].Separator.Step = chart.AxisY[0].MinValue + (1.0 / labels.Count) * (chart.AxisY[0].MaxValue - chart.AxisY[0].MinValue);
        }

        public override void SetPrimaryColors(Color color)
        {
            chart.Background = color.ToSolidColorBrush();
        }

        public override void SetAccentColors(Sd.Color color)
        {
            chart.AxisX[0].Foreground = color.ToSolidColorBrush();
            chart.AxisY[0].Foreground = color.ToSolidColorBrush();
            if (chart.ChartLegend != null) chart.ChartLegend.Foreground = color.ToSolidColorBrush();
        }

        public override void SetStrokeColor(Sd.Color color)
        {
            chart.AxisX[0].Separator.Stroke = color.ToSolidColorBrush();
            chart.AxisY[0].Separator.Stroke = color.ToSolidColorBrush();
        }

        public override void SetStrokeWidth(double width)
        {
            chart.AxisX[0].Separator.StrokeThickness = width;
            chart.AxisY[0].Separator.StrokeThickness = width;
        }


        #endregion

    }
}
