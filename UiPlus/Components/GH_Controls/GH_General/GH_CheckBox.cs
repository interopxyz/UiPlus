using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

using UiPlus.Elements;

namespace UiPlus.Components
{
    public class GH_CheckBox : GH_EditBase
    {
        /// <summary>
        /// Initializes a new instance of the GH_CheckBox class.
        /// </summary>
        public GH_CheckBox()
          : base("Ui Check Box", "Check Box",
              "Click to toggle between true and false",
              "Ui", "Control")
        {
        }

        /// <summary>
        /// Set Exposure level for the component.
        /// </summary>
        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.primary; }
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            base.RegisterInputParams(pManager);
            pManager.AddTextParameter("Label", "L", "The button label", GH_ParamAccess.item, "");
            pManager[1].Optional = true;

            pManager.AddBooleanParameter("State", "S", "The control's boolean status.", GH_ParamAccess.item, false);
            pManager[2].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            base.RegisterOutputParams(pManager);
            pManager[0].Description = "Ui Element | Check Box Control";
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            UiCheckBox control = new UiCheckBox();
            if (DA.GetData(0, ref control)) Message = "Update";

            string label = "";
            DA.GetData(1, ref label);

            bool state = false;
            DA.GetData(2, ref state);

            control.Label = label;
            control.State = state;

            DA.SetData(0, control);
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
                return Properties.Resources.UiPlus_Elements_CheckBox_01;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("1eb6e339-6531-41c3-8b1d-b20cbee629ba"); }
        }
    }
}