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
    public class UiSlider : UiElement
    {

        #region Members

        Wpf.Slider ctrl = new Wpf.Slider();
        Wpf.TextBox text = new Wpf.TextBox();
        Wpf.Label label = new Wpf.Label();
        Wpf.DockPanel dock = new Wpf.DockPanel();
        Wpf.Border brdr = new Wpf.Border();

        protected int precision = 3;

        #endregion

        #region Constructors

        public UiSlider() : base()
        {
            SetInputs();
        }

        public UiSlider(UiSlider uiControl) : base(uiControl)
        {
            this.border = uiControl.border;
        }

        #endregion

        #region Properties

        public virtual double CurrentValue
        {
            get { return ctrl.Value; }
            set 
            { 
                ctrl.Value = value;
                text.Text = Math.Round(value,precision).ToString();
            }
        }

        public virtual Rg.Interval Domain
        {
            get { return new Rg.Interval(ctrl.Minimum, ctrl.Maximum); }
            set 
            {
                ctrl.Minimum = value.Min;
                ctrl.Maximum = value.Max;
            }
        }

        public virtual double Increment
        {
            get { return ctrl.TickFrequency; }
            set
            {
                ctrl.TickFrequency = value;
                if (value > 0)
                {
                    ctrl.IsSnapToTickEnabled = true; 
                } 
                else 
                {
                    ctrl.IsSnapToTickEnabled = false; 
                }
            }
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

        //protected void StepUp()
        //{
        //    double temp = (double)upDown.Value + (double)upDown.Increment;
        //    if (temp > (double)upDown.Maximum)
        //    {
        //        upDown.Value = upDown.Maximum;
        //    }
        //    else
        //    {
        //        upDown.Value = temp;
        //    }

        //}

        //protected void StepDown()
        //{
        //    double temp = (double)upDown.Value - (double)upDown.Increment;
        //    if (temp < (double)upDown.Minimum)
        //    {
        //        upDown.Value = upDown.Minimum;
        //    }
        //    else
        //    {
        //        upDown.Value = temp;
        //    }

        //}


        #endregion

        #region Overrides

        public override void SetInputs()
        {
            ElementType = ElementTypes.Border;

            text.MinWidth = 60;
            text.IsReadOnly = true;
            text.HorizontalContentAlignment = Sw.HorizontalAlignment.Center;
            text.HorizontalAlignment = Sw.HorizontalAlignment.Center;
            text.VerticalContentAlignment = Sw.VerticalAlignment.Center;
            text.Background = Wm.Brushes.Transparent;
            text.Foreground = Wm.Brushes.White;
            text.BorderBrush = Wm.Brushes.Transparent;
            text.BorderThickness = new Sw.Thickness(0);
            text.Margin = new Sw.Thickness(4,2,4,2);
            text.Padding = new Sw.Thickness(0);
            //text.ShowButtonSpinner = false;

            label.VerticalContentAlignment = Sw.VerticalAlignment.Center;
            label.Background = Wm.Brushes.Transparent;
            label.Foreground = Constants.MaterialBrush();

            brdr.Background = Constants.MaterialBrush();
            brdr.BorderBrush = Constants.MaterialBrush();
            brdr.HorizontalAlignment = Sw.HorizontalAlignment.Stretch;
            brdr.BorderThickness = new Sw.Thickness(0);
            brdr.Margin = new Sw.Thickness(0);
            brdr.Padding = new Sw.Thickness(0);
            brdr.CornerRadius = new Sw.CornerRadius(Constants.DefaultRadius());

            ctrl.ToolTip = new Wpf.ToolTip();
            ctrl.AutoToolTipPlacement = Wpf.Primitives.AutoToolTipPlacement.TopLeft;
            ctrl.AutoToolTipPrecision = precision;
            ctrl.HorizontalAlignment = Sw.HorizontalAlignment.Stretch;
            ctrl.VerticalAlignment = Sw.VerticalAlignment.Center;
            ctrl.Background = Wm.Brushes.Transparent;
            ctrl.Margin = new Sw.Thickness(4, 0, 4, 0);

            ctrl.ValueChanged -= (o, e) => { text.Text = Math.Round(ctrl.Value, precision).ToString(); };
            ctrl.ValueChanged += (o, e) => { text.Text = Math.Round(ctrl.Value, precision).ToString(); };
            //upDown.ValueChanged -= (o, e) => { ctrl.Value = (double)upDown.Value; };
            //upDown.ValueChanged += (o, e) => { ctrl.Value = (double)upDown.Value; };


            brdr.Child = text;

            this.control = this.ctrl;

            dock.HorizontalAlignment = Sw.HorizontalAlignment.Stretch;
            dock.Children.Add(brdr);
            dock.Children.Add(this.label);
            dock.Children.Add(this.control);

            Wpf.DockPanel.SetDock(brdr, Wpf.Dock.Left);
            Wpf.DockPanel.SetDock(this.label, Wpf.Dock.Right);
            Wpf.DockPanel.SetDock(this.control, Wpf.Dock.Right);

            this.border.Child = dock;
            base.SetInputs(Alignment.Stretch);
        }

        public override string FontFamily 
        { 
            set 
            { 
                SetFontFamily(text, value);
                SetFontFamily(label, value);
            } 
        }

        public override double FontSize
        {
            set
            {
                text.FontSize = value;
                label.FontSize = value;
            }
        }

        public override bool IsBold
        { 
            set 
            {
                SetIsBold(text, value);
                SetIsBold(label, value);
            } 
        }

        public override bool IsItalic
        {
            set
            {
                SetIsItalic(text, value);
                SetIsItalic(label, value);
            }
        }

        public override Justifications TextJustification
        {
            set
            {
                SetTextJustification(text, value);
                SetTextJustification(label, value);
            }
        }

        public override void SetPrimaryColors(Sd.Color color)
        {
            ctrl.Foreground = color.ToSolidColorBrush();
            brdr.Background = color.ToSolidColorBrush();
            label.Foreground = color.ToSolidColorBrush();
        }

        public override void SetAccentColors(Sd.Color color)
        {
            text.Foreground = color.ToSolidColorBrush();
            border.Background = color.ToSolidColorBrush();
        }

        public override void SetStrokeColor(Sd.Color color)
        {
            base.SetStrokeColor(color);
        }

        public override void SetStrokeWidth(double width)
        {
            base.SetStrokeWidth(width);
        }

        public override void Update(Gk.GH_Component component)
        {
            ctrl.ValueChanged -= (o, e) => { component.ExpireSolution(true); };
            ctrl.ValueChanged += (o, e) => { component.ExpireSolution(true); };
        }

        public override List<object> GetValues()
        {
            return new List<object> { this.CurrentValue };

        }

        public override string ToString()
        {
            return "Ui Slider | " + this.Name;
        }

        #endregion

    }
}