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
    public class UiClock : UiElement
    {

        #region Members

        Mat.Clock ctrl = new Mat.Clock();

        #endregion

        #region Constructors

        public UiClock() : base()
        {
            SetInputs();
        }

        public UiClock(UiClock uiControl) : base(uiControl)
        {
            this.control = uiControl.Control;
        }

        #endregion

        #region Properties

        public virtual bool Mode
        {
            get { return ctrl.Is24Hours; }
            set { ctrl.Is24Hours = value; }
        }

        public virtual DateTime Time
        {
            get { return ctrl.Time; }
            set { ctrl.Time = value; }
        }

        #endregion

        #region Methods


        #endregion

        #region Overrides
        
        public override void SetInputs()
        {
            Mat.ColorZoneAssist.SetMode(ctrl, Mat.ColorZoneMode.Custom);

            this.control = this.ctrl;
            base.SetInputs();
        }

        public override void SetAccentColors(Sd.Color color)
        {
            Mat.ColorZoneAssist.SetBackground(ctrl, color.ToSolidColorBrush());
        }

        public override void SetPrimaryColors(Sd.Color color)
        {
            Mat.ColorZoneAssist.SetForeground(ctrl, color.ToSolidColorBrush());
        }

        public override void Update(Gk.GH_Component component)
        {
            ctrl.LayoutUpdated -= (o, e) => { component.ExpireSolution(true); };
            ctrl.LayoutUpdated += (o, e) => { component.ExpireSolution(true); };
        }

        public override List<object> GetValues()
        {
            return new List<object> { this.Time };
        }

        public override string ToString()
        {
            return "Ui Clock | " + this.Name;
        }

        #endregion

    }
}