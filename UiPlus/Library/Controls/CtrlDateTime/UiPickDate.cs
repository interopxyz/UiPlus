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
    public class UiPickDate : UiElement
    {

        #region Members

        Wpf.DatePicker ctrl = new Wpf.DatePicker();

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
            get { return (DateTime)ctrl.SelectedDate; }
            set
            {
                ctrl.SelectedDate = value;
                ctrl.DisplayDate = value;
            }
        }

        public virtual bool Long
        {
            get { return ctrl.SelectedDateFormat == Wpf.DatePickerFormat.Long; }
            set
            {
                if (value)
                {
                    ctrl.SelectedDateFormat = Wpf.DatePickerFormat.Long;
                }
                else
                {
                    ctrl.SelectedDateFormat = Wpf.DatePickerFormat.Short;
                }
            }
        }

        #endregion

        #region Methods


        #endregion

        #region Overrides

        public override void SetInputs()
        {
            ElementType = ElementTypes.Border;

            this.control = this.ctrl;
            this.border.Child = this.control;
            Mat.ColorZoneAssist.SetMode(ctrl, Mat.ColorZoneMode.Inverted);

            base.SetInputs();
        }

        public override void SetPrimaryColors(Sd.Color color)
        {
            base.SetPrimaryColors(color);
            ctrl.Foreground = color.ToSolidColorBrush();
            Mat.TextFieldAssist.SetUnderlineBrush(ctrl, color.ToSolidColorBrush());

            Mat.CalendarAssist.SetSelectionColor(ctrl, color.ToSolidColorBrush());
            Mat.CalendarAssist.SetHeaderBackground(ctrl, color.ToSolidColorBrush());
        }

        public override void SetAccentColors(Sd.Color color)
        {
            base.SetAccentColors(color);
            
            //Mat.CalendarAssist.SetSelectionForegroundColor(calendar, color.ToSolidColorBrush());
            //Mat.CalendarAssist.SetHeaderForeground(calendar, color.ToSolidColorBrush());
        }

        public override void Update(Gk.GH_Component component)
        {
            ctrl.SelectedDateChanged -= (o, e) => { component.ExpireSolution(true); };
            ctrl.SelectedDateChanged += (o, e) => { component.ExpireSolution(true); };
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