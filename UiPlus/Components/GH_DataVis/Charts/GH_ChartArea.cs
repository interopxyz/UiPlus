using Grasshopper.Kernel;
using Grasshopper.Kernel.Parameters;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

using UiPlus.Elements;

namespace UiPlus.Components.GH_DataVis
{
    public class GH_ChartArea : GH_EditBase
    {
        /// <summary>
        /// Initializes a new instance of the GH_ChartArea class.
        /// </summary>
        public GH_ChartArea()
          : base("Ui Area Chart", "Area Chart",
              "Visualize multiple data series as an area chart",
              "Ui", "Chart")
        {
        }

        /// <summary>
        /// Set Exposure level for the component.
        /// </summary>
        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.quarternary; }
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            base.RegisterInputParams(pManager);
            pManager.AddGenericParameter("Data Series", "Ds", "The data series to visualize", GH_ParamAccess.list);
            pManager[1].Optional = true;
            pManager.AddBooleanParameter("Horizontal", "H", "If true, the bars will be displayed horizontally", GH_ParamAccess.item, true);
            pManager[2].Optional = true;
            pManager.AddIntegerParameter("Type", "T", "Graph Type", GH_ParamAccess.item, 0);
            pManager[3].Optional = true;
            pManager.AddNumberParameter("Smoothness", "S", "The curvature of the chart. (0 = line, 1 = spline)", GH_ParamAccess.item, 0);
            pManager[4].Optional = true;

            Param_Integer paramA = (Param_Integer)pManager[3];
            foreach (UiChartArea.GraphTypes value in Enum.GetValues(typeof(UiChartArea.GraphTypes)))
            {
                paramA.AddNamedValue(value.ToString(), (int)value);
            }
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            base.RegisterOutputParams(pManager);
            pManager[0].Description = "Ui Element | Area Chart";
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            UiChartArea control = new UiChartArea();
            bool update = DA.GetData(0, ref control);
            if (update) Message = "Update";

            List<UiDataSet> dataSets = new List<UiDataSet>();
            bool hasData = DA.GetDataList(1, dataSets);

            bool isHorizontal = true;
            bool hasHorizontal = DA.GetData(2, ref isHorizontal);

            int graphType = 0;
            bool hasType = DA.GetData(3, ref graphType);

            double smoothness = 0;
            bool hasSmoothness = DA.GetData(4, ref smoothness);

            if (hasData) control.DataSets = dataSets;
            if (hasHorizontal) control.IsHorizontal = isHorizontal;
            if (hasType) control.GraphType = (UiChartArea.GraphTypes)graphType;
            if (hasSmoothness) control.Smoothness = smoothness;

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
                return Properties.Resources.UiPlus_Chart_Area_01;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("213c1d99-033c-434b-b7fa-85e77b37dbe1"); }
        }
    }
}