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
    public class UiLegend : UiElement
    {

        #region Members

        Wpf.WrapPanel wrapPanel = new Wpf.WrapPanel();

        public enum IconModes { Box, Dot, Bar, Fill, Underline }

        protected IconModes iconType = IconModes.Dot;
        protected bool isHorizontal = false;
        protected double spacing = 0;
        protected Dictionary<string, Sd.Color> entries = new Dictionary<string, Sd.Color>();
        protected bool isLight = false;

        #endregion

        #region Constructors

        public UiLegend() : base()
        {
            SetInputs();
        }

        public UiLegend(UiLegend uiControl) : base(uiControl)
        {
            this.control = uiControl.Control;

            this.iconType = uiControl.iconType;
            this.IsHorizontal = uiControl.IsHorizontal;
            this.spacing = uiControl.spacing;
            this.entries = uiControl.entries;
            this.IsLight = uiControl.isLight;

        }

        #endregion

        #region Properties

        public virtual Dictionary<string,Sd.Color> Entries
        {
            get { return entries; }
            set 
            {
                entries = value;
                SetLegend();
            }
        }

        public virtual IconModes IconType
        {
            get { return iconType; }
            set
            {
                this.iconType = value;
                SetLegend();
            }
        }

        public virtual bool IsHorizontal
        {
            get { return isHorizontal; }
            set
            {
                this.isHorizontal = value;
                SetLegend();
            }
        }

        public virtual bool IsLight
        {
            get { return IsLight; }
            set
            {
                this.IsLight = value;
                SetLegend();
            }
        }

        public virtual double Spacing
        {
            get { return spacing; }
            set
            {
                this.spacing = value;
                SetLegend();
            }
        }

        public virtual List<string> Titles
        {
            get { return entries.Keys.ToList(); }
        }

        #endregion

        #region Methods

        public void SetLegend()
        {
            wrapPanel.Margin = new Sw.Thickness(3);
            wrapPanel.Background = Wm.Brushes.Transparent;

            if (isHorizontal)
            {
                border.HorizontalAlignment = Sw.HorizontalAlignment.Stretch;
                wrapPanel.Orientation = Wpf.Orientation.Horizontal;
                wrapPanel.Height = double.NaN;
                wrapPanel.VerticalAlignment = Sw.VerticalAlignment.Stretch;
                if (spacing > 0) { wrapPanel.Width = spacing; } else { wrapPanel.Width = double.NaN; }
            }
            else
            {
                border.HorizontalAlignment = Sw.HorizontalAlignment.Left;
                wrapPanel.Orientation = Wpf.Orientation.Vertical;
                wrapPanel.Width = double.NaN;
                wrapPanel.HorizontalAlignment = Sw.HorizontalAlignment.Stretch;
                if (spacing > 0) { wrapPanel.Height = spacing; } else { wrapPanel.Height = double.NaN; }
            }

            wrapPanel.Children.Clear();
            foreach(KeyValuePair<string,Sd.Color> entry in entries)
            {
                wrapPanel.Children.Add(SetLegendItem(entry.Key, entry.Value));
            }

        }

        public virtual Sd.Color TextColor
        {
            set 
            {
                foreach(Wpf.StackPanel panel in wrapPanel.Children)
                {
                    ((Wpf.Label)panel.Children[1]).Foreground = value.ToSolidColorBrush();
                }
            }
        }

        private Wpf.Panel SetLegendItem(string name, System.Drawing.Color color)
        {
            Wpf.StackPanel panel = new Wpf.StackPanel();
            Wpf.Canvas canvas = new Wpf.Canvas();
            Wpf.Label text = new Wpf.Label();
            Wpf.Canvas spacer = new Wpf.Canvas();

            spacer.Background = new Wm.SolidColorBrush(Wm.Color.FromArgb(0, 0, 0, 0));
            spacer.Width = 20;
            spacer.Height = 10;

            canvas.Width = 12;
            canvas.Height = 12;
            Sw.Shapes.Path path = new Sw.Shapes.Path();

            switch (iconType)
            {
                case IconModes.Box:
                    path.Data = new Wm.RectangleGeometry(new Sw.Rect(1, 1, 10, 10));
                    break;
                case IconModes.Dot:
                    path.Data = new Wm.EllipseGeometry(new Sw.Rect(1, 1, 10, 10));
                    break;
                case IconModes.Bar:
                    path.Data = new Wm.RectangleGeometry(new Sw.Rect(0, 0, 4, 16));
                    canvas.Width = 4;
                    canvas.Height = 16;
                    spacer.Width = 4;
                    break;
                case IconModes.Underline:
                    canvas.Width = 0;
                    spacer.Width = 4;
                    text.BorderBrush = new Wm.SolidColorBrush(color.ToMediaColor());
                    text.BorderThickness = new Sw.Thickness(0, 0, 0, 4);
                    break;
                case IconModes.Fill:
                    canvas.Width = 0;
                    spacer.Width = 0;
                    text.Background = new Wm.SolidColorBrush(color.ToMediaColor());
                    text.Margin = new Sw.Thickness(1);
                    text.FontWeight = Sw.FontWeights.SemiBold;
                    break;
            }

            if (isLight) { text.Foreground = new Wm.SolidColorBrush(Wm.Color.FromArgb(255, 250, 250, 250)); }

            path.Fill = new Wm.SolidColorBrush(color.ToMediaColor());
            canvas.Children.Add(path);

            text.Content = name;

            panel.Orientation = Wpf.Orientation.Horizontal;
            panel.Children.Add(canvas);
            panel.Children.Add(text);
            panel.Children.Add(spacer);

            return panel;
        }

        #endregion

        #region Overrides

        public override void SetInputs()
        {
            this.ElementType = ElementTypes.Border;

            border.Child = wrapPanel;

            SetLegend();
            base.SetInputs(Alignment.Stretch);
        }

        public override void SetAccentColors(Sd.Color color)
        {
            this.border.Background = color.ToSolidColorBrush();
        }

        public override void SetPrimaryColors(Sd.Color color)
        {
            TextColor = color;
        }

        public override List<object> GetValues()
        {
            return new List<object> { this.Titles };
        }

        public override string ToString()
        {
            return "Ui Legend | " + this.Name;
        }

        #endregion

    }
}