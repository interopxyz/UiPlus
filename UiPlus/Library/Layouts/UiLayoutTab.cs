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
    public class UiLayoutTab : UiElement
    {

        #region Members

        Dictionary<string, List<UiElement>> elementGroups = new Dictionary<string, List<UiElement>>();

        #endregion

        #region Constructors

        public UiLayoutTab() : base()
        {
            SetInputs();
        }

        public UiLayoutTab(UiLayoutTab uiControl) : base(uiControl)
        {
            this.control = uiControl.Control;
        }

        #endregion

        #region Properties

        public virtual List<UiElement> Elements
        {
            get
            {
                List<UiElement> elements = new List<UiElement>();
                foreach (KeyValuePair<string, List<UiElement>> elementGroup in elementGroups)
                {
                    foreach(UiElement element in elementGroup.Value)
                    {
                        elements.Add(element);
                    }
                }
                return elements; 
            }
        }

        public virtual Dictionary<string, List<UiElement>> ElementGroups
        {
            get { return elementGroups; }
            set
            {
                elementGroups = value;
                SetInputs();
            }
        }

        #endregion

        #region Methods


        #endregion

        #region Overrides

        public override void SetInputs()
        {

            Wpf.TabControl ctrl = new Wpf.TabControl();

            foreach(KeyValuePair<string,List<UiElement>> elementGroup in elementGroups)
            {
                Wpf.TabItem tabItem = new Wpf.TabItem();

                Wpf.StackPanel stack = new Wpf.StackPanel();
                stack.VerticalAlignment = Sw.VerticalAlignment.Stretch;
                stack.HorizontalAlignment = Sw.HorizontalAlignment.Stretch;

                foreach(UiElement uiElement in elementGroup.Value)
                {
                    uiElement.DetachParent();
                    uiElement.SetElement();
                    stack.Children.Add(uiElement.Container);
                }

                //Wpf.TextBlock header = new Wpf.TextBlock();
                //header.Text = elementGroup.Key;
                //header.FontSize = 10;
                //tabItem.Header = header;

                tabItem.Header = elementGroup.Key ;

                Mat.NavigationRailAssist.SetShowSelectionBackground(tabItem, true);

                tabItem.Content = stack;

                ctrl.Items.Add(tabItem);
            }

            
            this.control = ctrl;

        }

        public override List<object> GetValues()
        {
            return new List<object> { null };
        }

        public override string ToString()
        {
            return "Ui Layout Tab | " + this.Name;
        }

        #endregion

    }
}