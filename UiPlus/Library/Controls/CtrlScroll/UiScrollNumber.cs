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
    public class UiScrollNumber : UiElement
    {

        #region Members

        Xcd.DoubleUpDown ctrl = new Xcd.DoubleUpDown();
        Wpf.Button btnUp = new Wpf.Button();
        Wpf.Button btnDown = new Wpf.Button();
        Wpf.Label label = new Wpf.Label();

        bool wrap = false;

        #endregion

        #region Constructors

        public UiScrollNumber() : base()
        {
            SetInputs();
        }

        public UiScrollNumber(UiScrollNumber uiControl) : base(uiControl)
        {
            this.border = uiControl.border;

            this.wrap = uiControl.wrap;
        }

        #endregion

        #region Properties

        public virtual double Value
        {
            get { return (double)ctrl.Value; }
            set { ctrl.Value = value; }
        }

        public virtual double Minimum
        {
            get { return (double)ctrl.Minimum; }
            set { ctrl.Minimum = value; }
        }

        public virtual double Maximum
        {
            get { return (double)ctrl.Maximum; }
            set { ctrl.Maximum = value; }
        }

        public virtual double Increment
        {
            get { return (double)ctrl.Increment; }
            set { ctrl.Increment = value; }
        }

        public virtual int Digits
        {
            set { ctrl.FormatString = "0." + string.Join("", Enumerable.Repeat("0", value)); }
        }

        public virtual bool Wrap
        {
            set { wrap = value; }
            get { return wrap; }
        }

        public virtual string Label
        {
            get { return label.Content.ToString(); }
            set
            {
                label.Content = value;
            }
        }

        #endregion

        #region Methods

        protected void StepUp()
        {
            double temp = (double)ctrl.Value + (double)ctrl.Increment;
            if (temp > (double)ctrl.Maximum)
            {
                if (wrap)
                {
                    ctrl.Value = ctrl.Minimum+(ctrl.Value+ctrl.Increment-ctrl.Maximum);
                }
                else
                {
                    ctrl.Value = ctrl.Maximum;
                }
            }
            else
            {
                ctrl.Value = temp;
            }

        }

        protected void StepDown()
        {
            double temp = (double)ctrl.Value - (double)ctrl.Increment;
            if (temp < (double)ctrl.Minimum)
            {
                if (wrap)
                {
                    ctrl.Value = ctrl.Maximum - (ctrl.Value - ctrl.Minimum);
                }
                else
                {
                    ctrl.Value = ctrl.Minimum;
                }
            }
            else
            {
                ctrl.Value = temp;
            }

        }

        #endregion

        #region Overrides

        public override void SetInputs()
        {
            ElementType = ElementTypes.Border;

            Wm.Brush defaultBrush = Constants.MaterialBrush();

            ctrl.MinWidth = 60;
            ctrl.Height = 20;
            ctrl.TextAlignment = System.Windows.TextAlignment.Left;
            ctrl.HorizontalAlignment = Sw.HorizontalAlignment.Stretch;
            ctrl.Background = Wm.Brushes.Transparent;
            ctrl.Foreground = defaultBrush;
            ctrl.BorderBrush = defaultBrush;
            ctrl.Margin = new Sw.Thickness(0, 0, 0, 0);
            ctrl.ShowButtonSpinner = false;

            label.VerticalContentAlignment = Sw.VerticalAlignment.Center;
            label.Background = Wm.Brushes.Transparent;
            label.Foreground = defaultBrush;

            btnDown.Width = 16;
            btnDown.Height = 20;
            btnDown.Content = "-";
            btnDown.Margin = new Sw.Thickness(0, 1, 0, 0);
            btnDown.Padding = new Sw.Thickness(2, 0, 2, 2);
            btnDown.Foreground = defaultBrush;
            btnDown.FontWeight = Sw.FontWeights.Bold;
            btnDown.Background = Wm.Brushes.Transparent;
            btnDown.Click -= (o, e) => { StepDown(); };
            btnDown.Click += (o, e) => { StepDown(); };

            btnUp.Width = 16;
            btnUp.Height = 20;
            btnUp.Content = "+";
            btnUp.Margin = new Sw.Thickness(1, 1, 2, 0);
            btnUp.Padding = new Sw.Thickness(2, 0, 2, 2);
            btnUp.Foreground = defaultBrush;
            btnUp.FontWeight = Sw.FontWeights.Bold;
            btnUp.Background = Wm.Brushes.Transparent;
            btnUp.Click -= (o, e) => { StepUp(); };
            btnUp.Click += (o, e) => { StepUp(); };

            Wpf.StackPanel pnl = new Wpf.StackPanel();
            pnl.Orientation = Wpf.Orientation.Horizontal;
            pnl.FlowDirection = Sw.FlowDirection.LeftToRight;
            pnl.HorizontalAlignment = Sw.HorizontalAlignment.Left;

            this.control = this.ctrl;
            pnl.Children.Add(this.btnDown);
            pnl.Children.Add(this.btnUp);
            pnl.Children.Add(this.control);
            pnl.Children.Add(this.label);

            border.Child = pnl;
            base.SetInputs();
        }

        public override string FontFamily
        {
            set
            {
                SetFontFamily(ctrl, value);
                SetFontFamily(label, value);
            }
        }

        public override double FontSize
        {
            set
            {
                ctrl.FontSize = value;
                label.FontSize = value;
            }
        }

        public override bool IsBold
        {
            set
            {
                SetIsBold(ctrl, value);
                SetIsBold(label, value);
            }
        }

        public override bool IsItalic
        {
            set
            {
                SetIsItalic(ctrl, value);
                SetIsItalic(label, value);
            }
        }

        public override Justifications TextJustification
        {
            set
            {
                SetTextJustification(ctrl, value);
                SetTextJustification(label, value);
            }
        }

        public override void SetAccentColors(Sd.Color color)
        {
            base.SetAccentColors(color);
        }

        public override void SetPrimaryColors(Sd.Color color)
        {
            base.SetPrimaryColors(color);
            Wm.Brush brush = color.ToSolidColorBrush();

            ctrl.Foreground = brush;
            ctrl.BorderBrush = brush;
            btnUp.Foreground = brush;
            btnUp.BorderBrush = brush;
            btnDown.Foreground = brush;
            btnDown.BorderBrush = brush;
            label.Foreground = brush;
        }

        public override void Update(Gk.GH_Component component)
        {
            ctrl.ValueChanged -= (o, e) => { component.ExpireSolution(true); };
            ctrl.ValueChanged += (o, e) => { component.ExpireSolution(true); };
        }

        public override List<object> GetValues()
        {
            return new List<object> { this.Value };
        }

        public override string ToString()
        {
            return "Ui Numeric Scroller | " + this.Name;
        }

        #endregion

    }
}