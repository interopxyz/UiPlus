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
    public class UiColorEyeDropper : UiElement
    {

        #region Members

        Mah.ColorEyeDropper ctrl = new Mah.ColorEyeDropper();

        #endregion

        #region Constructors

        public UiColorEyeDropper() : base()
        {
            SetInputs();
        }

        public UiColorEyeDropper(UiColorEyeDropper uiControl) : base(uiControl)
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

        private void SetBackground()
        {
            ctrl.Background = ((Wm.Color)ctrl.SelectedColor).ToSolidColorBrush();
        }

        #endregion

        #region Overrides

        public override void SetInputs()
        {

            this.ctrl.MinWidth = 90;
            this.ctrl.SelectedColorChanged -= (o, e) => { SetBackground(); };
            this.ctrl.SelectedColorChanged += (o, e) => { SetBackground(); };

            this.control = this.ctrl;
            base.SetInputs();
        }

        public override void SetAccentColors(Sd.Color color)
        {
            
        }

        public override void SetPrimaryColors(Sd.Color color)
        {
            base.SetPrimaryColors(color);
        }

        public override void Update(Gk.GH_Component component)
        {
            ctrl.SelectedColorChanged -= (o, e) => 
            {
                SetBackground();
                component.ExpireSolution(true); 
            };
            ctrl.SelectedColorChanged += (o, e) =>
            {
                SetBackground();
                component.ExpireSolution(true); 
            };
        }

        public override List<object> GetValues()
        {
            return new List<object> { this.Color };
        }

        public override string ToString()
        {
            return "Ui Eye Dropper | " + this.Name;
        }

        #endregion

    }
}