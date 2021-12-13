using Grasshopper.Kernel;
using Grasshopper.Kernel.Parameters;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

using UiPlus.Elements;

namespace UiPlus.Components
{
    public class GH_LayoutDock : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the GH_LayoutDock class.
        /// </summary>
        public GH_LayoutDock()
          : base("UI Dock Layout", "Dock",
              "Place elements and specify their dock direction",
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
            pManager.AddGenericParameter("Elements", "E", "The Elements or Layouts", GH_ParamAccess.list);
            pManager.AddIntegerParameter("Direction", "D", "A dock direction corresponding to each element", GH_ParamAccess.list, 0);
            pManager[1].Optional = true;

            Param_Integer param = (Param_Integer)pManager[1];
            foreach (UiLayoutDock.ObjectDirections value in Enum.GetValues(typeof(UiLayoutDock.ObjectDirections)))
            {
                param.AddNamedValue(value.ToString(), (int)value);
            }
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Element", "E", "Ui Element | Dock Layout", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            List<UiElement> elements = new List<UiElement>();
            if (!DA.GetDataList(0, elements)) return;

            List<int> directions = new List<int>();
            DA.GetDataList(1, directions);

            List<UiLayoutDock.ObjectDirections> objectDirections = new List<UiLayoutDock.ObjectDirections>();
            foreach (int dir in directions)
            {
                objectDirections.Add((UiLayoutDock.ObjectDirections)dir);
            }

            UiLayoutDock layout = new UiLayoutDock();
            layout.Elements = elements;
            layout.Directions = objectDirections;

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
                return Properties.Resources.UiPlus_Layouts_Dock_01;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("1272db78-5f77-4618-81f9-64ae4e56466f"); }
        }
    }
}