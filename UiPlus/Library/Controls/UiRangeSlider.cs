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
    public class UiRangeSlider : UiElement
    {

        #region Members



        #endregion

        #region Constructors

        public UiRangeSlider() : base()
        {
            SetInputs();
        }

        public UiRangeSlider(UiRangeSlider uiControl) : base(uiControl)
        {
            this.control = uiControl.Control;
        }

        #endregion

        #region Properties

        public virtual Interval Domain
        {
            get { return new Interval(((Mah.RangeSlider)control).Minimum, ((Mah.RangeSlider)control).Maximum); }
            set
            {
                ((Mah.RangeSlider)control).Minimum = value.Min;
                ((Mah.RangeSlider)control).Maximum = value.Max;
            }
        }

        public virtual Interval CurrentValue
        {
            get { return new Interval(((Mah.RangeSlider)control).LowerValue, ((Mah.RangeSlider)control).UpperValue); }
            set
            {
                ((Mah.RangeSlider)control).LowerValue = value.Min;
                ((Mah.RangeSlider)control).UpperValue = value.Max;
            }
        }

        public virtual double Increment
        {
            get { return ((Mah.RangeSlider)control).TickFrequency; }
            set
            {
                ((Mah.RangeSlider)control).TickFrequency = value;
                if (value > 0)
                {
                    ((Mah.RangeSlider)control).IsSnapToTickEnabled = true;
                }
                else
                {
                    ((Mah.RangeSlider)control).IsSnapToTickEnabled = false;
                }
            }
        }

        #endregion

        #region Methods


        #endregion

        #region Overrides

        public override void SetInputs()
        {
            this.control = new Mah.RangeSlider();

            Inputs.Add(new UiInput(UiInput.InputTypes.Param_Interval, "Domain", "D", "The bounds of the control.", Grasshopper.Kernel.GH_ParamAccess.item));
            Inputs.Add(new UiInput(UiInput.InputTypes.Param_Interval, "Selection", "S", "The selection bounds.", Grasshopper.Kernel.GH_ParamAccess.item));
            Inputs.Add(new UiInput(UiInput.InputTypes.Param_Number, "Interval", "I", "The control interval.", Grasshopper.Kernel.GH_ParamAccess.item));
        }

        public override List<object> GetValues()
        {
            return new List<object> { this.CurrentValue };
        }

        public override string ToString()
        {
            return "Ui Slider | " + this.Name;
        }

        #endregion

    }
}