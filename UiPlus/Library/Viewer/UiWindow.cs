using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Rg = Rhino.Geometry;

using Wpf = System.Windows.Forms;

using UiPlus.Elements;

using System.Windows.Controls;
using Sw = System.Windows;
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
        public Mat.ColorZone Zone = new Mat.ColorZone();
        protected List<UiElement> elements = new List<UiElement>();
        bool isClosed = false;

        #endregion

        #region Constructors

        public UiWindow()
        {
            this.ElementType = ElementTypes.Window;
        }

        public UiWindow(List<UiElement> elements)
        {
            this.ElementType = ElementTypes.Window;
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

        public virtual string Title
        {
            get { return viewer.Title; }
            set { viewer.Title = value; }
        }

        public virtual List<UiElement> Elements
        {
            set { elements = value; }
        }

        public virtual bool AreControlsVisible
        {
            set
            {
                this.viewer.ShowCloseButton = value;
                this.viewer.ShowMinButton = value;
                this.viewer.ShowMaxRestoreButton = value;
            }
        }

        public virtual bool IsTitleBarVisible
        {
            set
            {
                this.viewer.ShowTitleBar = value;
            }
        }

        public virtual Rg.Point3d Position
        {
            set 
            { 
                this.viewer.Left = value.X;
                this.viewer.Top = value.Y;
            }
        }

        #endregion

        #region Methods


        public void Launch()
        {
            if (!isClosed){viewer.Close();}
            viewer = new UiViewer();

            isClosed = false;
            if (viewer.IsActive) viewer.Close(); 
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
            Stack.Margin = new Sw.Thickness(5);

            viewer.Closing -= (o, e) => { Reset(); };
            viewer.Closing += (o, e) => { Reset(); };

            viewer.SizeToContent = Sw.SizeToContent.Height;
            viewer.Width = 502;
            viewer.AllowsTransparency = true;
            viewer.TitleCharacterCasing = CharacterCasing.Normal;
            

            //viewer.Background = Wm.Brushes.Transparent;
            Stack.Background = Wm.Brushes.Transparent;
            Zone.Background = Wm.Brushes.Transparent;
            ScrollFrame.Background = Wm.Brushes.Transparent;

            Zone.Content = Stack;
            ScrollFrame.Content = Zone;
            viewer.Content = ScrollFrame;

            foreach (UiElement element in elements)
            {
                AddElement(element);
            }

            SetStrokeColor(Constants.DefaultDarkColor());
            SetPrimaryColors(Constants.MaterialColor());
            viewer.OpenWindow();
        }

        protected void Reset()
        {
            Stack.Children.Clear();
            this.isClosed = true;
        }

        #endregion

        #region overrides

        public override string FontFamily
        {
            set
            {
                viewer.FontFamily = new Sw.Media.FontFamily(value);
            }
        }

        public override double FontSize
        {
            set
            {
                viewer.FontSize = value;
            }
        }

        public override bool IsBold
        {
            set
            {
                if (value)
                {
                    viewer.FontWeight = Sw.FontWeights.Bold;
                }
                else
                {
                    viewer.FontWeight = Sw.FontWeights.Normal;
                }
            }
        }

        public override bool IsItalic
        {
            set
            {
                if (value)
                {
                    viewer.FontStyle = Sw.FontStyles.Italic;
                }
                else
                {
                    viewer.FontStyle = Sw.FontStyles.Normal;
                }
            }
        }

        public override Justifications TextJustification
        {
            set{ viewer.TitleAlignment = GetHAlignment(value); }
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
