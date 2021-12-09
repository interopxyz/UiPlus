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
    public class UiDateTime : UiElement
    {

        #region Members

        Xcd.DateTimePicker ctrl = new Xcd.DateTimePicker();

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
            get { return (DateTime)ctrl.Value; }
            set { ctrl.Value = value; }
        }

        public virtual string Format
        {
            get { return ctrl.FormatString; }
            set
            {
                ctrl.Format = Xcd.DateTimeFormat.Custom;
                ctrl.FormatString = value;
            }
        }

        #endregion

        #region Methods


        #endregion

        #region Overrides

        public override void SetInputs()
        {
            ctrl.ShowButtonSpinner = false;
            ctrl.AllowSpin = true;
            ctrl.AllowTextInput = true;
            ctrl.ButtonSpinnerLocation = Xcd.Location.Left;

            this.control = ctrl;
            base.SetInputs();
        }

        public override void Update(Gk.GH_Component component)
        {
            ctrl.ValueChanged -= (o, e) => { component.ExpireSolution(true); };
            ctrl.ValueChanged += (o, e) => { component.ExpireSolution(true); };
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