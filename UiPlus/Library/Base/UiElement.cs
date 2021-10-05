using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Wpc = System.Windows.Controls;
using MaterialDesignThemes.Wpf;
namespace UiPlus.Elements
{
    public class UiElement
    {
        #region Members
        //public enum ElementTypes {None,Button,Toggle };
        //protected ElementTypes elementType = ElementTypes.None;

        protected string name = string.Empty;
        protected string id = string.Empty;
        public ColorZone Container = new ColorZone();
        public Wpc.Panel Layout = null;
        protected Wpc.Control control = null;
        public List<UiInput> Inputs = new List<UiInput>();

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

            this.Inputs = uiElement.Inputs;
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

        public virtual Wpc.Control Control
        {
            get { return control; }
        }

        #endregion

        #region Methods

        public void SetElement()
        {
            Container.Content = control;
        }

        public virtual void SetInputs()
        {

        }

        public void DetachParent()
        {
            if (Container.Parent != null)
            {
                Wpc.Panel ParentLayout = Container.Parent as Wpc.Panel;
                ParentLayout.Children.Remove(Container);
            }
        }

        public string GetElementType()
        {
            return control.GetType().Name.ToString();
        }

        #endregion
    }
}