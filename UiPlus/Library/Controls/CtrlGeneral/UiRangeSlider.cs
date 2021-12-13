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
using Wmc = System.Windows.Controls.Primitives;

namespace UiPlus.Elements
{
    public class UiRangeSlider : UiElement
    {

        #region Members

        Mah.RangeSlider ctrl = new Mah.RangeSlider();
        Wpf.TextBox text = new Wpf.TextBox();
        Wpf.Label label = new Wpf.Label();
        Wpf.DockPanel dock = new Wpf.DockPanel();
        Wpf.Border brdr = new Wpf.Border();

        #endregion

        #region Constructors

        public UiRangeSlider() : base()
        {
            SetInputs();
        }

        public UiRangeSlider(UiRangeSlider uiControl) : base(uiControl)
        {
            this.control = uiControl.Control;
        }

        #endregion

        #region Properties

        public virtual Rg.Interval Domain
        {
            get { return new Rg.Interval(ctrl.Minimum, ctrl.Maximum); }
            set
            {
                ctrl.Minimum = value.Min;
                ctrl.Maximum = value.Max;
            }
        }

        public virtual Rg.Interval CurrentValue
        {
            get { return new Rg.Interval(ctrl.LowerValue, ctrl.UpperValue); }
            set
            {
                ctrl.LowerValue = value.Min;
                ctrl.UpperValue = value.Max;
                text.Text = value.Min + ", " + value.Max;
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

        public string getValue()
        {
            return (ctrl.LowerValue + ", " + ctrl.UpperValue);
        }

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
            text.Margin = new Sw.Thickness(4,0,4,0);
            text.Padding = new Sw.Thickness(0);
            //text.ShowButtonSpinner = false;

            label.VerticalContentAlignment = Sw.VerticalAlignment.Center;
            label.Background = Wm.Brushes.Transparent;
            label.Foreground = Constants.MaterialBrush();

            brdr.Background = Constants.MaterialBrush();
            brdr.BorderBrush = Constants.MaterialBrush();
            brdr.Width = double.NaN;
            brdr.HorizontalAlignment = Sw.HorizontalAlignment.Left;
            brdr.BorderThickness = new Sw.Thickness(0);
            brdr.Margin = new Sw.Thickness(0);
            brdr.Padding = new Sw.Thickness(0);
            brdr.CornerRadius = new Sw.CornerRadius(Constants.DefaultRadius());

            ctrl.ToolTip = new Wpf.ToolTip();
            ctrl.AutoToolTipPlacement = Wpf.Primitives.AutoToolTipPlacement.TopLeft;
            ctrl.HorizontalAlignment = Sw.HorizontalAlignment.Stretch;
            ctrl.VerticalAlignment = Sw.VerticalAlignment.Center;
            ctrl.Background = Wm.Brushes.Transparent;
            ctrl.Margin = new Sw.Thickness(4, 0, 4, 0);

            ctrl.UpperValueChanged -= (o, e) => { text.Text = getValue(); };
            ctrl.UpperValueChanged += (o, e) => { text.Text = getValue(); };
            ctrl.LowerValueChanged -= (o, e) => { text.Text = getValue(); };
            ctrl.LowerValueChanged += (o, e) => { text.Text = getValue(); };
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

        public override void Update(Gk.GH_Component component)
        {
            ((Mah.RangeSlider)control).UpperValueChanged -= (o, e) => { component.ExpireSolution(true); };
            ((Mah.RangeSlider)control).UpperValueChanged += (o, e) => { component.ExpireSolution(true); };
            ((Mah.RangeSlider)control).LowerValueChanged -= (o, e) => { component.ExpireSolution(true); };
            ((Mah.RangeSlider)control).LowerValueChanged += (o, e) => { component.ExpireSolution(true); };
            }

        public override List<object> GetValues()
        {
            return new List<object> { this.CurrentValue };
        }

        public override string ToString()
        {
            return "Ui Range Slider | " + this.Name;
        }

        #endregion

    }
}