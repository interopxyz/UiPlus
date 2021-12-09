using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Wpf = System.Windows.Forms;

using UiPlus.Elements;

using System.Windows.Controls;
using System.Windows;
using System.Windows.Interop;

using Mat = MaterialDesignThemes.Wpf;

using Sd = System.Drawing;
using Wm = System.Windows.Media;

namespace UiPlus.Elements
{
    public class UiWindow: UiElement
    {

        #region Members

        public enum ScrollVisibily { Auto=1, Hidden=2, Visible = 3 }
        protected ScrollVisibily scroll = ScrollVisibily.Auto;

        public enum Arrangments {None,Topmost,Rhino,Grasshopper }
        public Arrangments Arrangment = Arrangments.None;

        public StackPanel Stack;
        public ScrollViewer ScrollFrame = new ScrollViewer();
        public Mat.ColorZone Container = new Mat.ColorZone();
        protected List<UiElement> elements = new List<UiElement>();

        #endregion

        #region Constructors

        public UiWindow()
        {
            this.ElementType = ElementTypes.Window;
            viewer = new UiViewer();
        }

        public UiWindow(List<UiElement> elements)
        {
            this.ElementType = ElementTypes.Window;
            viewer = new UiViewer();
            foreach (UiElement element in elements)
            {
                this.elements.Add(element);
            }
        }

        #endregion

        #region Properties

        public virtual ScrollVisibily ScrollVisible
        {
            get { return scroll; }
            set 
            { 
                scroll = value;
                ScrollFrame.VerticalScrollBarVisibility = (ScrollBarVisibility)scroll;
            }
        }
        #endregion

        #region Methods

        public void Launch()
        {
            Stack = new StackPanel();
            WindowInteropHelper H = new WindowInteropHelper(viewer);

            switch (Arrangment)
            {
                default:
                    viewer.Topmost = false;
                    break;
                case Arrangments.Topmost:
                    viewer.Topmost = true;
                    break;
                case Arrangments.Rhino:
                    H.Owner = Rhino.RhinoApp.MainWindowHandle();
                    break;
                case Arrangments.Grasshopper:
                    H.Owner = Grasshopper.Instances.DocumentEditor.Handle;
                    break;
            }

            Stack.Orientation = Orientation.Vertical;
            Stack.Margin = new Thickness(5);
            Stack.Background = Wm.Brushes.Transparent;

            viewer.Closing -= (o, e) => { Stack.Children.Clear(); };
            viewer.Closing += (o, e) => { Stack.Children.Clear(); };

            viewer.SizeToContent = SizeToContent.Height;
            viewer.Width = 502;
            Container.Content = Stack;

            ScrollFrame.Content = Container;
            viewer.Content = ScrollFrame;

            foreach (UiElement element in elements)
            {
                AddElement(element);
            }

            SetStrokeColor(Constants.DefaultDarkColor());
            SetPrimaryColors(Constants.MaterialColor());
            viewer.OpenWindow();
        }

        public void AddElement(UiElement uiElement)
        {
            uiElement.DetachParent();
            uiElement.SetElement();
            Stack.Children.Add(uiElement.Container);
        }

        #endregion

    }
}
