using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

using Sd = System.Drawing;

using UiPlus.Elements;


namespace UiPlus.Components.GH_Modify
{
    public class GH_Offset : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the GH_Offset class.
        /// </summary>
        public GH_Offset()
          : base("Ui Margin/Padding", "Margin/Padding",
              "Set the margin or padding offset per side",
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
            pManager[0].Optional = true;
            pManager.AddBooleanParameter("Margin", "M", "If true the control's margin will be modified, if false the control's padding", GH_ParamAccess.item);
            pManager[1].Optional = true;
            pManager.AddNumberParameter("Top", "T", "The control top offset distance", GH_ParamAccess.item,0);
            pManager[2].Optional = true;
            pManager.AddNumberParameter("Left", "L", "The control left offset distance", GH_ParamAccess.item,0);
            pManager[3].Optional = true;
            pManager.AddNumberParameter("Bottom", "B", "The control bottom offset distance", GH_ParamAccess.item,0);
            pManager[4].Optional = true;
            pManager.AddNumberParameter("Right", "R", "The control right offset distance", GH_ParamAccess.item,0);
            pManager[5].Optional = true;

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

            double top = 0;
            DA.GetData(2, ref top);

            double left = 0;
            DA.GetData(3, ref left);

            double bottom = 0;
            DA.GetData(4, ref bottom);

            double right = 0;
            DA.GetData(5, ref right);

            if (margin)
            {
                control.SetMargin(top,left,bottom,right);
            }
            else
            {
                control.SetPadding(top, left, bottom, right);
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
                return Properties.Resources.UiPlus_Modify_Offset_01;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("0aefdfeb-9937-407f-bc95-b7bbae230900"); }
        }
    }
}