using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Rhino.Geometry;
using Sd = System.Drawing;
using Wm = System.Windows.Media;

using Wpf = System.Windows.Controls;
using Mat = MaterialDesignThemes.Wpf;
using Mah = MahApps.Metro.Controls;
using Xcd = Xceed.Wpf.Toolkit;

namespace UiPlus.Elements
{
    public class UiColorCanvas : UiElement
    {

        #region Members



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
            get { return ((Wm.Color)((Xcd.ColorCanvas)control).SelectedColor).ToDrawingColor(); }
            set { ((Xcd.ColorCanvas)control).SelectedColor = value.ToMediaColor(); }
        }

        #endregion

        #region Methods



        #endregion

        #region Overrides

        public override void SetInputs()
        {
            Xcd.ColorCanvas ctrl = new Xcd.ColorCanvas();

            this.control = new Xcd.ColorCanvas();

            Inputs.Add(new UiInput(UiInput.InputTypes.Param_Colour, "Color", "C", "The control color.", Grasshopper.Kernel.GH_ParamAccess.item));
        }

        public override string ToString()
        {
            return "Ui Color Canvas | " + this.Name;
        }

        #endregion
    }
}