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

        Wpf.Label labelCtrl = new Wpf.Label();

        #endregion

        #region Constructors

        public UiLabel() : base()
        {
            SetInputs();
        }

        public UiLabel(UiLabel uiControl) : base(uiControl)
        {
            this.layout = uiControl.layout;
        }

        #endregion

        #region Properties

        public virtual string Content
        {
            get { return labelCtrl.Content.ToString(); }
            set { labelCtrl.Content = value; }
        }

        public virtual FontStyles Font
        {
            set { this.SetFont(value, labelCtrl); }
        }

        #endregion

        #region Methods



        #endregion

        #region Overrides

        public override void SetInputs()
        {
            this.ElementType = ElementTypes.Border;

            this.border.Child = labelCtrl;
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