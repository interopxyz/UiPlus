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
    public class UiPickTime : UiElement
    {

        #region Members

        Mat.TimePicker ctrl = new Mat.TimePicker();

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
            get { return ctrl.SelectedTime.Value; }
            set 
            {
                ctrl.SelectedTime = value;
            }
        }

        public virtual bool Long
        {
            get { return ctrl.SelectedTimeFormat == Wpf.DatePickerFormat.Long; }
            set 
            {
                if (value)
                {
                    ctrl.SelectedTimeFormat = Wpf.DatePickerFormat.Long;
                }
                else
                {
                    ctrl.SelectedTimeFormat = Wpf.DatePickerFormat.Short;
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
            base.SetInputs();
        }

        public override void SetPrimaryColors(Sd.Color color)
        {
            base.SetPrimaryColors(color);
            ctrl.Foreground = color.ToSolidColorBrush();
            Mat.TextFieldAssist.SetUnderlineBrush(ctrl, color.ToSolidColorBrush());

        }

        public override void Update(Gk.GH_Component component)
        {
           ctrl.MouseUp -= (o, e) => { component.ExpireSolution(true); };
           ctrl.MouseUp += (o, e) => { component.ExpireSolution(true); };
           ctrl.LayoutUpdated -= (o, e) => { component.ExpireSolution(true); };
            ctrl.LayoutUpdated += (o, e) => { component.ExpireSolution(true); };
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