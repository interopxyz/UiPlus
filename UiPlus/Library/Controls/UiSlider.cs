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
    public class UiSlider : UiElement
    {

        #region Members



        #endregion

        #region Constructors

        public UiSlider() : base()
        {
            SetInputs();
        }

        public UiSlider(UiSlider uiControl) : base(uiControl)
        {
            this.control = uiControl.Control;
        }

        #endregion

        #region Properties

        public virtual double CurrentValue
        {
            get { return ((Wpf.Slider)control).Value; }
            set { ((Wpf.Slider)control).Value = value; }
        }

        public virtual Interval Domain
        {
            get { return new Interval(((Wpf.Slider)control).Minimum, ((Wpf.Slider)control).Maximum); }
            set 
            {
                ((Wpf.Slider)control).Minimum = value.Min;
                ((Wpf.Slider)control).Maximum = value.Max;
            }
        }

        public virtual double Increment
        {
            get { return ((Wpf.Slider)control).TickFrequency; }
            set
            {
                ((Wpf.Slider)control).TickFrequency = value;
                if (value > 0)
                {
                    ((Wpf.Slider)control).IsSnapToTickEnabled = true; 
                } 
                else 
                {
                    ((Wpf.Slider)control).IsSnapToTickEnabled = false; 
                }
            }
        }

        #endregion

        #region Methods


        #endregion

        #region Overrides

        public override void SetInputs()
        {
            this.control = new Wpf.Slider();

            Inputs.Add(new UiInput(UiInput.InputTypes.Param_Number, "Value", "V", "The current control value.", Grasshopper.Kernel.GH_ParamAccess.item));
            Inputs.Add(new UiInput(UiInput.InputTypes.Param_Interval, "Bounds", "B", "The bounds of the control.", Grasshopper.Kernel.GH_ParamAccess.item));
            Inputs.Add(new UiInput(UiInput.InputTypes.Param_Number, "Increment", "I", "The control interval.", Grasshopper.Kernel.GH_ParamAccess.item));
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