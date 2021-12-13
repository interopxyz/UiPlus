using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Rhino.Geometry;

using Sd = System.Drawing;
using Sw = System.Windows;
using Sm = System.Windows.Media;

using Wpf = System.Windows.Controls;
using Mat = MaterialDesignThemes.Wpf;
using Mah = MahApps.Metro.Controls;
using Xcd = Xceed.Wpf.Toolkit;

namespace UiPlus.Elements
{
    public class UiLayoutGrid : UiElement
    {

        #region Members

        protected List<UiElement> elements = new List<UiElement>();

        protected List<int> rows = new List<int>();
        protected List<int> columns = new List<int>();

        #endregion

        #region Constructors

        public UiLayoutGrid() : base()
        {
            SetInputs();
        }

        public UiLayoutGrid(UiLayoutGrid uiControl) : base(uiControl)
        {
            this.control = uiControl.Control;
        }

        #endregion

        #region Properties

        public virtual Dictionary<Tuple<int, int>, UiElement> LocationElements
        {
            get 
            {
                Dictionary<Tuple<int, int>, UiElement> locations = new Dictionary<Tuple<int, int>, UiElement>();
                for (int i = 0; i < elements.Count; i++)
                {
                    locations.Add(new Tuple<int, int>(columns[i], rows[i]), elements[i]);
                }
                return locations;
            }
            set 
            {
                foreach(KeyValuePair<Tuple<int, int>, UiElement> location in value)
                {
                    columns.Add(location.Key.Item1);
                    rows.Add(location.Key.Item2);
                    elements.Add(location.Value);
                }
                SetInputs();
            }
        }

        #endregion

        #region Methods


        #endregion

        #region Overrides

        public override void SetInputs()
        {
            this.ElementType = ElementTypes.Layout;

            Wpf.Grid ctrl = new Wpf.Grid();

            if (elements.Count > 0)
            {

            for(int i = 0; i < columns.Max()+1;i++)
            {
                Wpf.ColumnDefinition column = new Wpf.ColumnDefinition();
                column.Width = new Sw.GridLength(100, Sw.GridUnitType.Star);
                ctrl.ColumnDefinitions.Add(column);
            }

            for (int i = 0; i < rows.Max()+1; i++)
            {
                Wpf.RowDefinition row = new Wpf.RowDefinition();
                row.Height = new Sw.GridLength(100, Sw.GridUnitType.Star);
                ctrl.RowDefinitions.Add(row);
            }


            for (int i = 0; i < elements.Count; i++)
            {
                elements[i].DetachParent();
                elements[i].SetElement();
                ctrl.Children.Add(elements[i].Container);
                Wpf.Grid.SetColumn(elements[i].Container, columns[i]);
                Wpf.Grid.SetRow(elements[i].Container, rows[i]);
                }
            }

            this.layout = ctrl;

        }

        public override List<object> GetValues()
        {
            return new List<object> { null };
        }

        public override string ToString()
        {
            return "Ui Layout Grid | " + this.Name;
        }

        #endregion

    }
}