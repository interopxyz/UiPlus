using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Rhino.Geometry;

using Sd = System.Drawing;
using Sw = System.Windows;
using Sm = System.Windows.Media;

using Wpf = System.Windows.Controls;
using Mat = MaterialDesignThemes.Wpf;
using Mah = MahApps.Metro.Controls;
using Xcd = Xceed.Wpf.Toolkit;

namespace UiPlus.Elements
{
    public class UiLayoutStack : UiElement
    {

        #region Members

        protected List<UiElement> elements = new List<UiElement>();

        protected bool isHorizontal = false;

        #endregion

        #region Constructors

        public UiLayoutStack() : base()
        {
            SetInputs();
        }

        public UiLayoutStack(UiLayoutStack uiControl) : base(uiControl)
        {
            this.control = uiControl.Control;
        }

        #endregion

        #region Properties

        public virtual List<UiElement> Elements
        {
            get { return elements; }
            set
            { 
                elements = value;
                SetInputs();
            }
        }

        public virtual bool IsHorizontal
        {
            get { return isHorizontal; }
            set
            {
                isHorizontal = value;
                SetInputs();
            }
        }

        #endregion

        #region Methods


        #endregion

        #region Overrides

        public override void SetInputs()
        {
            this.ElementType = ElementTypes.Layout;
            Wpf.StackPanel ctrl = new Wpf.StackPanel();
            if (isHorizontal) { ctrl.Orientation = Wpf.Orientation.Horizontal; } else { ctrl.Orientation = Wpf.Orientation.Vertical; }

            foreach(UiElement uiElement in elements)
            {
                uiElement.DetachParent();
                uiElement.SetElement();
                ctrl.Children.Add(uiElement.Container);
            }

            this.layout = ctrl;

        }

        public override List<object> GetValues()
        {
            return new List<object> { null };
        }

        public override string ToString()
        {
            return "Ui Layout Stack | " + this.Name;
        }

        #endregion

    }
}