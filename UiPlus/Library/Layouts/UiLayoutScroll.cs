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
    public class UiLayoutScroll : UiElement
    {

        #region Members

        protected List<UiElement> elements = new List<UiElement>();

        public enum Visibility { Auto = 1, Hidden = 2, Visible = 3 };

        protected Visibility horizontal = Visibility.Auto;
        protected Visibility vertical = Visibility.Auto;

        #endregion

        #region Constructors

        public UiLayoutScroll() : base()
        {
            SetInputs();
        }

        public UiLayoutScroll(UiLayoutScroll uiControl) : base(uiControl)
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

        public virtual Visibility Horizontal
        {
            get { return horizontal; }
            set
            {
                horizontal = value;
                SetInputs();
            }
        }

        public virtual Visibility Vertical
        {
            get { return vertical; }
            set
            {
                vertical = value;
                SetInputs();
            }
        }


        #endregion

        #region Methods


        #endregion

        #region Overrides

        public override void SetInputs()
        {
            Wpf.ScrollViewer ctrl = new Wpf.ScrollViewer();
            Wpf.StackPanel panel = new Wpf.StackPanel();

            foreach (UiElement uiElement in elements)
            {
                uiElement.DetachParent();
                uiElement.SetElement();
                panel.Children.Add(uiElement.Container);
            }
            ctrl.Content = panel;

            ctrl.VerticalScrollBarVisibility = (Wpf.ScrollBarVisibility)vertical;
            ctrl.HorizontalScrollBarVisibility= (Wpf.ScrollBarVisibility)horizontal;

            this.control = ctrl;
        }

        public override List<object> GetValues()
        {
            return new List<object> { null };
        }

        public override string ToString()
        {
            return "Ui Layout Scroll | " + this.Name;
        }

        #endregion

    }
}