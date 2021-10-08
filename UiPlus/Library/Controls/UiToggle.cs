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
    public class UiToggle : UiElement
    {

        #region Members



        #endregion

        #region Constructors

        public UiToggle() : base()
        {
            SetInputs();
        }

        public UiToggle(UiToggle uiControl) : base(uiControl)
        {
            this.control = uiControl.Control;
        }

        #endregion

        #region Properties

        public virtual bool State
        {
            get { return ((Mah.ToggleSwitch)control).IsOn; }
            set { ((Mah.ToggleSwitch)control).IsOn = value; }
        }

        #endregion

        #region Methods



        #endregion

        #region Overrides

        public override void SetInputs()
        {
            this.control = new Mah.ToggleSwitch();

            Inputs.Add(new UiInput(UiInput.InputTypes.Param_Boolean, "State", "S", "The control's boolean status.", Grasshopper.Kernel.GH_ParamAccess.item));
        }

        public override List<object> GetValues()
        {
            return new List<object> { this.State };
        }

        public override string ToString()
        {
            return "Ui Toggle | " + this.Name;
        }

        #endregion
    }
}