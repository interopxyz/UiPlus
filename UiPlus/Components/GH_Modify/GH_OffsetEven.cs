using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

using Sd = System.Drawing;

using UiPlus.Elements;


namespace UiPlus.Components.GH_Modify
{
    public class GH_OffsetEven : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the GH_OffsetEven class.
        /// </summary>
        public GH_OffsetEven()
          : base("Ui Even Margin/Padding", "Even Margin/Padding",
              "Uniformly set the margin or padding",
              "Ui", "Modify")
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
            pManager.AddGenericParameter("Element", "E", "The Element to update.", GH_ParamAccess.item);
            pManager.AddBooleanParameter("Margin", "M", "If true the control's margin will be modified, if false the control's padding", GH_ParamAccess.item);
            pManager[1].Optional = true;
            pManager.AddNumberParameter("Distance", "D", "The control offset distance", GH_ParamAccess.item,0);
            pManager[2].Optional = true;

        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Element", "E", "The updated Element.", GH_ParamAccess.item);
            pManager[0].Optional = true;
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            UiElement control = new UiElement();
            if (!DA.GetData(0, ref control)) return;

            bool margin = false;
            DA.GetData(1, ref margin);

            double distance = 0;
            DA.GetData(2, ref distance);

            if(margin)
            {
                control.SetMargin(distance, distance, distance, distance);
            }
            else
            {
                control.SetPadding(distance, distance, distance, distance);
            }

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
                return Properties.Resources.UiPlus_Modify_OffsetEven_01;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("c275c70a-477c-4b42-9ff5-5aa6ba001d2e"); }
        }
    }
}