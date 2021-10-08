using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

using Sd = System.Drawing;

using UiPlus.Elements;

namespace UiPlus.Components.GH_Controls
{
    public class GH_ColorCanvas : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the GH_ColorCanvas class.
        /// </summary>
        public GH_ColorCanvas()
          : base("UI Color Canvas", "Color Canvas",
              "Description",
              "Ui", "Elements")
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
            pManager.AddColourParameter("Color", "C", "The control's color.", GH_ParamAccess.item, Sd.Color.Teal);
            pManager[0].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Color Canvas", "C", "Ui Color Canvas", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Sd.Color color = Sd.Color.Teal;
            DA.GetData(0, ref color);

            UiColorCanvas control = new UiColorCanvas();
            control.Color = color;

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
                return Properties.Resources.UiPlus_Elements_ColorCanvas_01;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("a1766aaf-937e-4110-b482-4709ce5480bb"); }
        }
    }
}