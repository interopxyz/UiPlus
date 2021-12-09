using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

using UiPlus.Elements;

namespace UiPlus.Components
{
    public class GH_LayoutGrid : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the GH_LayoutGrid class.
        /// </summary>
        public GH_LayoutGrid()
          : base("UI Grid Layout", "Grid",
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
            pManager.AddIntegerParameter("Columns", "C", "The column index of each element", GH_ParamAccess.list);
            pManager[1].Optional = true;
            pManager.AddIntegerParameter("Rows", "R", "The row index of each element", GH_ParamAccess.list);
            pManager[2].Optional = true;

        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Element", "E", "Ui Element | Grid Layout", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            List<UiElement> elements = new List<UiElement>();
            if (!DA.GetDataList(0, elements)) return;

            List<int> columns = new List<int>();
            if (!DA.GetDataList(1, columns)) return;

            List<int> rows = new List<int>();
            if (!DA.GetDataList(2, rows)) return;

            if (elements.Count != columns.Count)
            {
                this.AddRuntimeMessage(GH_RuntimeMessageLevel.Warning, "The number of column indices must match the number of elements");
                return;
            }

            if (elements.Count != rows.Count)
            {
                this.AddRuntimeMessage(GH_RuntimeMessageLevel.Warning, "The number of row indices must match the number of elements");
                return;
            }

            Dictionary<Tuple<int, int>, UiElement> locations = new Dictionary<Tuple<int, int>, UiElement>();

            for(int i = 0; i < elements.Count; i++)
            {
                if(!locations.ContainsKey(new Tuple<int, int>(columns[i], rows[i])))
                {
                    locations.Add(new Tuple<int, int>(columns[i], rows[i]), elements[i]);
                }
            }

            UiLayoutGrid layout = new UiLayoutGrid();
            layout.LocationElements = locations;

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
                return Properties.Resources.UiPlus_Layouts_Grid_01;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("5b4090ae-e418-47f2-969e-fbd2d80fb9c9"); }
        }
    }
}