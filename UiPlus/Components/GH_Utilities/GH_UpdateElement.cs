using Grasshopper.Kernel;
using Grasshopper.Kernel.Parameters;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using UiPlus.Elements;

namespace UiPlus.Components
{
    public class GH_UpdateElement : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the GH_UpdateElement class.
        /// </summary>
        public GH_UpdateElement()
          : base("UI Update", "Update",
              "Description",
              "Ui", "Elements")
        {
        }
        
        /// <summary>
        /// Set Exposure level for the component.
        /// </summary>
        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.septenary; }
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Element", "E", "A Ui Control Element", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            UiElement uiElement = null;
            if (!DA.GetData(0, ref uiElement)) return;

            SetInputs(uiElement.Inputs);
            string elementType = uiElement.GetElementType();
            switch (elementType)
            {
                case "Button":
                    UiButton uiButton = (UiButton)uiElement;
                    string text = string.Empty;
                    if(DA.GetData(1, ref text)) uiButton.Text = text;
                    break;
                case "ToggleSwitch":
                    UiToggle uiToggle = (UiToggle)uiElement;
                    bool status = false;
                    if (DA.GetData(1, ref status)) uiToggle.Status= status;
                    break;
            }
        }

        private void SetInputs(List<UiInput> inputs)
        {
            int currentCount = Params.Input.Count;
            int newCount = inputs.Count+1;
            bool changed = (currentCount != newCount);

            int k = currentCount;
            for (int i = 1; i < currentCount; i++)
            {
                int j = i - 1;
                if (Params.Input[i].GetType().Name.ToString() != inputs[j].InputType.ToString())
                {
                    k = i;
                    changed = true;
                    break;
                }
            }

            for (int i = k; i < currentCount; i++)
            {
                Params.Input[k].Sources.Clear();
                Params.Input[k].ClearData();
                Params.UnregisterInputParameter(Params.Input[k]);
            }

            Params.OnParametersChanged();

            int c = 0;
            for (int i = k; i < newCount; i++)
            {

                switch (inputs[c].InputType)
                {
                    case UiInput.InputTypes.Param_Boolean:
                        Params.RegisterInputParam(new Param_Boolean());
                        break;
                    case UiInput.InputTypes.Param_Colour:
                        Params.RegisterInputParam(new Param_Colour());
                        break;
                    case UiInput.InputTypes.Param_GenericObject:
                        Params.RegisterInputParam(new Param_GenericObject());
                        break;
                    case UiInput.InputTypes.Param_Integer:
                        Params.RegisterInputParam(new Param_Integer());
                        break;
                    case UiInput.InputTypes.Param_Number:
                        Params.RegisterInputParam(new Param_Number());
                        break;
                    case UiInput.InputTypes.Param_String:
                        Params.RegisterInputParam(new Param_String());
                        break;
                }
                c++;
            }

            for(int i = 1; i < newCount; i++)
            {
                int j = i - 1;
                Params.Input[i].Name = inputs[j].Name;
                Params.Input[i].NickName= inputs[j].NickName;
                Params.Input[i].Description= inputs[j].Description;
                Params.Input[i].Access = inputs[j].Access;
                Params.Input[i].Optional = true;
            }

            Params.OnParametersChanged();
            if(changed) this.ExpireSolution(true);

        }

        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                //You can add image files to your project resources and access them like this:
                // return Resources.IconForThisComponent;
                return null;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("d6a0b1eb-d2a6-4afa-be06-85a37bc120ed"); }
        }
    }
}