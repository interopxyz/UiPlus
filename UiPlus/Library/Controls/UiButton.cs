﻿using System;
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
    public class UiButton : UiElement
    {

        #region Members



        #endregion

        #region Constructors

        public UiButton() : base()
        {
            SetInputs();
        }

        public UiButton(UiButton uiControl) : base(uiControl)
        {
            this.control = uiControl.Control;
        }

        #endregion

        #region Properties

        public virtual string Label
        {
            get { return ((Wpf.Button)control).Content.ToString(); }
            set
            { ((Wpf.Button)control).Content = value; }
        }

        public virtual bool State
        {
            get { return ((Wpf.Button)control).IsPressed; }
        }

        #endregion

        #region Methods


        #endregion

        #region Overrides

        public override void SetInputs()
        {
            this.control = new Wpf.Button();

            Inputs.Add(new UiInput(UiInput.InputTypes.Param_String, "Label", "L", "The control label.", Grasshopper.Kernel.GH_ParamAccess.item));
        }

        public override List<object> GetValues()
        {
            return new List<object> {this.State };
        }

        public override string ToString()
        {
            return "Ui Button | "+this.Name;
        }

        #endregion

    }
}