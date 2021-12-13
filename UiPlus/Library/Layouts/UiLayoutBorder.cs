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
    public class UiLayoutBorder : UiElement
    {

        #region Members

        Wpf.GroupBox ctrl = new Wpf.GroupBox();
            Wpf.StackPanel stack = new Wpf.StackPanel();

        protected List<UiElement> elements = new List<UiElement>();
        protected string title = " ";

        #endregion

        #region Constructors

        public UiLayoutBorder() : base()
        {
            SetInputs();
        }

        public UiLayoutBorder(UiLayoutBorder uiControl) : base(uiControl)
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

        public virtual string Title
        {
            get { return ctrl.Header.ToString(); }
            set 
            {
                ctrl.Header = value;
            }
        }

        #endregion

        #region Methods


        #endregion

        #region Overrides

        public override void SetInputs()
        {
            this.ElementType = ElementTypes.Control;

            foreach (UiElement uiElement in elements)
            {
                uiElement.DetachParent();
                uiElement.SetElement();
                stack.Children.Add(uiElement.Container);
            }
            
            ctrl.Header = title;
            ctrl.Content = stack;
            this.control = ctrl;
        }

        public override void SetPrimaryColors(Sd.Color color)
        {
            Mat.ColorZoneAssist.SetBackground(ctrl, color.ToSolidColorBrush());
        }

        public override void SetAccentColors(Sd.Color color)
        {
            ctrl.Background = color.ToSolidColorBrush();
        }

        public override void SetStrokeColor(Sd.Color color)
        {
            Mat.ColorZoneAssist.SetForeground(ctrl, color.ToSolidColorBrush());
        }

        public override void SetStrokeWidth(double width)
        {
            ctrl.BorderThickness = new Sw.Thickness(width);
        }

        public override List<object> GetValues()
        {
            return new List<object> { null };
        }

        public override string ToString()
        {
            return "Ui Layout Border | " + this.Name;
        }

        #endregion

    }
}