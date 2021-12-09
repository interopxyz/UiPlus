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
    public class UiBrowser: UiElement
    {

        #region Members


        #endregion

        #region Constructors

        public UiBrowser() : base()
        {
            SetInputs();
        }

        public UiBrowser(UiBrowser uiControl) : base(uiControl)
        {
            this.control = uiControl.Control;
        }

        #endregion

        #region Properties

        public virtual string Address
        {
            set { browser.Navigate(value); }
        }

        #endregion

        #region Methods



        #endregion

        #region Overrides

        public override void SetInputs()
        {
            this.ElementType = ElementTypes.Browser;

            browser.VerticalAlignment = Sw.VerticalAlignment.Stretch;
            browser.HorizontalAlignment = Sw.HorizontalAlignment.Stretch;

            browser.MinHeight = 400;
        }

        public override List<object> GetValues()
        {
            return new List<object> { browser.Source };
        }

        public override string ToString()
        {
            return "Ui Browser | " + this.Name;
        }

        #endregion

    }
}