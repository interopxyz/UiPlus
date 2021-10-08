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
    public class UiRadio : UiElement
    {

        #region Members



        #endregion

        #region Constructors

        public UiRadio() : base()
        {
            SetInputs();
        }

        public UiRadio(UiRadio uiControl) : base(uiControl)
        {
            this.control = uiControl.Control;
        }

        #endregion

        #region Properties

        public virtual string Label
        {
            get { return ((Wpf.RadioButton)control).Content.ToString(); }
            set { ((Wpf.RadioButton)control).Content = value; }
        }

        public virtual string Group
        {
            get { return ((Wpf.RadioButton)control).GroupName; }
            set
            { ((Wpf.RadioButton)control).GroupName = value; }
        }

        public virtual bool State
        {
            get { return (bool)((Wpf.RadioButton)control).IsChecked; }
            set {((Wpf.RadioButton)control).IsChecked = value; }
        }

        #endregion

        #region Methods


        #endregion

        #region Overrides

        public override void SetInputs()
        {
            this.control = new Wpf.RadioButton();

            Inputs.Add(new UiInput(UiInput.InputTypes.Param_String, "Label", "L", "The control label.", Grasshopper.Kernel.GH_ParamAccess.item));
            Inputs.Add(new UiInput(UiInput.InputTypes.Param_Boolean, "State", "S", "The control's boolean status.", Grasshopper.Kernel.GH_ParamAccess.item));
            Inputs.Add(new UiInput(UiInput.InputTypes.Param_String, "Group", "G", "The Radio group.", Grasshopper.Kernel.GH_ParamAccess.item));
        }

        public override List<object> GetValues()
        {
            return new List<object> { this.State };
        }

        public override string ToString()
        {
            return "Ui Radio | " + this.Name;
        }

        #endregion

    }
}