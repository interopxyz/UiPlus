using Grasshopper.Kernel;
using Grasshopper.Kernel.Parameters;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

using UiPlus.Elements;

namespace UiPlus.Components
{
    public class GH_LayoutWrap : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the GH_LayoutWrap class.
        /// </summary>
        public GH_LayoutWrap()
          : base("UI Wrap Layout", "Wrap",
              "Description",
              "Ui", "Layout")
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
            pManager.AddGenericParameter("Elements", "E", "The Elements (Controls, Layouts, Charts) to Layout", GH_ParamAccess.list);
            pManager.AddBooleanParameter("Horizontal", "H", "The layout's flow direction", GH_ParamAccess.item, true);
            pManager[1].Optional = true;
            pManager.AddIntegerParameter("Direction", "D", "The alignment direction", GH_ParamAccess.item, 0);
            pManager[2].Optional = true;

            Param_Integer param = (Param_Integer)pManager[2];
            foreach (UiLayoutWrap.Directions value in Enum.GetValues(typeof(UiLayoutWrap.Directions)))
            {
                param.AddNamedValue(value.ToString(), (int)value);
            }
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Element", "E", "Ui Element | Wrapper Layout", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            List<UiElement> elements = new List<UiElement>();
            if (!DA.GetDataList(0, elements)) return;

            bool isHorizontal = true;
            DA.GetData(1, ref isHorizontal);

            int direction = 1;
            DA.GetData(2, ref direction);

            UiLayoutWrap layout = new UiLayoutWrap();
            layout.Elements = elements;
            layout.IsHorizontal = isHorizontal;
            layout.Direction = (UiLayoutWrap.Directions)direction;

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
                return Properties.Resources.UiPlus_Layouts_Wrap_01;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("dd4e56e8-3fb3-4bd4-9560-c4ea105b40bc"); }
        }
    }
}