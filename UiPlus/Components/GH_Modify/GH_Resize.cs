using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

using Sd = System.Drawing;

using UiPlus.Elements;

namespace UiPlus.Components.GH_Modify
{
    public class GH_Resize : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the GH_Resize class.
        /// </summary>
        public GH_Resize()
          : base("Ui Resize", "Resize",
              "Resize an element",
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
            pManager.AddNumberParameter("Width", "W", "The control width", GH_ParamAccess.item);
            pManager[1].Optional = true;
            pManager.AddNumberParameter("Height", "H", "The control height", GH_ParamAccess.item);
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

            double width = 300;
            bool hasWidth = DA.GetData(1, ref width);

            double height = 300;
            bool hasHeight = DA.GetData(2, ref height);

            if (hasWidth & hasHeight)
            {
                control.SetSizing(width, height);
            }
            else if (hasWidth)
            {
                control.SetSizing(width, 0);
            }
            else if (hasHeight)
            {
                control.SetSizing(0,height);
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
                return Properties.Resources.UiPlus_Modify_Resize_01;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("f1e94c92-337b-4b20-8421-7f82a98ceb5a"); }
        }
    }
}