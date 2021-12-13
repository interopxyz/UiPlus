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
using System.Drawing;

namespace UiPlus.Elements
{
    public class UiGradient : UiElement
    {

        #region Members

        Wpf.Label minLabel = new Wpf.Label();
        Wpf.Label maxLabel = new Wpf.Label();

        public enum TextAlignments { Right, Center, Left }

        protected TextAlignments textAlignment = TextAlignments.Right;

        protected Dictionary<double, Sd.Color> stops = new Dictionary<double, Sd.Color>();

        protected double width = 50;
        protected bool isHorizontal = false;

        protected string minValue = "Low";
        protected string maxValue = "High";

        protected Sd.Color textColor = Sd.Color.Black;

        protected string family = "Arial";
        protected bool isBold = true;
        protected bool isItalic = false;

        #endregion

        #region Constructors

        public UiGradient() : base()
        {
            SetInputs();
        }

        public UiGradient(UiGradient uiControl) : base(uiControl)
        {
            this.control = uiControl.Control;

            this.textAlignment = uiControl.textAlignment;
            this.stops = uiControl.stops;

            this.width = uiControl.width;
            this.isHorizontal = uiControl.isHorizontal;

            this.minValue = uiControl.minValue;
            this.maxValue = uiControl.maxValue;

            this.textColor = uiControl.textColor;

            this.family = uiControl.family;
            this.isBold = uiControl.isBold;
            this.isItalic = uiControl.isItalic;

            base.SetInputs();
        }

        #endregion

        #region Properties

        public virtual Dictionary<double, Sd.Color> Stops
        {
            get { return stops; }
            set
            {
                stops = value;
                SetGradient();
            }
        }

        public virtual TextAlignments TextAlignment
        {
            get { return textAlignment; }
            set
            {
                this.textAlignment = value;
                SetGradient();
            }
        }

        public virtual double Width
        {
            set
            {
                this.width = value;
                SetGradient();
            }
            get { return this.width; }
        }

        public virtual bool IsHorizontal
        {
            set
            {
                this.isHorizontal = value;
                SetGradient();
            }
            get { return isHorizontal; }
        }

        public virtual string MinValue
        {
            set
            {
                this.minValue = value;
                SetGradient();
            }
            get { return minValue; }
        }

        public virtual string MaxValue
        {
            set
            {
                this.maxValue = value;
                SetGradient();
            }
            get { return maxValue; }
        }

        public virtual Sd.Color TextColor
        {
            set
            {
                this.textColor = value;
                SetGradient();
            }
            get { return textColor; }
        }

        #endregion

        #region Methods

        public void SetGradient()
        {
            Wpf.Grid ctrl = new Wpf.Grid();
            minLabel = new Wpf.Label();
            maxLabel = new Wpf.Label();

            Wm.Brush brush = new Wm.SolidColorBrush(this.textColor.ToMediaColor());

            SetFontFamily( minLabel,family);
            SetIsBold(minLabel, isBold);
            minLabel.Foreground = brush;

            SetFontFamily(maxLabel, family);
            SetIsBold(maxLabel, isBold);
            maxLabel.Foreground = brush;

            minLabel.Content = minValue;
            maxLabel.Content = maxValue;

            if (isHorizontal)
            {
                border.HorizontalAlignment = Sw.HorizontalAlignment.Stretch;
                ctrl.HorizontalAlignment = Sw.HorizontalAlignment.Stretch;
                ctrl.VerticalAlignment = Sw.VerticalAlignment.Top;

                ctrl.Width = double.NaN;
                ctrl.MinWidth = 200;
                ctrl.MinHeight = width;

                minLabel.HorizontalAlignment = Sw.HorizontalAlignment.Left;
                minLabel.VerticalAlignment = Sw.VerticalAlignment.Center;

                maxLabel.HorizontalAlignment = Sw.HorizontalAlignment.Right;
                maxLabel.VerticalAlignment = Sw.VerticalAlignment.Center;

                ctrl.RowDefinitions.Add(new Wpf.RowDefinition());
                ctrl.ColumnDefinitions.Add(new Wpf.ColumnDefinition());
                ctrl.ColumnDefinitions.Add(new Wpf.ColumnDefinition());

                ctrl.ColumnDefinitions[0].Width = new Sw.GridLength(100, Sw.GridUnitType.Star);
                ctrl.ColumnDefinitions[1].Width = new Sw.GridLength(100, Sw.GridUnitType.Star);

                ctrl.HorizontalAlignment = Sw.HorizontalAlignment.Stretch;
                ctrl.VerticalAlignment = Sw.VerticalAlignment.Top;

                minLabel.LayoutTransform = new Wm.RotateTransform(90);
                maxLabel.LayoutTransform = new Wm.RotateTransform(90);

                Wpf.Grid.SetColumn(minLabel, 0);
                Wpf.Grid.SetRow(minLabel, 0);

                Wpf.Grid.SetColumn(maxLabel, 1);
                Wpf.Grid.SetRow(maxLabel, 0);
            }
            else
            {
                border.HorizontalAlignment = Sw.HorizontalAlignment.Left;
                ctrl.HorizontalAlignment = Sw.HorizontalAlignment.Right;
                ctrl.VerticalAlignment = Sw.VerticalAlignment.Stretch;

                ctrl.Width = width;
                ctrl.Height = double.NaN;
                ctrl.MinHeight = 200;

                minLabel.HorizontalAlignment = Sw.HorizontalAlignment.Center;
                minLabel.VerticalAlignment = Sw.VerticalAlignment.Bottom;

                maxLabel.HorizontalAlignment = Sw.HorizontalAlignment.Center;
                maxLabel.VerticalAlignment = Sw.VerticalAlignment.Top;

                ctrl.RowDefinitions.Add(new Wpf.RowDefinition());
                ctrl.RowDefinitions.Add(new Wpf.RowDefinition());
                ctrl.ColumnDefinitions.Add(new Wpf.ColumnDefinition());

                ctrl.RowDefinitions[0].Height = new Sw.GridLength(100, Sw.GridUnitType.Star);
                ctrl.RowDefinitions[1].Height = new Sw.GridLength(100, Sw.GridUnitType.Star);

                ctrl.HorizontalAlignment = Sw.HorizontalAlignment.Left;
                ctrl.VerticalAlignment = Sw.VerticalAlignment.Stretch;

                Wpf.Grid.SetColumn(minLabel, 0);
                Wpf.Grid.SetRow(minLabel, 1);

                Wpf.Grid.SetColumn(maxLabel, 0);
                Wpf.Grid.SetRow(maxLabel, 0);
            }

            ctrl.Children.Add(minLabel);
            ctrl.Children.Add(maxLabel);
            border.Background = BuildGradient();


            this.border.Child = ctrl;
        }

        public Wm.LinearGradientBrush BuildGradient()
        {
            Wm.GradientStopCollection gradientStops = new Wm.GradientStopCollection();

            foreach(KeyValuePair<double,Sd.Color> gstop in stops)
            {
                if(isHorizontal)
                {
                    gradientStops.Add(new Wm.GradientStop(gstop.Value.ToMediaColor(), gstop.Key));
                }
                else
                {
                    gradientStops.Add(new Wm.GradientStop(gstop.Value.ToMediaColor(), 1.0-gstop.Key));
                }
            }

            if (IsHorizontal) 
            { 
                return new Wm.LinearGradientBrush(gradientStops, 0.0); 
            } 
            else 
            {
                gradientStops.Reverse();
                return new Wm.LinearGradientBrush(gradientStops, 90.0); 
            }

        }

        #endregion

        #region Overrides

        public override void SetInputs()
        {
            this.ElementType = ElementTypes.Border;

            border.CornerRadius = new Sw.CornerRadius(Constants.DefaultRadius());
            border.Padding = new Sw.Thickness(Constants.DefaultPadding());
            border.Margin = new Sw.Thickness(1);

            SetGradient();
        }

        public override string FontFamily
        {
            set
            {
                this.family = value;
                SetFontFamily(minLabel, value);
                SetFontFamily(maxLabel, value);
            }
        }

        public override double FontSize
        {
            set
            {
                minLabel.FontSize = value;
                maxLabel.FontSize = value;
            }
        }

        public override bool IsBold
        {
            set
            {
                this.isBold = value;
                SetIsBold(minLabel, value);
                SetIsBold(maxLabel, value);
            }
        }

        public override bool IsItalic
        {
            set
            {
                this.isItalic = value;
                SetIsItalic(minLabel, value);
                SetIsItalic(maxLabel, value);
            }
        }

        public override void SetAccentColors(Color color)
        {

        }

        public override void SetPrimaryColors(Color color)
        {
            TextColor = color;
        }

        public override List<object> GetValues()
        {
            return new List<object> { this.minValue, this.maxValue };
        }

        public override string ToString()
        {
            return "Ui Gradient | " + this.Name;
        }

        #endregion

    }
}