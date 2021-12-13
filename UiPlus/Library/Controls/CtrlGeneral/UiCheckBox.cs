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
    public class UiCheckBox : UiElement
    {

        #region Members

        Wpf.CheckBox ctrl = new Wpf.CheckBox();

        #endregion

        #region Constructors

        public UiCheckBox() : base()
        {
            SetInputs();
        }

        public UiCheckBox(UiCheckBox uiControl) : base(uiControl)
        {
            this.border = uiControl.border;
        }

        #endregion

        #region Properties

        public virtual string Label
        {
            get { return ctrl.Content.ToString(); }
            set { ctrl.Content = value; }
        }

        public virtual bool State
        {
            get { return (bool)ctrl.IsChecked; }
            set { ctrl.IsChecked = value; }
        }

        #endregion

        #region Methods


        #endregion

        #region Overrides

        public override void SetInputs()
        {
            ElementType = ElementTypes.Border;

            ctrl.Margin = new Sw.Thickness(0,0,3,0);

            this.control = this.ctrl;
            this.border.Child = this.control;
            base.SetInputs();
        }

        public override void SetPrimaryColors(Sd.Color color)
        {
            ctrl.Background = color.ToSolidColorBrush();
            ctrl.Foreground = color.ToSolidColorBrush();
            ctrl.BorderBrush = color.ToSolidColorBrush();
            Mat.ColorZoneAssist.SetMode(ctrl, Mat.ColorZoneMode.PrimaryDark);
        }

        public override void Update(Gk.GH_Component component)
        {
            ((Wpf.CheckBox)ctrl).Click -= (o, e) => { component.ExpireSolution(true); };
            ((Wpf.CheckBox)ctrl).Click += (o, e) => { component.ExpireSolution(true); };
        }

        public override List<object> GetValues()
        {
            return new List<object> { this.State };
        }

        public override string ToString()
        {
            return "Ui Check Box | " + this.Name;
        }

        #endregion

    }
}