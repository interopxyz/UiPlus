using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

using UiPlus.Elements;

namespace UiPlus.Components
{
    public class GH_Button : GH_EditBase
    {
        /// <summary>
        /// Initializes a new instance of the GH_Button class.
        /// </summary>
        public GH_Button()
          : base("Ui Button", "Button",
              "Updates the solution once when clicked",
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
            pManager.AddTextParameter("Label", "L", "The control label text", GH_ParamAccess.item, "Ok");
            pManager[1].Optional = true;
            pManager.AddTextParameter("Icon", "I", "The control icon name."+Environment.NewLine+"See https://fonts.google.com/icons for more details.", GH_ParamAccess.item);
            pManager[2].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            base.RegisterOutputParams(pManager);
            pManager[0].Description = "Ui Element | Button Control";
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            UiButton control = new UiButton();
            if(DA.GetData(0, ref control)) Message = "Update";

            string label = "Ok";
            DA.GetData(1, ref label);

            string icon = "CheckCircleOutline";
            if(DA.GetData(2,ref icon)) control.Icon = icon;
            control.Label = label;

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
                return Properties.Resources.UiPlus_Elements_Button_01;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("f28079c2-e0f3-43e0-a6b0-f23e08c646f2"); }
        }
    }
}