using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

using Sd = System.Drawing;
using UiPlus.Elements;

namespace UiPlus.Components.GH_Controls.GH_Static
{
    public class GH_Spacer : GH_EditBase
    {
        /// <summary>
        /// Initializes a new instance of the GH_Spacer class.
        /// </summary>
        public GH_Spacer()
          : base("UI Spacer", "Spacer",
              "Add a horizontal or vertical spacer with a fixed width",
              "Ui", "Display")
        {
        }

        /// <summary>
        /// Set Exposure level for the component.
        /// </summary>
        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.secondary; }
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            base.RegisterInputParams(pManager);
            pManager.AddNumberParameter("Width", "W", "The controls width", GH_ParamAccess.item, 10);
            pManager[1].Optional = true;
            pManager.AddBooleanParameter("Horizontal", "H", "Is the spacer control horizontal", GH_ParamAccess.item, false);
            pManager[2].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            base.RegisterOutputParams(pManager);
            pManager[0].Description = "Ui Element | Spacer";
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            UiSpacer control = new UiSpacer();
            if (DA.GetData(0, ref control)) Message = "Update";

            double width = 10.0;
            bool hasWidth = DA.GetData(1, ref width);

            bool isHorizontal = false;
            bool hasHorizontal = DA.GetData(2, ref isHorizontal);

            if(hasWidth) control.Width = width;
            if(hasHorizontal) control.IsHorizontal = isHorizontal;

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
                return Properties.Resources.UiPlus_Elements_Spacer_01;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("c67b90ec-471f-4389-9125-e660e473141a"); }
        }
    }
}