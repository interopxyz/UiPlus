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
    public class UiTextBox: UiElement
    {

        #region Members



        #endregion

        #region Constructors

        public UiTextBox() : base()
        {
            SetInputs();
        }

        public UiTextBox(UiTextBox uiControl) : base(uiControl)
        {
            this.control = uiControl.Control;
        }

        #endregion

        #region Properties

        public virtual string Content
        {
            get { return ((Wpf.TextBox)control).Text; }
            set { ((Wpf.TextBox)control).Text = value; }
        }

        public virtual bool Wrap
        {
            get { return ((Wpf.TextBox)control).TextWrapping == System.Windows.TextWrapping.Wrap; }
            set
            {
                if (value)
                {
                    ((Wpf.TextBox)control).TextWrapping = System.Windows.TextWrapping.Wrap;
                }
                else
                {
                    ((Wpf.TextBox)control).TextWrapping = System.Windows.TextWrapping.NoWrap;
                }
            }
        }

        public virtual double Width
        {
            get { return ((Wpf.TextBox)control).Width; }
            set { ((Wpf.TextBox)control).Width= value; }
        }

        #endregion

        #region Methods


        #endregion

        #region Overrides

        public override void SetInputs()
        {
            this.control = new Wpf.TextBox();

            Inputs.Add(new UiInput(UiInput.InputTypes.Param_String, "Text", "T", "The control text.", Grasshopper.Kernel.GH_ParamAccess.item));
            Inputs.Add(new UiInput(UiInput.InputTypes.Param_Boolean, "Wrap", "W", "If true the text will wrap.", Grasshopper.Kernel.GH_ParamAccess.item));
            Inputs.Add(new UiInput(UiInput.InputTypes.Param_Number, "Width", "B", "An optional limiting width for the text panel.", Grasshopper.Kernel.GH_ParamAccess.item));
        }

        public override List<object> GetValues()
        {
            return new List<object> { this.Content };
        }

        public override string ToString()
        {
            return "Ui Text Box | " + this.Name;
        }

        #endregion

    }
}