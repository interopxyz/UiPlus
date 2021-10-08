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
    public class UiPickDate : UiElement
    {

        #region Members



        #endregion

        #region Constructors

        public UiPickDate() : base()
        {
            SetInputs();
        }

        public UiPickDate(UiPickDate uiControl) : base(uiControl)
        {
            this.control = uiControl.Control;
        }

        #endregion

        #region Properties

        public virtual DateTime Date
        {
            get { return (DateTime)((Wpf.DatePicker)control).SelectedDate; }
            set
            {
                ((Wpf.DatePicker)control).SelectedDate = value;
                ((Wpf.DatePicker)control).DisplayDate = value;
            }
        }

        public virtual bool Long
        {
            get { return ((Wpf.DatePicker)control).SelectedDateFormat == Wpf.DatePickerFormat.Long; }
            set
            {
                if (value)
                {
                    ((Wpf.DatePicker)control).SelectedDateFormat = Wpf.DatePickerFormat.Long;
                }
                else
                {
                    ((Wpf.DatePicker)control).SelectedDateFormat = Wpf.DatePickerFormat.Short;
                }
            }
        }

        #endregion

        #region Methods


        #endregion

        #region Overrides

        public override void SetInputs()
        {
            this.control = new Wpf.DatePicker();

            Inputs.Add(new UiInput(UiInput.InputTypes.Param_Time, "Date", "D", "The control date.", Grasshopper.Kernel.GH_ParamAccess.item));
            Inputs.Add(new UiInput(UiInput.InputTypes.Param_Boolean, "Long", "L", "The control date long format.", Grasshopper.Kernel.GH_ParamAccess.item));
        }

        public override List<object> GetValues()
        {
            return new List<object> { this.Date };
        }

        public override string ToString()
        {
            return "Ui Date Picker | " + this.Name;
        }

        #endregion

    }
}