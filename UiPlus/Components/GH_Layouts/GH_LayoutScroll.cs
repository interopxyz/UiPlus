using Grasshopper.Kernel;
using Grasshopper.Kernel.Parameters;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

using UiPlus.Elements;

namespace UiPlus.Components
{
    public class GH_LayoutScroll : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the GH_LayoutScroll class.
        /// </summary>
        public GH_LayoutScroll()
          : base("UI Scroll Layout", "Scroll",
              "Description",
              "Ui", "Layout")
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
            pManager.AddGenericParameter("Elements", "E", "The Elements or Layouts", GH_ParamAccess.list);
            pManager.AddIntegerParameter("Horizontal ", "H", "Scroll in the horizontal direction?", GH_ParamAccess.item, 0);
            pManager[1].Optional = true;
            pManager.AddIntegerParameter("Vertical ", "V", "Scroll in the vertical direction?", GH_ParamAccess.item, 0);
            pManager[2].Optional = true;

            Param_Integer paramA = (Param_Integer)pManager[1];
            foreach (UiLayoutScroll.Visibility value in Enum.GetValues(typeof(UiLayoutScroll.Visibility)))
            {
                paramA.AddNamedValue(value.ToString(), (int)value);
            }

            Param_Integer paramB = (Param_Integer)pManager[2];
            foreach (UiLayoutScroll.Visibility value in Enum.GetValues(typeof(UiLayoutScroll.Visibility)))
            {
                paramB.AddNamedValue(value.ToString(), (int)value);
            }

        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Element", "E", "Ui Element | Scroll Layout", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            List<UiElement> elements = new List<UiElement>();
            if (!DA.GetDataList(0, elements)) return;

            int horizontal = 0;
            DA.GetData(1, ref horizontal);

            int vertical = 0;
            DA.GetData(2, ref vertical);

            UiLayoutScroll layout = new UiLayoutScroll();
            layout.Elements = elements;
            layout.Horizontal = (UiLayoutScroll.Visibility)horizontal;
            layout.Vertical = (UiLayoutScroll.Visibility)vertical;

            DA.SetData(0, layout);
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
                return Properties.Resources.UiPlus_Layouts_Scroller_01;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("51515f11-1077-4af2-b9f6-8abd408b2ecf"); }
        }
    }
}