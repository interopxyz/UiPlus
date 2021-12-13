using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Rhino.Geometry;

using Sd = System.Drawing;
using Sw = System.Windows;
using Sm = System.Windows.Media;

using Wpf = System.Windows.Controls;
using Mat = MaterialDesignThemes.Wpf;
using Mah = MahApps.Metro.Controls;
using Xcd = Xceed.Wpf.Toolkit;

namespace UiPlus.Elements
{
    public class UiLayoutPlacement: UiElement
    {

        #region Members

        protected List<UiElement> elements = new List<UiElement>();
        protected List<Point3d> locations = new List<Point3d>();

        protected double width = 600;
        protected double height = 600;

        #endregion

        #region Constructors

        public UiLayoutPlacement() : base()
        {
            SetInputs();
        }

        public UiLayoutPlacement(UiLayoutPlacement uiControl) : base(uiControl)
        {
            this.control = uiControl.Control;
        }

        #endregion

        #region Properties

        public virtual double Width
        {
            get { return width; }
            set { 
                width = value;
                SetInputs();
            }
        }

        public virtual double Height
        {
            get { return height; }
            set
            {
                height = value;
                SetInputs();
            }
        }

        public virtual List<UiElement> Elements
        {
            get { return elements; }
        }

        public virtual List<Point3d> Locations
        {
            get { return locations; }
        }

        public virtual Dictionary<UiElement, Point3d> LocationElements
        {
            get
            {
                Dictionary<UiElement, Point3d> collection = new Dictionary<UiElement, Point3d>();
                for (int i = 0; i < elements.Count; i++)
                {
                    collection.Add(elements[i], locations[i]);
                }
                return collection;
            }
            set
            {
                foreach (KeyValuePair<UiElement, Point3d> location in value)
                {
                    locations.Add(location.Value);
                    elements.Add(location.Key);
                }
                SetInputs();
            }
        }

        #endregion

        #region Methods


        #endregion

        #region Overrides

        public override void SetInputs()
        {
            this.ElementType = ElementTypes.Layout;
            Wpf.Canvas ctrl = new Wpf.Canvas();

            ctrl.HorizontalAlignment = Sw.HorizontalAlignment.Left;
            ctrl.Width = width;
            ctrl.Height = height;

            for (int i = 0; i < elements.Count; i++)
            {
                elements[i].DetachParent();
                elements[i].SetElement();
                Wpf.Canvas.SetLeft(elements[i].Container, locations[i].X);
                Wpf.Canvas.SetTop(elements[i].Container, locations[i].Y);
                ctrl.Children.Add(elements[i].Container);
            }


            this.layout = ctrl;

        }

        public override List<object> GetValues()
        {
            return new List<object> { null };
        }

        public override string ToString()
        {
            return "Ui Layout Placement | " + this.Name;
        }

        #endregion

    }
}