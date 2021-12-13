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
    public class UiDropdownList : UiElement
    {

        #region Members

        Wpf.ComboBox ctrl = new Wpf.ComboBox();

        #endregion

        #region Constructors

        public UiDropdownList() : base()
        {
            SetInputs();
        }

        public UiDropdownList(UiDropdownList uiControl) : base(uiControl)
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

            this.ctrl.MinWidth = 80;

            this.control = this.ctrl;
            this.border.Child = this.control;
            base.SetInputs();
            border.Padding = new Sw.Thickness(Constants.DefaultPadding()+7,Constants.DefaultPadding(), Constants.DefaultPadding()+7,Constants.DefaultPadding());
        }

        public override void SetPrimaryColors(Sd.Color color)
        {
            if(color.GetBrightness()>0.5)
            {
                Mat.ColorZoneAssist.SetMode(ctrl, Mat.ColorZoneMode.Dark);
            }
            else
            {
                Mat.ColorZoneAssist.SetMode(ctrl, Mat.ColorZoneMode.Light);
            }

            ctrl.Foreground = color.ToSolidColorBrush();
            ctrl.BorderBrush = color.ToSolidColorBrush();
        }

        public override void SetAccentColors(Sd.Color color)
        {
            border.Background = color.ToSolidColorBrush();
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
            return "Ui Dropdown List | " + this.Name;
        }

        #endregion

    }
}