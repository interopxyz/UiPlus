using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

using UiPlus.Elements;

namespace UiPlus.Components.GH_DataVis
{
    public class GH_ChartDonut : GH_EditBase
    {
        /// <summary>
        /// Initializes a new instance of the GH_ChartDonut class.
        /// </summary>
        public GH_ChartDonut()
          : base("Ui Donut Chart", "Donut Chart",
              "Description",
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
            pManager.AddNumberParameter("Inner Radius", "R", "The inner radius of the donut chart. 0 = Pie", GH_ParamAccess.item, 50);
            pManager[2].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            base.RegisterOutputParams(pManager);
            pManager[0].Description = "Ui Element | Donut Chart";
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            UiChartDonut control = new UiChartDonut();
            bool update = DA.GetData(0, ref control);
            if(update)Message = "Update";

            List<UiDataSet> dataSets = new List<UiDataSet>();
            bool hasData = DA.GetDataList(1, dataSets); 

            double inner = 50;
            bool getInner = DA.GetData(2, ref inner);

            if(hasData) control.DataSets = dataSets;
            if (getInner) control.InnerRadius = inner;

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
                return Properties.Resources.UiPlus_Chart_Donut_01;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("1b007229-e4a1-470d-ae64-ded53cebe616"); }
        }
    }
}