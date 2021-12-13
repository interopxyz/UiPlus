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

using ControlzEx.Theming;

namespace UiPlus.Elements
{
    public class UiColorCanvasPicker : UiElement
    {

        #region Members

        Xcd.ColorPicker ctrl = new Xcd.ColorPicker();
        Theme newTheme = null;

        #endregion

        #region Constructors

        public UiColorCanvasPicker() : base()
        {
            SetInputs();
        }

        public UiColorCanvasPicker(UiColorCanvasPicker uiControl) : base(uiControl)
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

            this.ctrl.Background = Wm.Brushes.Transparent;
            this.ctrl.Foreground = Constants.MaterialBrush();
            this.ctrl.BorderBrush = Wm.Brushes.Transparent;
            this.ctrl.Margin = new Sw.Thickness(0);
            this.ctrl.Padding = new Sw.Thickness(0);

            this.ctrl.MinWidth = 90;
            this.ctrl.ShowDropDownButton = false;
            this.ctrl.ShowStandardColors = false;
            this.ctrl.ShowRecentColors = false;
            this.ctrl.ShowAvailableColors = false;
            this.ctrl.ColorMode = Xcd.ColorMode.ColorCanvas;
            this.ctrl.ShowTabHeaders = false;

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

        public override void SetPrimaryColors(Sd.Color color)
        {
            newTheme = new Theme(name: "PaletteTheme",
                                      displayName: "PaletteTheme",
                                      baseColorScheme: "Light",
                                      colorScheme: "CustomAccent",
                                      primaryAccentColor: color.ToMediaColor(),
                                      showcaseBrush: color.ToSolidColorBrush(),
                                      isRuntimeGenerated: true,
                                      isHighContrast: false);
            ThemeManager.Current.ChangeTheme(ctrl, newTheme);

        }

        public override List<object> GetValues()
        {
            return new List<object> { this.Color };
        }

        public override string ToString()
        {
            return "Ui Color Canvas Picker | " + this.Name;
        }

        #endregion

    }
}