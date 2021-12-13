using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

using Sd = System.Drawing;

using UiPlus.Elements;
using Grasshopper.Kernel.Parameters;

namespace UiPlus.Components.GH_Modify
{
    public class GH_Align : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the GH_Align class.
        /// </summary>
        public GH_Align()
          : base("Ui Resize", "Resize",
              "Change the horizontal alignment of an element",
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
            pManager.AddIntegerParameter("Alignment", "A", "The horizontal alignment", GH_ParamAccess.item);
            pManager[1].Optional = true;

            Param_Integer paramA = (Param_Integer)pManager[1];
            foreach (UiElement.Alignment value in Enum.GetValues(typeof(UiElement.Alignment)))
            {
                paramA.AddNamedValue(value.ToString(), (int)value);
            }
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

            int align = 0;
            bool hasAlign = DA.GetData(1, ref align);

            if (hasAlign) control.SetHorizontalAlignment((UiElement.Alignment)align);

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
                return Properties.Resources.UiPlus_Modify_Align_01;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("2887e98d-9e45-4744-94d6-1cc808f7aef5"); }
        }
    }
}