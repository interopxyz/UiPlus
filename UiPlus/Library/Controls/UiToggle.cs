using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf = System.Windows.Controls;

using Mc = MahApps.Metro.Controls;

namespace UiPlus.Elements
{
    public class UiToggle : UiElement
    {

        #region Members



        #endregion

        #region Constructors

        public UiToggle() : base()
        {
            this.control = new Mc.ToggleSwitch();
            SetInputs();
        }

        public UiToggle(UiToggle uiToggle) : base(uiToggle)
        {
            this.control = uiToggle.Control;
        }

        #endregion

        #region Properties

        public virtual bool Status
        {
            get { return ((Mc.ToggleSwitch)control).IsOn; }
            set { ((Mc.ToggleSwitch)control).IsOn = value; }
        }

        #endregion

        #region Methods

        public override void SetInputs()
        {
            Inputs.Add(new UiInput(UiInput.InputTypes.Param_Boolean, "State", "S", "The toggle state.", Grasshopper.Kernel.GH_ParamAccess.item));
        }

        #endregion
    }
}