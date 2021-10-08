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
    public class UiDateTime : UiElement
    {

        #region Members



        #endregion

        #region Constructors

        public UiDateTime() : base()
        {
            SetInputs();
        }

        public UiDateTime(UiDateTime uiControl) : base(uiControl)
        {
            this.control = uiControl.Control;
        }

        #endregion

        #region Properties

        public virtual DateTime Time
        {
            get { return (DateTime)((Xcd.DateTimePicker)control).Value; }
            set
            {
                ((Xcd.DateTimePicker)control).Value = value;
            }
        }

        public virtual string Format
        {
            get { return ((Xcd.DateTimePicker)control).FormatString; }
            set
            {
                ((Xcd.DateTimePicker)control).Format = Xcd.DateTimeFormat.Custom;
                ((Xcd.DateTimePicker)control).FormatString = value;
            }
        }

        #endregion

        #region Methods


        #endregion

        #region Overrides

        public override void SetInputs()
        {
            Xcd.DateTimePicker ctrl = new Xcd.DateTimePicker();
            ctrl.ShowButtonSpinner = false;
            ctrl.AllowSpin = true;
            ctrl.AllowTextInput = true;
            ctrl.ButtonSpinnerLocation = Xcd.Location.Left;

            this.control = ctrl;

            Inputs.Add(new UiInput(UiInput.InputTypes.Param_Time, "Time", "T", "The control time.", Grasshopper.Kernel.GH_ParamAccess.item));
            Inputs.Add(new UiInput(UiInput.InputTypes.Param_String, "Format", "F", "The control time format.", Grasshopper.Kernel.GH_ParamAccess.item));
        }

        public override List<object> GetValues()
        {
            return new List<object> { this.Time };
        }

        public override string ToString()
        {
            return "Ui Date Time | " + this.Name;
        }

        #endregion

    }
}