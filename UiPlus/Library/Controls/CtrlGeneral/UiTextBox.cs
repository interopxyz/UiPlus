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
    public class UiTextBox: UiElement
    {

        #region Members

        Wpf.TextBox ctrl = new Wpf.TextBox();

        #endregion

        #region Constructors

        public UiTextBox() : base()
        {
            SetInputs();
        }

        public UiTextBox(UiTextBox uiControl) : base(uiControl)
        {
            this.border = uiControl.border;
        }

        #endregion

        #region Properties

        public virtual string Content
        {
            get { return ctrl.Text; }
            set { ctrl.Text = value; }
        }

        public virtual bool Wrap
        {
            get { return ctrl.TextWrapping == System.Windows.TextWrapping.Wrap; }
            set
            {
                if (value)
                {
                    ctrl.TextWrapping = System.Windows.TextWrapping.Wrap;
                }
                else
                {
                    ctrl.TextWrapping = System.Windows.TextWrapping.NoWrap;
                }
            }
        }

        public virtual double Width
        {
            get { return ctrl.Width; }
            set { ctrl.Width= value; }
        }

        #endregion

        #region Methods


        #endregion

        #region Overrides

        public override void SetInputs()
        {
            ElementType = ElementTypes.Border;

            this.control = this.ctrl;
            this.border.Child = control;
            base.SetInputs();
        }

        public override void SetPrimaryColors(Sd.Color color)
        {
            base.SetPrimaryColors(color);
            Mat.TextFieldAssist.SetUnderlineBrush(ctrl, color.ToSolidColorBrush());
        }

        public override void Update(Gk.GH_Component component)
        {
            ctrl.TextChanged -= (o, e) => { component.ExpireSolution(true); };
            ctrl.TextChanged += (o, e) => { component.ExpireSolution(true); };
        }

        public override List<object> GetValues()
        {
            return new List<object> { this.Content };
        }

        public override string ToString()
        {
            return "Ui Text Box | " + this.Name;
        }

        #endregion

    }
}