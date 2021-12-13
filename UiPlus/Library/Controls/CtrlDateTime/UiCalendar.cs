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
    public class UiCalendar: UiElement
    {

        #region Members

        Wpf.Calendar ctrl = new Wpf.Calendar();

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
            get { return ctrl.SelectedDates.ToList(); }
        }

        public virtual DateTime Time
        {
            get { return (DateTime)ctrl.SelectedDate; }
            set { ctrl.SelectedDate = value; }
        }

        public virtual bool SelectSingle
        {
            get { return ctrl.SelectionMode == Wpf.CalendarSelectionMode.SingleRange; }
            set
            {
                if(value)
                {
                    ctrl.SelectionMode = Wpf.CalendarSelectionMode.SingleDate;
                }
                else
                {
                    ctrl.SelectionMode = Wpf.CalendarSelectionMode.SingleRange;
                }
            }
        }

        public virtual Modes DisplayMode
        {
            get { return (Modes)ctrl.DisplayMode; }
            set { ctrl.DisplayMode = (Wpf.CalendarMode)value; }
        }

        #endregion

        #region Methods


        #endregion

        #region Overrides

        public override void SetInputs()
        {
            ctrl.IsTodayHighlighted = false;
            Mat.ColorZoneAssist.SetMode(ctrl, Mat.ColorZoneMode.Custom);
            this.Time = DateTime.Now;

            this.control = this.ctrl;
            base.SetInputs();
        }

        public override void Update(Gk.GH_Component component)
        {
            ctrl.SelectedDatesChanged -= (o, e) => { component.ExpireSolution(true); };
            ctrl.SelectedDatesChanged += (o, e) => { component.ExpireSolution(true); };
        }

        public override void SetPrimaryColors(Sd.Color color)
        {
            base.SetPrimaryColors(color);
            Mat.CalendarAssist.SetSelectionColor(ctrl, color.ToSolidColorBrush());
            Mat.CalendarAssist.SetHeaderBackground(ctrl, color.ToSolidColorBrush());
            ctrl.BorderBrush = color.ToSolidColorBrush();
            Mat.ColorZoneAssist.SetForeground(ctrl, color.ToSolidColorBrush());
        }

        public override void SetAccentColors(Sd.Color color)
        {
            base.SetAccentColors(color);
            Mat.CalendarAssist.SetSelectionForegroundColor(ctrl, color.ToSolidColorBrush());
            Mat.CalendarAssist.SetHeaderForeground(ctrl, color.ToSolidColorBrush());
            Mat.ColorZoneAssist.SetBackground(ctrl, color.ToSolidColorBrush());
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