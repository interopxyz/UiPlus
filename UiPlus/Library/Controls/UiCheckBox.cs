using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Rhino.Geometry;

using Wpf = System.Windows.Controls;
using Mat = MaterialDesignThemes.Wpf;
using Mah = MahApps.Metro.Controls;
using Xcd = Xceed.Wpf.Toolkit;

namespace UiPlus.Elements
{
    public class UiCheckBox : UiElement
    {

        #region Members



        #endregion

        #region Constructors

        public UiCheckBox() : base()
        {
            SetInputs();
        }

        public UiCheckBox(UiCheckBox uiControl) : base(uiControl)
        {
            this.control = uiControl.Control;
        }

        #endregion

        #region Properties

        public virtual string Label
        {
            get { return ((Wpf.CheckBox)control).Content.ToString(); }
            set { ((Wpf.CheckBox)control).Content = value; }
        }

        public virtual bool State
        {
            get { return (bool)((Wpf.CheckBox)control).IsChecked; }
            set { ((Wpf.CheckBox)control).IsChecked = value; }
        }

        #endregion

        #region Methods


        #endregion

        #region Overrides

        public override void SetInputs()
        {
            this.control = new Wpf.CheckBox();

            Inputs.Add(new UiInput(UiInput.InputTypes.Param_String, "Label", "L", "The control label.", Grasshopper.Kernel.GH_ParamAccess.item));
            Inputs.Add(new UiInput(UiInput.InputTypes.Param_Boolean, "State", "S", "The control's boolean status.", Grasshopper.Kernel.GH_ParamAccess.item));
        }

        public override List<object> GetValues()
        {
            return new List<object> { this.State };
        }

        public override string ToString()
        {
            return "Ui Check Box | " + this.Name;
        }

        #endregion

    }
}