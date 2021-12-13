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
    public class UiImage : UiElement
    {

        #region Members

        public enum FittingModes { None, Fit, Fill, Stretch, Center };

        #endregion

        #region Constructors

        public UiImage() : base()
        {
            SetInputs();
        }

        public UiImage(UiImage uiControl) : base(uiControl)
        {
            this.control = uiControl.Control;
        }

        #endregion

        #region Properties

        public virtual Sd.Bitmap Content
        {
            set { image.Source = value.ToImageSource(); }
        }

        public virtual FittingModes FittingMode
        {
            set
            {
                switch (value)
                {
                    case FittingModes.Fill:
                        image.Stretch = Wm.Stretch.UniformToFill;
                        break;
                    case FittingModes.Stretch:
                        image.Stretch = Wm.Stretch.Fill;
                        break;
                    case FittingModes.Center:
                        image.Stretch = Wm.Stretch.None;
                        image.HorizontalAlignment = Sw.HorizontalAlignment.Center;
                        image.VerticalAlignment = Sw.VerticalAlignment.Center;
                        break;
                    default:
                        image.Stretch = Wm.Stretch.None;
                        image.HorizontalAlignment = Sw.HorizontalAlignment.Left;
                        image.VerticalAlignment = Sw.VerticalAlignment.Top;
                        break;
                    case FittingModes.Fit:
                        image.Stretch = Wm.Stretch.Uniform;
                        break;
                }
            }
        }

        #endregion

        #region Methods



        #endregion

        #region Overrides

        public override void SetInputs()
        {
            this.ElementType = ElementTypes.Image;
        }

        public override List<object> GetValues()
        {
            return new List<object> { ((Wm.Imaging.BitmapSource)image.Source).ToDrawingBitmap() };
        }

        public override string ToString()
        {
            return "Ui Image | " + this.Name;
        }

        #endregion

    }
}