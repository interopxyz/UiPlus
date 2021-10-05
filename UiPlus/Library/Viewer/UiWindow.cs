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

namespace UiPlus.Viewer
{
    public class UiWindow
    {

        #region Members

        public enum Arrangments {None,Topmost,Rhino,Grasshopper }
        public Arrangments Arrangment = Arrangments.None;
        protected UiViewer viewer = new UiViewer();
        public StackPanel Container;
        public ScrollViewer ScrollFrame;

        protected List<UiElement> elements = new List<UiElement>();

        #endregion

        #region Constructors

        public UiWindow()
        {

        }

        public UiWindow(List<UiElement> elements)
        {
            foreach (UiElement element in elements)
            {
                this.elements.Add(element);
            }
        }

        #endregion

        #region Properties



        #endregion

        #region Methods

        public void Launch()
        {
            viewer = new UiViewer();
            Container = new StackPanel();
            ScrollFrame = new ScrollViewer();

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

            Container.Orientation = Orientation.Vertical;
            Container.Margin = new Thickness(5);

            viewer.Closing -= (o, e) => { Container.Children.Clear(); };
            viewer.Closing += (o, e) => { Container.Children.Clear(); };

            viewer.SizeToContent = SizeToContent.WidthAndHeight;
            ScrollFrame.Content = Container;
            viewer.Content = ScrollFrame;

            foreach (UiElement element in elements)
            {
                AddElement(element);
            }

            viewer.OpenWindow();
        }

        public void AddElement(UiElement uiElement)
        {
            uiElement.DetachParent();
            uiElement.SetElement();
            Container.Children.Add(uiElement.Container);
        }

        #endregion

    }
}
