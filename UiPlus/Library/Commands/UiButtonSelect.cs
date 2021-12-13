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
    public class UiButtonSelect : UiElement
    {

        #region Members

        Wpf.Button button = new Wpf.Button();

        public enum SelectionTypes { Any = 0, Point = 1, Curve = 4, Brep = 16, Mesh = 32, SubD = 262144};

        protected SelectionTypes selectionType = SelectionTypes.Any;

        List<object> guids = new List<object>();

        #endregion

        #region Constructors

        public UiButtonSelect() : base()
        {
            SetInputs();
        }

        public UiButtonSelect(UiButtonSelect uiControl) : base(uiControl)
        {
            this.control = uiControl.Control;
        }

        #endregion

        #region Properties

        public virtual string Label
        {
            get { return button.Content.ToString(); }
            set
            { button.Content = value; }
        }

        public virtual SelectionTypes SelectionType
        {
            get { return selectionType; }
            set
            { 
                selectionType = value;
            }
        }

        #endregion

        #region Methods

        public void SelectObjects()
        {
            uint selType = (uint)selectionType;
            if (selType == 0) selType = 4294967295;

            Rhino.DocObjects.ObjRef[] references = null;

            Rhino.Input.RhinoGet.GetMultipleObjects("Select "+ selectionType.ToString() +" objects", true, (Rhino.DocObjects.ObjectType)selType, out references);

            guids = new List<object>();
            if (references != null)
            {
                foreach (Rhino.DocObjects.ObjRef reference in references)
                {
                    guids.Add(reference.ObjectId);
                }
                OnSelection(EventArgs.Empty);
            }
        }

        #endregion

        #region Events
        protected virtual void OnSelection(EventArgs e)
        {
            Selected?.Invoke(this, e);
        }

        public event EventHandler Selected;

        #endregion

        #region Overrides

        public override void SetInputs()
        {
            Mat.ButtonAssist.SetCornerRadius(button, new Sw.CornerRadius(Constants.DefaultRadius()));

            button.Click -= (o, e) => { SelectObjects(); };
            button.Click += (o, e) => { SelectObjects(); };

            this.button.MinWidth = 90;
            this.control = button;
            base.SetInputs(Alignment.Stretch);
        }

        public override void Update(Gk.GH_Component component)
        {
            this.Selected -= (o, e) => { component.ExpireSolution(true); };
            this.Selected += (o, e) => { component.ExpireSolution(true); };
        }

        public override List<object> GetValues()
        {
            return guids;
        }

        public override string ToString()
        {
            return "Ui Rhino Select | " + this.Name;
        }

        #endregion

    }
}