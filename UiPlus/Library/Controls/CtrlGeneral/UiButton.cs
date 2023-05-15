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
using System.IO;

namespace UiPlus.Elements
{
    public class UiButton : UiElement
    {

        #region Members
        Wpf.StackPanel contents = new Wpf.StackPanel();
        Mat.PackIcon icon = new Mat.PackIcon();
        Wpf.Image img = new Wpf.Image();
        Wpf.TextBlock text = new Wpf.TextBlock();
        Wpf.Button ctrl = new Wpf.Button();

        string iconName = "Check";
        bool iconMode = true;
        bool status = false;

        #endregion

        #region Constructors

        public UiButton() : base()
        {
            SetInputs();
        }

        public UiButton(UiButton uiControl) : base(uiControl)
        {
            this.control = uiControl.Control;
            this.icon = uiControl.icon;
            this.img = uiControl.img;
            this.text = uiControl.text;
            this.contents = uiControl.contents;
            this.iconName = uiControl.iconName;
            this.iconMode = uiControl.iconMode;
        }

        #endregion

        #region Properties

        public virtual string Label
        {
            get { return text.Text.ToString(); }
            set { text.Text = value; }
        }

        public virtual string Icon
        {
            get { return this.iconName; }
            set
            {
                bool isIcon = false;
                if (File.Exists(value))
                {
                    if (iconMode) if (this.contents.Children.Count > 1) this.contents.Children.RemoveAt(0);
                    string ext = Path.GetExtension(value);
                    ext = ext.ToLower();
                    switch(ext)
                    {
                        case ".png":
                        case ".jpg":
                        case ".jpeg":
                        case ".bmp":
                            Sd.Bitmap bitmap = new Sd.Bitmap(value, false);
                            this.image.Source = bitmap.ToImageSource();
                            if (this.contents.Children.Count < 2) this.contents.Children.Insert(0, this.image);
                            break;

                        default:
                            isIcon = true;
                            break;
                    }
                    iconMode = false;
                }
                else 
                { 
                    isIcon = true; 
                }

                if (isIcon) { 
                if(!iconMode) if (this.contents.Children.Count > 1) this.contents.Children.RemoveAt(0);
                    if (Enum.IsDefined(typeof(Mat.PackIconKind), value))
                    {
                        this.icon.Kind = (Mat.PackIconKind)Enum.Parse(typeof(Mat.PackIconKind), value);
                        if (this.contents.Children.Count < 2) this.contents.Children.Insert(0, this.icon);
                    }
                    iconMode = true;
                }

            }
        }

        public virtual bool State
        {
            get { return ctrl.IsPressed; }
        }

        #endregion

        #region Methods


        #endregion

        #region Overrides

        public override void SetInputs()
        {
            Mat.ButtonAssist.SetCornerRadius(ctrl, new Sw.CornerRadius(Constants.DefaultRadius()));

            this.icon.Kind = Mat.PackIconKind.Check;
            this.icon.VerticalAlignment = Sw.VerticalAlignment.Center;

            this.image.Stretch = Wm.Stretch.Fill;
            this.image.Margin = new Sw.Thickness(5);

            this.text.VerticalAlignment = Sw.VerticalAlignment.Center;
            this.text.Text = "Ok";

            contents.Orientation = Wpf.Orientation.Horizontal;
            contents.Children.Add(text);

            this.ctrl.Content = contents;
            this.ctrl.MinWidth = 90;
            this.control = this.ctrl;
            base.SetInputs();
        }

        public override void Update(Gk.GH_Component component)
        {
            ctrl.PreviewMouseLeftButtonDown -= (o, e) => {
                status = true;
                component.ExpireSolution(true);
            };
            ctrl.PreviewMouseLeftButtonDown += (o, e) => {
                status = true;
                component.ExpireSolution(true);
            };
            ctrl.PreviewMouseLeftButtonUp -= (o, e) => {
                status = false;
                component.ExpireSolution(true);
            };
            ctrl.PreviewMouseLeftButtonUp += (o, e) => {
                status = false;
                component.ExpireSolution(true);
            };
        }

        public override List<object> GetValues()
        {
            return new List<object> { this.status };
        }

        public override string ToString()
        {
            return "Ui Button | "+this.Name;
        }

        #endregion

    }
}