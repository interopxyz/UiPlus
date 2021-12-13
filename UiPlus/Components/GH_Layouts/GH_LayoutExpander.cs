using Grasshopper.Kernel;
using Grasshopper.Kernel.Parameters;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

using UiPlus.Elements;

namespace UiPlus.Components
{
    public class GH_LayoutExpander : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the GH_LayoutExpander class.
        /// </summary>
        public GH_LayoutExpander()
          : base("UI Expander Layout", "Expander",
              "Place elements in an expandable layout",
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
            pManager.AddTextParameter("Title", "T", "The layout's title", GH_ParamAccess.item, " ");
            pManager[1].Optional = true;
            pManager.AddIntegerParameter("Direction", "D", "The expansion direction", GH_ParamAccess.item, 0);
            pManager[2].Optional = true;
            pManager.AddBooleanParameter("Closed", "C", "Closed if true", GH_ParamAccess.item, true);
            pManager[3].Optional = true;

            Param_Integer param = (Param_Integer)pManager[2];
            foreach (UiLayoutExpander.Directions value in Enum.GetValues(typeof(UiLayoutExpander.Directions)))
            {
                param.AddNamedValue(value.ToString(), (int)value);
            }
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Element", "E", "Ui Element | Expander Layout", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            List<UiElement> elements = new List<UiElement>();
            if (!DA.GetDataList(0, elements)) return;

            string title = "Title";
            DA.GetData(1, ref title);

            int direction = 0;
            DA.GetData(2, ref direction);

            bool closed = true;
            DA.GetData(3, ref closed);


            UiLayoutExpander layout = new UiLayoutExpander();
            layout.Elements = elements;
            layout.Title = title;
            layout.Direction = (UiLayoutExpander.Directions)direction;
            layout.Closed = closed;

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
                return Properties.Resources.UiPlus_Layouts_Expander_01;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("cb6f3cf8-7425-4f54-90c9-284c59739c44"); }
        }
    }
}