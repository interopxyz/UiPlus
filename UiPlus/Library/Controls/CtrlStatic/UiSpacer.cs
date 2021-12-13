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
    public class UiSpacer : UiElement
    {

        #region Members

        protected double width = 10;
        protected bool isHorizontal = false;

        #endregion

        #region Constructors

        public UiSpacer() : base()
        {
            SetInputs();
        }

        public UiSpacer(UiSpacer uiControl) : base(uiControl)
        {
            this.control = uiControl.Control;

            this.width = uiControl.width;
            this.isHorizontal = uiControl.isHorizontal;
        }

        #endregion

        #region Properties

        public virtual double Width
        {
            set
            {
                this.width = value;
                SetSizing();
            }
            get { return this.width; }
        }

        public virtual bool IsHorizontal
        {
            set 
            { 
                this.isHorizontal = value;
                SetSizing();
            }
            get { return isHorizontal; }
        }

        #endregion

        #region Methods

        public void SetSizing()
        {
            if (isHorizontal)
            {
                border.Width = double.NaN;
                border.HorizontalAlignment = Sw.HorizontalAlignment.Stretch;
                if (width == 0)
                {
                    border.Height = double.NaN;
                    border.VerticalAlignment = Sw.VerticalAlignment.Stretch;
                }
                else
                {
                    border.Height = width;
                }
            }
            else
            {
                border.Height = double.NaN;
                border.VerticalAlignment = Sw.VerticalAlignment.Stretch;
                if (width == 0)
                {
                    border.Width = double.NaN;
                    border.HorizontalAlignment = Sw.HorizontalAlignment.Stretch;
                }
                else
                {
                    border.Width = width;
                }
            }
        }

        #endregion

        #region Overrides

        public override void SetInputs()
        {
            this.ElementType = ElementTypes.Border;

            base.SetInputs(Alignment.Stretch);
        }

        public override List<object> GetValues()
        {
            return new List<object> { this.width };
        }

        public override string ToString()
        {
            return "Ui Spacer | " + this.Name;
        }

        #endregion

    }
}