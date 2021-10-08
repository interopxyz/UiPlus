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
using System.Collections.ObjectModel;

namespace UiPlus.Elements
{
    public class UiColorPicker : UiElement
    {

        #region Members



        #endregion

        #region Constructors

        public UiColorPicker() : base()
        {
            SetInputs();
        }

        public UiColorPicker(UiColorPicker uiControl) : base(uiControl)
        {
            this.control = uiControl.Control;
        }

        #endregion

        #region Properties

        public virtual Sd.Color Color
        {
            get { return ((Wm.Color)((Xcd.ColorPicker)control).SelectedColor).ToDrawingColor(); }
            set { ((Xcd.ColorPicker)control).SelectedColor = value.ToMediaColor(); }
        }

        public virtual List<Sd.Color> Palette
        {
            get 
            {
                List<Sd.Color> colors = new List<Sd.Color>();
                foreach(Xcd.ColorItem clr in ((Xcd.ColorPicker)control).AvailableColors)
                {
                    colors.Add(((Wm.Color)clr.Color).ToDrawingColor());
                }
                return colors;
            }
            set
            {
                ObservableCollection<Xcd.ColorItem> ColorSet = new ObservableCollection<Xcd.ColorItem>();

                foreach(Sd.Color color in value)
                {
                    Wm.Color mColor = color.ToMediaColor();
                    ColorSet.Add(new Xcd.ColorItem(mColor, mColor.ToString()));
                }

                ((Xcd.ColorPicker)control).AvailableColors = ColorSet;
            }
        }

        #endregion

        #region Methods



        #endregion

        #region Overrides

        public override void SetInputs()
        {
            Xcd.ColorPicker ctrl = new Xcd.ColorPicker();
            ctrl.ShowDropDownButton = false;
            this.control = ctrl;

            Inputs.Add(new UiInput(UiInput.InputTypes.Param_Colour, "Color", "C", "The control's color.", Grasshopper.Kernel.GH_ParamAccess.item));
            Inputs.Add(new UiInput(UiInput.InputTypes.Param_Colour, "Palette", "P", "The control's optional color set.", Grasshopper.Kernel.GH_ParamAccess.list));
        }

        public override List<object> GetValues()
        {
            return new List<object> { this.Color };
        }

        public override string ToString()
        {
            return "Ui Toggle | " + this.Name;
        }

        #endregion
    }
}