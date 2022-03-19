using Grasshopper.Kernel;
using Grasshopper.Kernel.Parameters;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Drawing;
using UiPlus.Elements;

namespace UiPlus.Components.GH_DataVis
{
    public class GH_ChartGauge : GH_EditBase
    {
        /// <summary>
        /// Initializes a new instance of the GH_ChartGauge class.
        /// </summary>
        public GH_ChartGauge()
          : base("Ui Gauge Chart", "Gauge Chart",
              "Visualize a value within a specified domain as a gauge",
              "Ui", "Chart")
        {
        }

        /// <summary>
        /// Set Exposure level for the component.
        /// </summary>
        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.quinary; }
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            base.RegisterInputParams(pManager);
            pManager.AddNumberParameter("Value", "V", "The value of the chart", GH_ParamAccess.item);
            pManager[1].Optional = true;
            pManager.AddIntervalParameter("Domain", "D", "The low and high value of the chart", GH_ParamAccess.item);
            pManager[2].Optional = true;
            pManager.AddBooleanParameter("Circular", "C", "If true, the chart is circular.", GH_ParamAccess.item);
            pManager[3].Optional = true;
            pManager.AddNumberParameter("Inner Radius", "R", "The inner radius of the chart.", GH_ParamAccess.item);
            pManager[4].Optional = true;
            pManager.AddColourParameter("Low Color", "L", "The color at the lowest value of the chart", GH_ParamAccess.item);
            pManager[5].Optional = true;
            pManager.AddColourParameter("High Color", "H", "The color at the highest value of the chart", GH_ParamAccess.item);
            pManager[6].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            base.RegisterOutputParams(pManager);
            pManager[0].Description = "Ui Element | Gauge Chart";
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            UiChartGauge control = new UiChartGauge();
            bool update = DA.GetData(0, ref control);
            if (update) Message = "Update";

            double val = 25;
            bool hasValue = DA.GetData(1, ref val);

            Interval domain = new Interval(0,100);
            bool hasDomain = DA.GetData(2, ref domain);

            bool isCircular = true;
            bool hasCircular = DA.GetData(3, ref isCircular);

            double radius = 0;
            bool hasRadius = DA.GetData(4, ref radius);

            Color low = Color.Empty;
            bool hasLow = DA.GetData(5, ref low);

            Color high = Color.Empty;
            bool hasHigh = DA.GetData(6, ref high);


            if (hasDomain) control.Minimum = domain.Min;
            if (hasDomain) control.Maximum = domain.Max;
            if (hasValue) control.Value = val;
            if (hasCircular) control.IsCircular = isCircular;
            if (hasRadius) control.InnerRadius = radius;
            if (hasLow) control.StartColor = low;
            if (hasHigh) control.EndColor = high;

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
                return Properties.Resources.UiPlus_Chart_Gauge_01;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("88f83bae-8022-4c5a-9d4e-9ad436b62d6f"); }
        }
    }
}