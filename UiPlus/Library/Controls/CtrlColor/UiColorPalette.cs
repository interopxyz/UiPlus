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
    public class UiColorPalette : UiElement
    {

        #region Members

        Mah.ColorPalette ctrl = new Mah.ColorPalette();
        Theme newTheme = null;

        #endregion

        #region Constructors

        public UiColorPalette() : base()
        {
            SetInputs();
        }

        public UiColorPalette(UiColorPalette uiControl) : base(uiControl)
        {
            this.control = uiControl.Control;
        }

        #endregion

        #region Properties

        public virtual Sd.Color Color
        {
            get 
            {
                if (ctrl.SelectedItem != null)
                {
                    return ((Wm.Color)ctrl.SelectedItem).ToDrawingColor();
                }
                else
                {
                    return Constants.MaterialColor();
                }
            }
            set { ctrl.SelectedItem = value.ToMediaColor(); }
        }

        public virtual List<Sd.Color> Palette
        {
            get
            {
                List<Sd.Color> colors = new List<Sd.Color>();
                foreach (Wm.Color clr in ctrl.ItemsSource)
                {
                    colors.Add(clr.ToDrawingColor());
                }
                return colors;
            }
            set
            {
                List<Wm.Color> colors = new List<Wm.Color>();
                foreach (Sd.Color clr in value)
                {
                    colors.Add(clr.ToMediaColor());
                }
                ctrl.ItemsSource = colors;
            }
        }

        #endregion

        #region Methods



        #endregion

        #region Overrides

        public override void SetInputs()
        {

            ctrl.ItemsSource = Constants.DefaultMediaColors();
            ctrl.HorizontalAlignment = Sw.HorizontalAlignment.Stretch;

            newTheme = new Theme(name: "PaletteTheme",
                                   displayName: "PaletteTheme",
                                   baseColorScheme: "Light",
                                   colorScheme: "CustomAccent",
                                   primaryAccentColor: Wm.Colors.Transparent,
                                   showcaseBrush: Constants.MaterialBrush(),
                                   isRuntimeGenerated: true,
                                   isHighContrast: false);
            ThemeManager.Current.ChangeTheme(ctrl, newTheme);
            ctrl.Header = null;

            this.control = this.ctrl;
            base.SetInputs();
        }

        public override void Update(Gk.GH_Component component)
        {
            ctrl.SelectionChanged -= (o, e) => { component.ExpireSolution(true); };
            ctrl.SelectionChanged += (o, e) => { component.ExpireSolution(true); };
        }

        public override List<object> GetValues()
        {
            return new List<object> { this.Color };
        }

        public override string ToString()
        {
            return "Ui Color Palette | " + this.Name;
        }

        #endregion

    }
}