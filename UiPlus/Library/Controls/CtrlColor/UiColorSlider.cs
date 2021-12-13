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
    public class UiColorSlider : UiElement
    {

        #region Members

        Mat.ColorPicker ctrl = new Mat.ColorPicker();

        #endregion

        #region Constructors

        public UiColorSlider() : base()
        {
            SetInputs();
        }

        public UiColorSlider(UiColorSlider uiControl) : base(uiControl)
        {
            this.control = uiControl.Control;
        }

        #endregion

        #region Properties

        public virtual Sd.Color Color
        {
            get { return (ctrl.Color).ToDrawingColor(); }
            set { ctrl.Color = value.ToMediaColor(); }
        }

        #endregion

        #region Methods



        #endregion

        #region Overrides

        public override void SetInputs()
        {
            ElementType = ElementTypes.Border;

            this.ctrl.Background = Wm.Brushes.Transparent;
            this.ctrl.Foreground = Wm.Brushes.Transparent;
            this.ctrl.BorderBrush = Wm.Brushes.Transparent;
            this.ctrl.Margin = new Sw.Thickness(0);
            this.ctrl.Padding = new Sw.Thickness(0);

            this.control = this.ctrl;
            this.border.Child = this.control;
            base.SetInputs(Alignment.Stretch);
            this.border.Padding = new Sw.Thickness(0);
        }

        public override void Update(Gk.GH_Component component)
        {
            ctrl.MouseDown -= (o, e) => { component.ExpireSolution(true); };
            ctrl.MouseDown += (o, e) => { component.ExpireSolution(true); };
            ctrl.MouseUp -= (o, e) => { component.ExpireSolution(true); };
            ctrl.MouseUp += (o, e) => { component.ExpireSolution(true); };
        }

        public override List<object> GetValues()
        {
            return new List<object> { ctrl.Color.ToDrawingColor() };
        }

        public override string ToString()
        {
            return "Ui Color Slider | " + this.Name;
        }

        #endregion

    }
}