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
    public class UiLayoutExpander : UiElement
    {

        #region Members

        Wpf.Expander ctrl = new Wpf.Expander();
        Wpf.StackPanel panel = new Wpf.StackPanel();

        protected List<UiElement> elements = new List<UiElement>();

        public enum Directions { Down = 0, Up = 1, Left = 2, Right = 3 };
        protected Directions direction = Directions.Down;

        #endregion

        #region Constructors

        public UiLayoutExpander() : base()
        {
            SetInputs();
        }

        public UiLayoutExpander(UiLayoutExpander uiControl) : base(uiControl)
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
            get { return (Directions)ctrl.ExpandDirection; }
            set
            {
                ctrl.ExpandDirection = (Wpf.ExpandDirection)value;
            }
        }

        public virtual string Title
        {
            get { return ctrl.Header.ToString(); }
            set
            {
                ctrl.Header = value;
            }
        }

        public virtual bool Closed
        {
            get { return ctrl.IsExpanded; }
            set
            {
                ctrl.IsExpanded = !value;
            }
        }

        #endregion

        #region Methods


        #endregion

        #region Overrides

        public override void SetInputs()
        {

            foreach (UiElement uiElement in elements)
            {
                uiElement.DetachParent();
                uiElement.SetElement();
                panel.Children.Add(uiElement.Container);
            }
            Mat.ColorZoneAssist.SetMode(ctrl, Mat.ColorZoneMode.Custom);
            ctrl.Content = panel;

            this.control = ctrl;
        }

        public override void SetStrokeColor(Sd.Color color)
        {
            base.SetStrokeColor(color);
            Mat.ExpanderAssist.SetHeaderBackground(ctrl, color.ToSolidColorBrush());
        }

        public override void SetPrimaryColors(Sd.Color color)
        {
           ctrl.Foreground = color.ToSolidColorBrush();
        }

        public override void SetAccentColors(Sd.Color color)
        {
            ctrl.Background = color.ToSolidColorBrush();
        }

        public override List<object> GetValues()
        {
            return new List<object> { null };
        }

        public override string ToString()
        {
            return "Ui Layout Expander | " + this.Name;
        }

        #endregion

    }
}