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
    public class UiColorCanvas : UiElement
    {

        #region Members

        Mah.ColorCanvas ctrl = new Mah.ColorCanvas();

        #endregion

        #region Constructors

        public UiColorCanvas() : base()
        {
            SetInputs();
        }

        public UiColorCanvas(UiColorCanvas uiControl) : base(uiControl)
        {
            this.control = uiControl.Control;
        }

        #endregion

        #region Properties

        public virtual Sd.Color Color
        {
            get { return ((Wm.Color)ctrl.SelectedColor).ToDrawingColor(); }
            set { ctrl.SelectedColor = value.ToMediaColor(); }
        }

        #endregion

        #region Methods



        #endregion

        #region Overrides

        public override void SetInputs()
        {
            ElementType = ElementTypes.Border;

            this.ctrl.HorizontalAlignment = Sw.HorizontalAlignment.Stretch;
            this.ctrl.Margin = new Sw.Thickness(0);

            this.control = this.ctrl;
            this.border.Child = this.control;
            base.SetInputs( Alignment.Stretch);
            this.border.Padding = new Sw.Thickness(0);
        }

        public override void Update(Gk.GH_Component component)
        {
            ctrl.SelectedColorChanged -= (o, e) => { component.ExpireSolution(true); };
            ctrl.SelectedColorChanged += (o, e) => { component.ExpireSolution(true); };
        }

        public override List<object> GetValues()
        {
            return new List<object> { this.Color };
        }

        public override string ToString()
        {
            return "Ui Color Canvas | " + this.Name;
        }

        #endregion

    }
}