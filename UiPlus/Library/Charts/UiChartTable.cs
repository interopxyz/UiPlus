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

namespace UiPlus.Elements
{
    public class UiChartTable : UiDataVis
    {

        #region Members

        Wpf.DataGrid dataGrid = new Wpf.DataGrid();

        #endregion

        #region Constructors

        public UiChartTable() : base()
        {
            SetInputs();
        }

        public UiChartTable(UiChartTable uiControl) : base(uiControl)
        {
            this.control = uiControl.Control;
        }

        #endregion

        #region Properties

        #endregion

        #region Methods

        protected override void SetData()
        {
            dataGrid.Columns.Clear();

            if (dataSets.Count > 0)
            {
                Syd.DataTable Table = new Syd.DataTable();
                Syd.DataSet DS = new Syd.DataSet();
                int step = 0;
                List<string> names = new List<string>();
                foreach (UiDataSet dataSet in dataSets)
                {
                    string name = dataSet.Name;
                    if (Table.Columns.Contains(name)) name += step;
                    Syd.DataColumn col = new Syd.DataColumn(name, typeof(string));
                    Table.Columns.Add(col);
                    names.Add(name);
                    step++;
                }

                for (int i = 0; i < dataSets[0].Total; i++)
                {
                    Syd.DataRow row = Table.NewRow();
                    Table.Rows.Add(row);
                    for (int j = 0; j < dataSets.Count; j++)
                    {
                        row[names[j]] = dataSets[j].TextItems[i];
                    }
                }
                
                DS.Tables.Add(Table);

                dataGrid.DataContext = DS.Tables[0].DefaultView;
                dataGrid.ItemsSource = DS.Tables[0].DefaultView;

                dataGrid.AutoGenerateColumns = true;
            }


        }

        #endregion

        #region Overrides

        public override void SetInputs()
        {

            dataGrid.HorizontalAlignment = Sw.HorizontalAlignment.Stretch;
            dataGrid.HorizontalScrollBarVisibility = Wpf.ScrollBarVisibility.Auto;
            dataGrid.SelectionMode = Wpf.DataGridSelectionMode.Extended;
            dataGrid.SelectionUnit = Wpf.DataGridSelectionUnit.Cell;

            dataGrid.CanUserAddRows = false;
            dataGrid.CanUserDeleteRows = false;

            dataGrid.GridLinesVisibility = Wpf.DataGridGridLinesVisibility.Horizontal;
            dataGrid.AutoGenerateColumns = true;

            SetData();

            this.control = dataGrid;
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
            return "Ui Data Table | " + this.Name;
        }

        #endregion

    }
}