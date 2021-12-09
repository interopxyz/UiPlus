using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

using UiPlus.Elements;

namespace UiPlus.Components
{
    public class GH_LayoutPlacement : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the GH_LayoutPlacement class.
        /// </summary>
        public GH_LayoutPlacement()
          : base("UI Placement Layout", "Placement",
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
            pManager.AddGenericParameter("Elements", "E", "The Elements or Layouts", GH_ParamAccess.list);
            pManager.AddPointParameter("Position", "P", "The placement location X and Y", GH_ParamAccess.list);
            pManager[1].Optional = true;
            pManager.AddNumberParameter("Width", "W", "The placement panel width", GH_ParamAccess.item, 600);
            pManager[2].Optional = true;
            pManager.AddNumberParameter("Height", "H", "The placement panel height", GH_ParamAccess.item, 600);
            pManager[3].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Element", "E", "Ui Element | Placement Layout", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            List<UiElement> elements = new List<UiElement>();
            if (!DA.GetDataList(0, elements)) return;

            List<Point3d> locations = new List<Point3d>();
            if (!DA.GetDataList(1, locations)) return;

            double width = 600;
            DA.GetData(2, ref width);

            double height = 600;
            DA.GetData(3, ref height);

            if (elements.Count != locations.Count)
            {
                this.AddRuntimeMessage(GH_RuntimeMessageLevel.Warning, "The number of locations must match the number of elements");
                return;
            }

            Dictionary<UiElement, Point3d> collections = new Dictionary<UiElement, Point3d>();
            for(int i = 0; i < elements.Count; i++)
            {
                if (!collections.ContainsKey(elements[i])) collections.Add(elements[i], locations[i]);
            }

            UiLayoutPlacement layout = new UiLayoutPlacement();
            layout.LocationElements = collections;
            layout.Width = width;
            layout.Height = height;


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
                return Properties.Resources.UiPlus_Layouts_Placement_01;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("c5cc8b44-b6d2-4562-b008-f09a5392284e"); }
        }
    }
}