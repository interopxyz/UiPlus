using Grasshopper.Kernel;
using Grasshopper.Kernel.Parameters;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

using UiPlus.Elements;
namespace UiPlus.Components.GH_DataVis
{
    public class GH_ChartGrid : GH_EditBase
    {
        /// <summary>
        /// Initializes a new instance of the GH_ChartGrid class.
        /// </summary>
        public GH_ChartGrid()
          : base("Ui Grid Chart", "Grid Chart",
              "Quickly display a list of data series in a visual table",
              "Ui", "Chart")
        {
        }

        /// <summary>
        /// Set Exposure level for the component.
        /// </summary>
        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.tertiary; }
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            base.RegisterInputParams(pManager);
            pManager.AddGenericParameter("Data Series", "Ds", "The data series to visualize", GH_ParamAccess.list);
            pManager[1].Optional = true;
            pManager.AddBooleanParameter("Titles", "T", "If true, titles are visible", GH_ParamAccess.item, true);
            pManager[2].Optional = true;

        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            base.RegisterOutputParams(pManager);
            pManager[0].Description = "Ui Element | Data Grid";
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            UiChartGrid control = new UiChartGrid();
            bool update = DA.GetData(0, ref control);
            if (update) Message = "Update";

            List<UiDataSet> dataSets = new List<UiDataSet>();
            bool hasData = DA.GetDataList(1, dataSets);

            bool titles = true;
            bool hasTitles = DA.GetData(2, ref titles);

            if (hasData) control.DataSets = dataSets;
            if (hasTitles) control.HasTitles = titles;

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
                return Properties.Resources.UiPlus_Chart_Grid_01;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("f50aaa3e-124a-4770-b08b-e4a94f9ce9b7"); }
        }
    }
}