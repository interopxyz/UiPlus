using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf = System.Windows.Controls;

namespace UiPlus.Elements
{
    public class UiButton : UiElement
    {

        #region Members



        #endregion

        #region Constructors

        public UiButton() : base()
        {
            SetInputs();
        }

        public UiButton(UiButton uiButton) : base(uiButton)
        {
            this.control = uiButton.Control;
        }

        #endregion

        #region Properties

        public virtual string Text
        {
            get { return ((Wpf.Button)control).Content.ToString(); }
            set
            { ((Wpf.Button)control).Content = value; }
        }

        #endregion

        #region Methods

        public override void SetInputs()
        {
            this.control = new Wpf.Button();

            Inputs.Add(new UiInput(UiInput.InputTypes.Param_String, "Label", "L", "The button label.", Grasshopper.Kernel.GH_ParamAccess.item));
        }

        #endregion

    }
}