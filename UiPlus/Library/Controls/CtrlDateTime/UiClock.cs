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
    public class UiClock : UiElement
    {

        #region Members



        #endregion

        #region Constructors

        public UiClock() : base()
        {
            SetInputs();
        }

        public UiClock(UiClock uiControl) : base(uiControl)
        {
            this.control = uiControl.Control;
        }

        #endregion

        #region Properties

        public virtual bool Mode
        {
            get { return ((Mat.Clock)control).Is24Hours; }
            set { ((Mat.Clock)control).Is24Hours = value; }
        }

        public virtual DateTime Time
        {
            get { return ((Mat.Clock)control).Time; }
            set { ((Mat.Clock)control).Time = value; }
        }

        #endregion

        #region Methods


        #endregion

        #region Overrides

        public override void SetInputs()
        {
            this.control = new Mat.Clock();

            Inputs.Add(new UiInput(UiInput.InputTypes.Param_Time, "Time", "T", "The control time.", Grasshopper.Kernel.GH_ParamAccess.item));
            Inputs.Add(new UiInput(UiInput.InputTypes.Param_Boolean, "Mode", "M", "The control mode.", Grasshopper.Kernel.GH_ParamAccess.item));
        }

        public override List<object> GetValues()
        {
            return new List<object> { this.Time };
        }

        public override string ToString()
        {
            return "Ui Clock | " + this.Name;
        }

        #endregion

    }
}