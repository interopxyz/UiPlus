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
    public class UiScrollText : UiElement
    {

        #region Members

        Xcd.ButtonSpinner ctrl = new Xcd.ButtonSpinner();
        Wpf.Button btnUp = new Wpf.Button();
        Wpf.Button btnDown = new Wpf.Button();
        Wpf.Label label = new Wpf.Label();

        protected List<string> values = new List<string> { "Item 1", "Item 2", "Item 3" };
        protected int index = 0;
        protected bool wrap = false;

        #endregion

        #region Constructors

        public UiScrollText() : base()
        {
            SetInputs();
        }

        public UiScrollText(UiScrollText uiControl) : base(uiControl)
        {
            this.border = uiControl.border;

            this.values = uiControl.values;
            this.index = uiControl.index;
            this.wrap = uiControl.wrap;
        }

        #endregion

        #region Properties

        public virtual int Index
        {
            get { return index; }
            set
            {
                index = value;
                ctrl.Content = values[Cap()];
            }
        }

        public virtual bool Wrap
        {
            get { return wrap; }
            set { wrap = value; }
        }

        public virtual List<string> Values
        {
            get { return values; }
            set
            {
                values = value;
                ctrl.Content = values[Cap()];
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


        private int CapValue(bool dir)
        {
            if (dir) { Index += 1; } else { Index -= 1; }

            return Cap();
        }

        private int Cap()
        {
            int MinVal = 0;
            int MaxVal = values.Count - 1;

            if (index < MinVal) { if (Wrap) { index = MaxVal; } else { index = MinVal; } }
            if (index > MaxVal) { if (Wrap) { index = MinVal; } else { index = MaxVal; } }

            return index;
        }

        public void StepUp()
        {
            Index += 1;
        }

        public void StepDown()
        {
            Index -= 1;
        }

        #endregion

        #region Overrides

        public override void SetInputs()
        {
            ElementType = ElementTypes.Border;

            Wm.Brush defaultBrush = Constants.MaterialBrush();

            ctrl.MinWidth = 60;
            ctrl.Height = 20;
            ctrl.HorizontalAlignment = Sw.HorizontalAlignment.Stretch;
            ctrl.Background = Wm.Brushes.Transparent;
            ctrl.Foreground = defaultBrush;
            ctrl.BorderBrush = defaultBrush;
            ctrl.Margin = new Sw.Thickness(0, 0, 0, 0);
            ctrl.ShowSpinner = false;
            ctrl.Content = values[index];

            label.VerticalContentAlignment = Sw.VerticalAlignment.Center;
            label.Background = Wm.Brushes.Transparent;
            label.Foreground = defaultBrush;

            btnDown.Width = 16;
            btnDown.Height = 20;
            btnDown.Content = "▼";
            btnDown.FontSize = 8;
            btnDown.Margin = new Sw.Thickness(0, 1, 0, 0);
            btnDown.Padding = new Sw.Thickness(2, 0, 2, 2);
            btnDown.Foreground = defaultBrush;
            btnDown.FontWeight = Sw.FontWeights.Bold;
            btnDown.Background = Wm.Brushes.Transparent;
            btnDown.Click -= (o, e) => { StepDown(); };
            btnDown.Click += (o, e) => { StepDown(); };

            btnUp.Width = 16;
            btnUp.Height = 20;
            btnUp.Content = "▲";
            btnUp.FontSize = 8;
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

            pnl.Children.Add(btnDown);
            pnl.Children.Add(btnUp);
            pnl.Children.Add(control);
            pnl.Children.Add(label);

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

        public override void Update(Gk.GH_Component component)
        {
            ctrl.MouseUp -= (o, e) => { component.ExpireSolution(true); };
            ctrl.MouseUp += (o, e) => { component.ExpireSolution(true); };
            ctrl.MouseDown -= (o, e) => { component.ExpireSolution(true); };
            ctrl.MouseDown += (o, e) => { component.ExpireSolution(true); };

            btnUp.Click -= (o, e) => { component.ExpireSolution(true); };
            btnUp.Click += (o, e) => { component.ExpireSolution(true); };

            btnDown.Click -= (o, e) => { component.ExpireSolution(true); };
            btnDown.Click += (o, e) => { component.ExpireSolution(true); };
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

        public override List<object> GetValues()
        {
            return new List<object> { this.Index };
        }

        public override string ToString()
        {
            return "Ui Item Scroller | " + this.Name;
        }

        #endregion

    }
}