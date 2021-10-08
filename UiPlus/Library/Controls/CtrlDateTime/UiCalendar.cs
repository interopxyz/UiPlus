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
    public class UiCalendar: UiElement
    {

        #region Members
        public enum Modes { Month,Year,Decade}

        #endregion

        #region Constructors

        public UiCalendar() : base()
        {
            SetInputs();
        }

        public UiCalendar(UiCalendar uiControl) : base(uiControl)
        {
            this.control = uiControl.Control;
        }

        #endregion

        #region Properties

        public virtual List<DateTime> Times
        {
            get { return ((Wpf.Calendar)control).SelectedDates.ToList(); }
        }

        public virtual DateTime Time
        {
            get { return (DateTime)((Wpf.Calendar)control).SelectedDate; }
            set { ((Wpf.Calendar)control).SelectedDate = value; }
        }

        public virtual bool SelectSingle
        {
            get { return ((Wpf.Calendar)control).SelectionMode == Wpf.CalendarSelectionMode.SingleRange; }
            set
            {
                if(value)
                {
                    ((Wpf.Calendar)control).SelectionMode = Wpf.CalendarSelectionMode.SingleDate;
                }
                else
                {
                    ((Wpf.Calendar)control).SelectionMode = Wpf.CalendarSelectionMode.SingleRange;
                }
            }
        }

        public virtual Modes DisplayMode
        {
            get { return (Modes)((Wpf.Calendar)control).DisplayMode; }
            set { ((Wpf.Calendar)control).DisplayMode = (Wpf.CalendarMode)value; }
        }

        #endregion

        #region Methods


        #endregion

        #region Overrides

        public override void SetInputs()
        {
            Wpf.Calendar ctrl = new Wpf.Calendar();
            ctrl.IsTodayHighlighted = false;

            this.control = ctrl;

            Inputs.Add(new UiInput(UiInput.InputTypes.Param_Time, "DateTime", "D", "The control time.", Grasshopper.Kernel.GH_ParamAccess.item));
            Inputs.Add(new UiInput(UiInput.InputTypes.Param_Boolean, "Single", "S", "If true, only a single date can be selected", Grasshopper.Kernel.GH_ParamAccess.item));
            Inputs.Add(new UiInput(UiInput.InputTypes.Param_Integer, "Mode", "M", "Set calendar mode", Grasshopper.Kernel.GH_ParamAccess.item));
        }

        public override List<object> GetValues()
        {
            List<object> objects = new List<object>();
            foreach(DateTime datetime in this.Times)
            {
                objects.Add(datetime);
            }
            return objects;
        }

        public override string ToString()
        {
            return "Ui Calendar | " + this.Name;
        }

        #endregion

    }
}