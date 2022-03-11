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
using System.Windows.Controls;

namespace UiPlus.Elements
{
    public class UiTextBlock : UiElement
    {

        #region Members



        #endregion

        #region Constructors

        public UiTextBlock() : base()
        {
            SetInputs();
        }

        public UiTextBlock(UiTextBlock uiControl) : base(uiControl)
        {
            this.control = uiControl.Control;
        }

        #endregion

        #region Properties

        public virtual string Content
        {
            get { return block.Text; }
            set { block.Text = value; }
        }

        #endregion

        #region Methods



        #endregion

        #region Overrides

        public override void SetInputs()
        {
            this.ElementType = ElementTypes.Border;

            block.TextWrapping = System.Windows.TextWrapping.Wrap;
            
            this.border.Child = block;
            base.SetInputs(Alignment.Stretch);
        }

        public override List<object> GetValues()
        {
            return new List<object> { this.Content.ToString() };
        }

        public override string ToString()
        {
            return "Ui Text Block | " + this.Name;
        }

        #endregion

    }
}