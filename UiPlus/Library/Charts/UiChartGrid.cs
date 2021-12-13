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

using Syd = System.Data;
using Wpf = System.Windows.Controls;
using Mat = MaterialDesignThemes.Wpf;
using Mah = MahApps.Metro.Controls;
using Xcd = Xceed.Wpf.Toolkit;

using Lch = LiveCharts.Wpf;
using System.Drawing;

namespace UiPlus.Elements
{
    public class UiChartGrid : UiDataVis
    {

        #region Members

        Wpf.Grid grid = new Wpf.Grid();
        protected bool hasTitles = true;

        #endregion

        #region Constructors

        public UiChartGrid() : base()
        {
            SetInputs();
        }

        public UiChartGrid(UiChartGrid uiControl) : base(uiControl)
        {
            this.control = uiControl.Control;
            this.hasTitles = uiControl.hasTitles;
        }

        #endregion

        #region Properties

        public virtual bool HasTitles
        {
            get { return this.hasTitles; }
            set
            {
                this.hasTitles = value;
                SetData();
            }
        }

        #endregion

        #region Methods

        protected override void SetData()
        {
            grid.Children.Clear();
            grid.RowDefinitions.Clear();
            grid.ColumnDefinitions.Clear();

            if (dataSets.Count > 0)
            {
                int bump = 0;
                int step = 0;
                List<string> names = new List<string>();
                if (hasTitles) grid.RowDefinitions.Add(new Wpf.RowDefinition());
                if (hasTitles) bump = 1;
                foreach (UiDataSet dataSet in dataSets)
                {
                    Wpf.ColumnDefinition col = new Wpf.ColumnDefinition();
                    grid.ColumnDefinitions.Add(col);

                    string name = dataSet.Name;
                    if (names.Contains(name)) name += step;

                    if (hasTitles)
                    {
                        Wpf.Label title = new Wpf.Label();
                        title.Content = name;

                        title.HorizontalContentAlignment = Sw.HorizontalAlignment.Center;
                        title.FontWeight = Sw.FontWeights.Bold;

                        Wpf.Grid.SetColumn(title, step);
                        Wpf.Grid.SetRow(title, 0);
                        grid.Children.Add(title);
                    }
                    names.Add(name);
                    step++;
                }

                for (int i = 0; i < dataSets[0].Total; i++)
                {
                    grid.RowDefinitions.Add(new Wpf.RowDefinition());
                    for (int j = 0; j < dataSets.Count; j++)
                    {
                        Wpf.Label label = new Wpf.Label();
                        label.Content = dataSets[j].TextItems[i];

                        if (dataSets[j].HasFillColors) label.Background = dataSets[j].FillColors[i].ToSolidColorBrush();
                        if (dataSets[j].HasStrokeColors) label.BorderBrush = dataSets[j].StrokeColors[i].ToSolidColorBrush();
                        if (dataSets[j].HasStrokeWeights) label.BorderThickness = new Sw.Thickness( dataSets[j].StrokeWeights[i]);
                        if (dataSets[j].HasTextColors) label.Foreground = dataSets[j].TextColors[i].ToSolidColorBrush();

                        label.HorizontalContentAlignment = Sw.HorizontalAlignment.Center;

                        Wpf.Grid.SetRow(label, i + bump);
                        Wpf.Grid.SetColumn(label, j);
                        grid.Children.Add(label);
                    }
                }
            }
        }

        #endregion

        #region Overrides

        public override void SetInputs()
        {
            this.ElementType = ElementTypes.Layout;

            this.layout = grid;
        }

        //public override void SetForeFill(Color color)
        //{
        //    foreach (Wpf.Control ctrl in grid.Children)
        //    {
        //        ((Wpf.Label)ctrl).Foreground = color.ToSolidColorBrush();
        //    }
        //}

        //public override void SetStrokeColor(Color color)
        //{
        //    foreach(Wpf.Control ctrl in grid.Children)
        //    {
        //        ((Wpf.Label)ctrl).BorderBrush = color.ToSolidColorBrush();
        //    }
        //}

        //public override void SetStrokeWidth(double width)
        //{
        //    foreach (Wpf.Control ctrl in grid.Children)
        //    {
        //        ((Wpf.Label)ctrl).BorderThickness = new Sw.Thickness(width);
        //    }
        //}

        public override void Update(Gk.GH_Component component)
        {

        }

        public override List<object> GetValues()
        {
            return new List<object> { null };
        }

        public override string ToString()
        {
            return "Ui Data Grid | " + this.Name;
        }

        #endregion

    }
}