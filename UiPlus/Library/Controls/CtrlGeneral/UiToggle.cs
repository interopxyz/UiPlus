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
using System.Windows.Controls;

namespace UiPlus.Elements
{
    public class UiToggle : UiElement
    {

        #region Members

        Wmc.ToggleButton ctrl = new Wmc.ToggleButton();
        Wpf.StackPanel stackPanel = new Wpf.StackPanel();
        Wpf.Label label = new Wpf.Label();

        #endregion

        #region Constructors

        public UiToggle() : base()
        {
            SetInputs();
        }

        public UiToggle(UiToggle uiControl) : base(uiControl)
        {
            this.border = uiControl.border;
        }

        #endregion

        #region Properties

        public virtual bool State
        {
            get { return (bool)ctrl.IsChecked; }
            set { ctrl.IsChecked = value; }
        }

        public virtual string Label
        {
            get { return label.Content.ToString(); }
            set { label.Content = value; }
        }

        #endregion

        #region Methods



        #endregion

        #region Overrides

        public override void SetInputs()
        {
            ElementType = ElementTypes.Border;
            ctrl.HorizontalAlignment = Sw.HorizontalAlignment.Left;
            ctrl.Margin = new Sw.Thickness(2, 2, 0, 2);

            stackPanel.Orientation = Wpf.Orientation.Horizontal;
            stackPanel.Background = Wm.Brushes.Transparent;
            
            this.control = this.ctrl;

            stackPanel.Children.Add(this.control);
            stackPanel.Children.Add(label);

            this.border.Child = stackPanel;
            base.SetInputs();
        }

        public override void SetPrimaryColors(Sd.Color color)
        {
            Mat.ToggleButtonAssist.SetSwitchTrackOnBackground(ctrl, color.ToSolidColorBrush());
            ctrl.Background = color.ToSolidColorBrush();
            ctrl.Foreground = color.ToSolidColorBrush();
            label.Foreground = color.ToSolidColorBrush();
        }

        public override string FontFamily { set => SetFontFamily(label,value); }
        public override double FontSize { set => label.FontSize = value; }
        public override bool IsBold { set => SetIsBold(label, value); }
        public override bool IsItalic { set => SetIsItalic(label,value); }
        public override Justifications TextJustification { set => SetTextJustification(label,value); }

        public override void Update(Gk.GH_Component component)
        {
            ctrl.Click -= (o, e) => { component.ExpireSolution(true); };
            ctrl.Click += (o, e) => { component.ExpireSolution(true); };
        }

        public override List<object> GetValues()
        {
            return new List<object> { this.State };
        }

        public override string ToString()
        {
            return "Ui Toggle | " + this.Name;
        }

        #endregion
    }
}