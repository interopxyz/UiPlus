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
    public class UiPickTime : UiElement
    {

        #region Members



        #endregion

        #region Constructors

        public UiPickTime() : base()
        {
            SetInputs();
        }

        public UiPickTime(UiPickTime uiControl) : base(uiControl)
        {
            this.control = uiControl.Control;
        }

        #endregion

        #region Properties

        public virtual DateTime Time
        {
            get { return ((Mat.TimePicker)control).SelectedTime.Value; }
            set 
            {
                ((Mat.TimePicker)control).SelectedTime = value;
            }
        }

        public virtual bool Long
        {
            get { return ((Mat.TimePicker)control).SelectedTimeFormat == Wpf.DatePickerFormat.Long; }
            set 
            {
                if (value)
                {
                    ((Mat.TimePicker)control).SelectedTimeFormat = Wpf.DatePickerFormat.Long;
                }
                else
                {
                    ((Mat.TimePicker)control).SelectedTimeFormat = Wpf.DatePickerFormat.Short;
                }
            }
        }

        #endregion

        #region Methods


        #endregion

        #region Overrides

        public override void SetInputs()
        {
            this.control = new Mat.TimePicker();

            Inputs.Add(new UiInput(UiInput.InputTypes.Param_Time, "Time", "T", "The control time.", Grasshopper.Kernel.GH_ParamAccess.item));
            Inputs.Add(new UiInput(UiInput.InputTypes.Param_Boolean, "Long", "L", "The control time long format.", Grasshopper.Kernel.GH_ParamAccess.item));
        }

        public override List<object> GetValues()
        {
            return new List<object> { this.Time };
        }

        public override string ToString()
        {
            return "Ui Time Picker | " + this.Name;
        }

        #endregion

    }
}