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
    public class UiLabel : UiElement
    {

        #region Members

        Wpf.Label ctrl = new Wpf.Label();

        #endregion

        #region Constructors

        public UiLabel() : base()
        {
            SetInputs();
        }

        public UiLabel(UiLabel uiControl) : base(uiControl)
        {
            this.border = uiControl.border;
        }

        #endregion

        #region Properties

        public virtual string Content
        {
            get { return ctrl.Content.ToString(); }
            set { ctrl.Content = value; }
        }

        public virtual FontStyles Font
        {
            set { this.SetFont(value, ctrl); }
        }

        #endregion

        #region Methods



        #endregion

        #region Overrides

        public override void SetInputs()
        {
            this.ElementType = ElementTypes.Border;

            this.control = this.ctrl;
            this.border.Child = control;
            base.SetInputs();
        }

        public override List<object> GetValues()
        {
            return new List<object> { this.Content.ToString() };
        }

        public override string ToString()
        {
            return "Ui Label | " + this.Name;
        }

        #endregion

    }
}