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
using System.Collections.ObjectModel;

namespace UiPlus.Elements
{
    public class UiColorPalettePicker : UiElement
    {

        #region Members

        Xcd.ColorPicker ctrl = new Xcd.ColorPicker();

        #endregion

        #region Constructors

        public UiColorPalettePicker() : base()
        {
            SetInputs();
        }

        public UiColorPalettePicker(UiColorPalettePicker uiControl) : base(uiControl)
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

        public virtual List<Sd.Color> Palette
        {
            get 
            {
                List<Sd.Color> colors = new List<Sd.Color>();
                foreach(Xcd.ColorItem clr in ctrl.AvailableColors)
                {
                    colors.Add(((Wm.Color)clr.Color).ToDrawingColor());
                }
                return colors;
            }
            set
            {
                ctrl.AvailableColors = DrawingToColorSet(value);
            }
        }

        #endregion

        #region Methods

        ObservableCollection<Xcd.ColorItem> DrawingToColorSet(List<Sd.Color> colors)
        {
            ObservableCollection<Xcd.ColorItem> ColorSet = new ObservableCollection<Xcd.ColorItem>();

            foreach (Sd.Color color in colors)
            {
                Wm.Color mColor = color.ToMediaColor();
                ColorSet.Add(new Xcd.ColorItem(mColor, mColor.ToString()));
            }

            return ColorSet;
        }

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

            this.ctrl.MinWidth = 90;
            this.ctrl.ShowDropDownButton = false;
            this.ctrl.ShowStandardColors = false;
            this.ctrl.ShowRecentColors = false;
            this.ctrl.ShowTabHeaders = false;
            this.ctrl.MaxDropDownWidth = 225;
            this.ctrl.AvailableColors = DrawingToColorSet(Constants.DefaultDrawingColors().ToList());

            this.control = this.ctrl;
            this.border.Child = this.control;
            base.SetInputs();
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
            return "Ui Color Palette Picker | " + this.Name;
        }

        #endregion

    }
}