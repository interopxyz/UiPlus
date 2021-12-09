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
    public class UiButton : UiElement
    {

        #region Members

        Wpf.Button ctrl = new Wpf.Button();

        #endregion

        #region Constructors

        public UiButton() : base()
        {
            SetInputs();
        }

        public UiButton(UiButton uiControl) : base(uiControl)
        {
            this.control = uiControl.Control;
        }

        #endregion

        #region Properties

        public virtual string Label
        {
            get { return ctrl.Content.ToString(); }
            set
            { ctrl.Content = value; }
        }

        public virtual bool State
        {
            get { return ctrl.IsPressed; }
        }

        #endregion

        #region Methods


        #endregion

        #region Overrides

        public override void SetInputs()
        {
            Mat.ButtonAssist.SetCornerRadius(ctrl, new Sw.CornerRadius(Constants.DefaultRadius()));

            this.ctrl.MinWidth = 90;
            this.control = this.ctrl;
            base.SetInputs(Alignment.Center);
        }

        public override void SetAccentColors(Sd.Color color)
        {
            ctrl.Foreground = color.ToSolidColorBrush();
        }

        public override void SetPrimaryColors(Sd.Color color)
        {
            ctrl.Background = color.ToSolidColorBrush();
        }

        public override void Update(Gk.GH_Component component)
        {
            ctrl.Click -= (o, e) => { component.ExpireSolution(true); };
            ctrl.Click += (o, e) => { component.ExpireSolution(true); };
        }

        public override List<object> GetValues()
        {
            return new List<object> { this.State };
        }

        public override string ToString()
        {
            return "Ui Button | "+this.Name;
        }

        #endregion

    }
}