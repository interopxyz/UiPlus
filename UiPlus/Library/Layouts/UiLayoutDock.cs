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
    public class UiLayoutDock : UiElement
    {

        #region Members

        protected List<UiElement> elements = new List<UiElement>();

        public enum Directions { Left = 0,Top = 1, Right = 2, Bottom = 3 };
        protected Directions direction = Directions.Top;

        #endregion

        #region Constructors

        public UiLayoutDock() : base()
        {
            SetInputs();
        }

        public UiLayoutDock(UiLayoutDock uiControl) : base(uiControl)
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

        public virtual Directions Direction
        {
            get { return direction; }
            set
            {
                direction = value;
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
            Wpf.DockPanel ctrl = new Wpf.DockPanel();

            foreach (UiElement uiElement in elements)
            {
                uiElement.DetachParent();
                uiElement.SetElement();

                Wpf.DockPanel.SetDock(uiElement.Container, (Wpf.Dock)direction);

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
            return "Ui Layout Dock | " + this.Name;
        }

        #endregion

    }
}