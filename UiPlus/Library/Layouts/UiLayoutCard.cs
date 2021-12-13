using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Rhino.Geometry;

using Sd = System.Drawing;
using Sw = System.Windows;
using Wm = System.Windows.Media;

using Wpf = System.Windows.Controls;
using Mat = MaterialDesignThemes.Wpf;
using Mah = MahApps.Metro.Controls;
using Xcd = Xceed.Wpf.Toolkit;

namespace UiPlus.Elements
{
    public class UiLayoutCard : UiElement
    {

        #region Members
            Mat.Card ctrl = new Mat.Card();
            Wpf.GroupBox groupBox = new Wpf.GroupBox();
        Wpf.StackPanel panel = new Wpf.StackPanel();

        protected List<UiElement> elements = new List<UiElement>();


        #endregion

        #region Constructors

        public UiLayoutCard() : base()
        {
            SetInputs();
        }

        public UiLayoutCard(UiLayoutCard uiControl) : base(uiControl)
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
            get { return groupBox.Header.ToString(); }
            set
            {
                groupBox.Header = value;
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
                panel.Children.Add(uiElement.Container);
            }

            panel.Background = Wm.Brushes.Transparent;

            Mat.ColorZoneAssist.SetMode(groupBox, Mat.ColorZoneMode.Custom);

            SetStrokeColor(Constants.MaterialColor());
            SetPrimaryColors(Sd.Color.White);
            groupBox.Background = Wm.Brushes.Transparent;
            groupBox.BorderThickness = new Sw.Thickness(0);
            groupBox.Content = panel;

            ctrl.HorizontalAlignment = Sw.HorizontalAlignment.Stretch;
            ctrl.VerticalAlignment = Sw.VerticalAlignment.Top;

            ctrl.Content = groupBox;
            //ctrl.Name = title;
            this.control = ctrl;

        }

        public override List<object> GetValues()
        {
            return new List<object> { null };
        }

        public override void SetPrimaryColors(Sd.Color color)
        {
            Mat.ColorZoneAssist.SetForeground(groupBox, color.ToSolidColorBrush());
        }

        public override void SetAccentColors(Sd.Color color)
        {
            ctrl.Background = color.ToSolidColorBrush();
        }

        public override void SetStrokeColor(Sd.Color color)
        {
            Mat.ColorZoneAssist.SetBackground(groupBox, color.ToSolidColorBrush());
        }

        public override void SetStrokeWidth(double width)
        {
            groupBox.BorderThickness = new Sw.Thickness(width);
        }

        public override string ToString()
        {
            return "Ui Layout Card | " + this.Name;
        }

        #endregion

    }
}