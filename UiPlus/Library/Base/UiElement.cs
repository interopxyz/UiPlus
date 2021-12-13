using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Gk = Grasshopper.Kernel;

using Sd = System.Drawing;
using Sw = System.Windows;
using Wm = System.Windows.Media;

using Wpf = System.Windows.Controls;
using Mat = MaterialDesignThemes.Wpf;
using Mah = MahApps.Metro.Controls;
using Xcd = Xceed.Wpf.Toolkit;
using Swf = System.Windows.Forms.Integration;

namespace UiPlus.Elements
{
    public class UiElement
    {

        #region Members

        public enum Alignment { Left, Center, Right, Stretch }

        public enum ElementTypes { Control, Layout, Border, Image, Block, Browser, Host, Chart, Window };
        public ElementTypes ElementType = ElementTypes.Control;

        public enum FontStyles { Body, Bold, Title, Subtitle, Subtext };

        public enum Justifications { BottomLeft, BottomMiddle, BottomRight, CenterLeft, CenterMiddle, CenterRight, TopLeft, TopMiddle, TopRight };


        protected string name = string.Empty;
        protected string id = string.Empty;
        public Mat.ColorZone Container = new Mat.ColorZone();
        public Wpf.Panel layout = null;

        protected UiViewer viewer = new UiViewer();

        protected Wpf.Control control = null;
        protected Wpf.Border border = new Wpf.Border();
        protected Wpf.Image image = new Wpf.Image();
        protected Wpf.TextBlock block = new Wpf.TextBlock();
        protected Wpf.WebBrowser browser = new Wpf.WebBrowser();
        protected Swf.WindowsFormsHost host = new Swf.WindowsFormsHost();

        //public List<UiInput> Inputs = new List<UiInput>();

        #endregion

        #region Constructors

        public UiElement()
        {

        }

        public UiElement(UiElement uiElement)
        {
            this.name = uiElement.name;
            this.id = uiElement.id;

            this.control = uiElement.control;
            this.layout = uiElement.layout;
            this.image = uiElement.image;
            this.block = uiElement.block;
            this.browser = uiElement.browser;
            this.host = uiElement.host;

            //this.Inputs = uiElement.Inputs;
        }

        #endregion

        #region Properties

        public virtual string Name
        {
            get { return name; }
        }

        public virtual string Id
        {
            get { return id; }
        }

        public virtual Wpf.Control Control
        {
            get { return control; }
        }

        public virtual Wpf.Border Border
        {
            get { return border; }
        }

        #endregion

        #region Methods

        protected virtual void SetData()
        {

        }

        public virtual void SetFont(FontStyles fontStyle, Wpf.Control wpfControl = null)
        {
            switch (fontStyle)
            {
                default:
                    AssignFont("Calibri", 12, false, false, Sw.HorizontalAlignment.Left, Sw.VerticalAlignment.Top, wpfControl);
                    break;
                case FontStyles.Bold:
                    AssignFont("Arial", 24, false, true, Sw.HorizontalAlignment.Left, Sw.VerticalAlignment.Top, wpfControl);
                    break;
                case FontStyles.Title:
                    AssignFont("Arial", 24, false, false, Sw.HorizontalAlignment.Left, Sw.VerticalAlignment.Top, wpfControl);
                    break;
                case FontStyles.Subtitle:
                    AssignFont("Arial", 16, false, false, Sw.HorizontalAlignment.Left, Sw.VerticalAlignment.Top, wpfControl);
                    break;
                case FontStyles.Subtext:
                    AssignFont("Calibri Light", 10.5, false, false, Sw.HorizontalAlignment.Left, Sw.VerticalAlignment.Top, wpfControl);
                    break;
            }

        }

        #region Font

        public virtual string FontFamily
        {
            set
            {

                switch (ElementType)
                {
                    case ElementTypes.Control:
                        SetFontFamily(control, value);
                        break;
                    case ElementTypes.Border:
                        if (control != null) SetFontFamily(control, value);
                        break;
                }
            }
        }

        public virtual bool IsBold
        {
            set
            {
                switch (ElementType)
                {
                    case ElementTypes.Control:
                        SetIsBold(control, value);
                        break;
                    case ElementTypes.Border:
                        if (control != null) SetIsBold(control, value);
                        break;
                }
            }
        }

        public virtual bool IsItalic
        {
            set
            {
                switch (ElementType)
                {
                    case ElementTypes.Control:
                        SetIsItalic(control, value);
                        break;
                    case ElementTypes.Border:
                        if (control != null) SetIsItalic(control, value);
                        break;
                }
            }
        }

        public virtual double FontSize
        {
            set
            {
                switch (ElementType)
                {
                    case ElementTypes.Control:
                        control.FontSize = value;
                        break;
                    case ElementTypes.Border:
                        if (control != null) control.FontSize = value;
                        break;
                }
            }
        }

        public virtual Justifications TextJustification
        {
            set
            {
                switch (ElementType)
                {
                    case ElementTypes.Control:
                        SetTextJustification(control, value);
                        break;
                    case ElementTypes.Border:
                        if (control != null) SetTextJustification(control, value);
                        break;
                }
            }
        }

        protected virtual void SetFontFamily(Wpf.Control ctrl, string family)
        {
            ctrl.FontFamily = new Sw.Media.FontFamily(family);
        }

        protected virtual void SetIsBold(Wpf.Control ctrl, bool status)
        {
            if (status)
            {
                ctrl.FontWeight = Sw.FontWeights.Bold;
            }
            else
            {
                ctrl.FontWeight = Sw.FontWeights.Normal;
            }
        }

        protected virtual void SetIsItalic(Wpf.Control ctrl, bool status)
        {
            if (status)
            {
                ctrl.FontStyle = Sw.FontStyles.Italic;
            }
            else
            {
                ctrl.FontStyle = Sw.FontStyles.Normal;
            }
        }

        protected virtual Sw.HorizontalAlignment GetHAlignment(Justifications justification)
        {
            switch (justification)
            {
                case Justifications.BottomLeft:
                case Justifications.CenterLeft:
                case Justifications.TopLeft:
                    return Sw.HorizontalAlignment.Left;
                    break;
                case Justifications.BottomMiddle:
                case Justifications.CenterMiddle:
                case Justifications.TopMiddle:
                    return Sw.HorizontalAlignment.Center;
                    break;
                case Justifications.BottomRight:
                case Justifications.CenterRight:
                case Justifications.TopRight:
                    return Sw.HorizontalAlignment.Right;
                    break;
            }
            return Sw.HorizontalAlignment.Left;
        }
        protected virtual Sw.VerticalAlignment GetVAlignment(Justifications justification)
        {

            switch (justification)
            {
                case Justifications.BottomLeft:
                case Justifications.BottomMiddle:
                case Justifications.BottomRight:
                    return Sw.VerticalAlignment.Bottom;
                    break;
                case Justifications.CenterLeft:
                case Justifications.CenterMiddle:
                case Justifications.CenterRight:
                    return Sw.VerticalAlignment.Center;
                    break;
                case Justifications.TopLeft:
                case Justifications.TopMiddle:
                case Justifications.TopRight:
                    return Sw.VerticalAlignment.Top;
                    break;
            }
            return Sw.VerticalAlignment.Bottom;
        }


        protected virtual void SetTextJustification(Wpf.Control ctrl, Justifications justification)
        {
            ctrl.HorizontalContentAlignment = GetHAlignment(justification);
            ctrl.VerticalContentAlignment = GetVAlignment(justification);
        }

        private void AssignFont(string fontFamily, double size, bool isItalic, bool isBold, Sw.HorizontalAlignment horizontalAlignment, Sw.VerticalAlignment verticalAlignment, Wpf.Control wpfControl = null)
        {
            if (wpfControl != null)
            {
                wpfControl.FontFamily = new System.Windows.Media.FontFamily(fontFamily);
                wpfControl.FontSize = size;
                if (isItalic)
                {
                    wpfControl.FontStyle = Sw.FontStyles.Italic;
                }
                else
                {
                    wpfControl.FontStyle = Sw.FontStyles.Normal;
                }
                if (isBold)
                {
                    wpfControl.FontWeight = Sw.FontWeights.Bold;
                }
                else
                {
                    wpfControl.FontWeight = Sw.FontWeights.Normal;
                }
                wpfControl.HorizontalContentAlignment = horizontalAlignment;
                wpfControl.VerticalContentAlignment = verticalAlignment;
            }
            else
            {
                FontFamily = fontFamily;
                FontSize = size;
                IsItalic = isItalic;
                IsBold = isBold;

                control.HorizontalContentAlignment = horizontalAlignment;
                control.VerticalContentAlignment = verticalAlignment;
            }
        }

        #endregion

        #region Graphics

        public virtual void SetPrimaryColors(Sd.Color color)
        {

            switch (ElementType)
            {
                case ElementTypes.Control:
                    control.Foreground = color.ToSolidColorBrush();
                    break;
                case ElementTypes.Border:
                case ElementTypes.Layout:
                case ElementTypes.Image:
                case ElementTypes.Browser:
                    break;
                case ElementTypes.Block:
                    block.Foreground = color.ToSolidColorBrush();
                    break;
                case ElementTypes.Host:
                    host.Foreground = color.ToSolidColorBrush();
                    break;
                case ElementTypes.Window:
                    viewer.WindowTitleBrush = color.ToSolidColorBrush();
                    break;
            }
        }

        public virtual void SetAccentColors(Sd.Color color)
        {

            switch (ElementType)
            {
                case ElementTypes.Control:
                    control.Background = color.ToSolidColorBrush();
                    break;
                case ElementTypes.Border:
                    border.Background = color.ToSolidColorBrush();
                    break;
                case ElementTypes.Image:
                case ElementTypes.Browser:
                    break;
                case ElementTypes.Layout:
                    layout.Background = color.ToSolidColorBrush();
                    break;
                case ElementTypes.Block:
                    block.Background = color.ToSolidColorBrush();
                    break;
                case ElementTypes.Host:
                    host.Background = color.ToSolidColorBrush();
                    break;
                case ElementTypes.Window:
                    viewer.Background = color.ToSolidColorBrush();
                    break;
            }
        }


        public virtual void SetStrokeColor(Sd.Color color)
        {

            switch (ElementType)
            {
                case ElementTypes.Control:
                    control.BorderBrush = color.ToSolidColorBrush();
                    break;
                case ElementTypes.Border:
                    border.BorderBrush = color.ToSolidColorBrush();
                    break;
                case ElementTypes.Layout:
                case ElementTypes.Image:
                case ElementTypes.Browser:
                case ElementTypes.Block:
                case ElementTypes.Host:
                case ElementTypes.Window:
                    viewer.BorderBrush = color.ToSolidColorBrush();
                    break;
            }
        }

        public virtual void SetStrokeWidth(double width)
        {

            switch (ElementType)
            {
                case ElementTypes.Control:
                    control.BorderThickness = new Sw.Thickness(width);
                    break;
                case ElementTypes.Border:
                    border.BorderThickness = new Sw.Thickness(width);
                    break;
                case ElementTypes.Layout:
                case ElementTypes.Image:
                case ElementTypes.Browser:
                case ElementTypes.Block:
                case ElementTypes.Host:
                case ElementTypes.Window:
                    viewer.BorderThickness = new Sw.Thickness(width);
                    break;
            }
        }
        #endregion

        public virtual void SetPadding(double offset = 0)
        {
            SetPadding(offset, offset, offset, offset);
        }

        public virtual void SetPadding(double left, double top, double right, double bottom)
        {
            switch (ElementType)
            {
                case ElementTypes.Control:
                    control.Padding = new Sw.Thickness(left, top, right, bottom);
                    break;
                case ElementTypes.Border:
                    border.Padding = new Sw.Thickness(left, top, right, bottom);
                    break;
                case ElementTypes.Image:
                case ElementTypes.Browser:
                case ElementTypes.Layout:
                    break;
                case ElementTypes.Block:
                    block.Padding = new Sw.Thickness(left, top, right, bottom);
                    break;
                case ElementTypes.Host:
                    host.Padding = new Sw.Thickness(left, top, right, bottom);
                    break;
            }
        }

        public virtual void SetMargin(double offset = 0)
        {
            SetMargin(offset, offset, offset, offset);
        }

        public virtual void SetMargin(double left, double top, double right, double bottom)
        {
            switch (ElementType)
            {
                case ElementTypes.Control:
                    control.Margin = new Sw.Thickness(left, top, right, bottom);
                    control.UpdateLayout();
                    break;
                case ElementTypes.Border:
                    border.Margin = new Sw.Thickness(left, top, right, bottom);
                    break;
                case ElementTypes.Image:
                    image.Margin = new Sw.Thickness(left, top, right, bottom);
                    break;
                case ElementTypes.Browser:
                    browser.Margin = new Sw.Thickness(left, top, right, bottom);
                    break;
                case ElementTypes.Layout:
                    layout.Margin = new Sw.Thickness(left, top, right, bottom);
                    break;
                case ElementTypes.Block:
                    block.Margin = new Sw.Thickness(left, top, right, bottom);
                    break;
                case ElementTypes.Host:
                    host.Margin = new Sw.Thickness(left, top, right, bottom);
                    break;
            }
        }

        public virtual void SetSizing(double width = 0, double height = 0)
        {
            switch (ElementType)
            {
                case ElementTypes.Control:
                    if (width > 0) control.Width = width;
                    if (height > 0) control.Height = height;
                    break;
                case ElementTypes.Border:
                    if (width > 0) border.Width = width;
                    if (height > 0) border.Height = height;
                    break;
                case ElementTypes.Image:
                    if (width > 0) image.Width = width;
                    if (height > 0) image.Height = height;
                    break;
                case ElementTypes.Browser:
                    if (width > 0) browser.Width = width;
                    if (height > 0) browser.Height = height;
                    break;
                case ElementTypes.Layout:
                    if (width > 0) layout.Width = width;
                    if (height > 0) layout.Height = height;
                    break;
                case ElementTypes.Block:
                    if (width > 0) block.Width = width;
                    if (height > 0) block.Height = height;
                    break;
                case ElementTypes.Host:
                    if (width > 0) host.Width = width;
                    if (height > 0) host.Height = height;
                    break;
                case ElementTypes.Window:
                    if (width > 0) viewer.Width = width;
                    if (height > 0) viewer.Height = height;
                    break;
            }
        }

        public void SetElement()
        {
            Container.Background = Wm.Brushes.Transparent;
            switch (ElementType)
            {
                default:
                    Container.Content = control;
                    break;
                case ElementTypes.Layout:
                    Container.Content = layout;
                    break;
                case ElementTypes.Border:
                    Container.Content = border;
                    break;
                case ElementTypes.Image:
                    Container.Content = image;
                    break;
                case ElementTypes.Block:
                    Container.Content = block;
                    break;
                case ElementTypes.Browser:
                    Container.Content = browser;
                    break;
                case ElementTypes.Host:
                    Container.Content = host;
                    break;
            }

        }

        public void SetSpacing()
        {
            this.control.Margin = new Sw.Thickness(2);
        }

        public virtual void SetHorizontalAlignment(Alignment alignment = Alignment.Stretch)
        {
            switch (ElementType)
            {
                case ElementTypes.Control:
                    control.HorizontalAlignment = (Sw.HorizontalAlignment)alignment;
                    break;
                case ElementTypes.Border:
                    border.HorizontalAlignment = (Sw.HorizontalAlignment)alignment;
                    break;
            }
        }

        public virtual void SetCornerRadius(double radius = 2)
        {
            switch (ElementType)
            {
                case ElementTypes.Border:
                    border.CornerRadius = new Sw.CornerRadius(radius);
                    break;
            }
        }

        public virtual void SetInputs()
        {
            SetInputs(Alignment.Left);
        }

        public virtual void SetInputs(Alignment alignment = Alignment.Left)
        {
            SetMargin(0, 2, 0, 2);
            SetPadding(Constants.DefaultPadding());
            SetCornerRadius(Constants.DefaultRadius());

            SetHorizontalAlignment(alignment);

            SetStrokeColor(Sd.Color.Transparent);
            SetStrokeWidth(1);
        }


        public void DetachParent()
        {
            if (Container.Parent != null)
            {
                Wpf.Panel ParentLayout = Container.Parent as Wpf.Panel;
                ParentLayout.Children.Remove(Container);
            }
        }

        public virtual List<object> GetValues()
        {
            return null;
        }

        public virtual void Update(Gk.GH_Component component)
        {
        }

        public string GetElementType()
        {
            return control.GetType().Name.ToString();
        }


        public void AddElement(UiElement uiElement)
        {
            uiElement.DetachParent();
            uiElement.SetElement();
        }


        #endregion

    }
}