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

namespace UiPlus.Elements
{
    public class UiListBox : UiElement
    {

        #region Members

        Wpf.ListBox ctrl = new Wpf.ListBox();

        #endregion

        #region Constructors

        public UiListBox() : base()
        {
            SetInputs();
        }

        public UiListBox(UiListBox uiControl) : base(uiControl)
        {
            this.border = uiControl.border;
        }

        #endregion

        #region Properties

        public virtual List<string> Items
        {
            set { ctrl.ItemsSource = value; }
        }

        public virtual int Index
        {
            get { return ctrl.SelectedIndex; }
            set { ctrl.SelectedIndex = value; }
        }

        #endregion

        #region Methods


        #endregion

        #region Overrides

        public override void SetInputs()
        {
            ElementType = ElementTypes.Border;

            ctrl.SelectionMode = Wpf.SelectionMode.Extended;

            this.control = this.ctrl;
            this.border.Child= this.control;
            base.SetInputs();
        }

        public override void SetPrimaryColors(Sd.Color color)
        {
            ctrl.Foreground = color.ToSolidColorBrush();
        }

        public override void Update(Gk.GH_Component component)
        {
            ctrl.SelectionChanged -= (o, e) => { component.ExpireSolution(true); };
            ctrl.SelectionChanged += (o, e) => { component.ExpireSolution(true); };
        }

        public override List<object> GetValues()
        {
            return new List<object> { this.Index };
        }

        public override string ToString()
        {
            return "Ui List Box | " + this.Name;
        }

        #endregion

    }
}