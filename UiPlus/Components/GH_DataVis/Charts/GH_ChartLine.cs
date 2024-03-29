﻿using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

using UiPlus.Elements;

namespace UiPlus.Components.GH_DataVis
{
    public class GH_ChartLine : GH_EditBase
    {
        /// <summary>
        /// Initializes a new instance of the GH_ChartLine class.
        /// </summary>
        public GH_ChartLine()
          : base("Ui Line Chart", "Line Chart",
              "Visualize multiple data series of numbers as a line chart",
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
            pManager.AddBooleanParameter("Horizontal", "H", "If true, the lines will be displayed horizontally", GH_ParamAccess.item, true);
            pManager[2].Optional = true;
            pManager.AddNumberParameter("Smoothness", "S", "The curvature of the chart. (0 = line, 1 = spline)", GH_ParamAccess.item, 0);
            pManager[3].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            base.RegisterOutputParams(pManager);
            pManager[0].Description = "Ui Element | Line Chart";
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            UiChartLine control = new UiChartLine();
            bool update = DA.GetData(0, ref control);
            if (update) Message = "Update";

            List<UiDataSet> dataSets = new List<UiDataSet>();
            bool hasData = DA.GetDataList(1, dataSets);

            bool isHorizontal = false;
            bool hasHorizontal = DA.GetData(2, ref isHorizontal);

            double smooth = 0;
            bool getSmooth = DA.GetData(3, ref smooth);

            if (hasHorizontal) control.IsHorizontal = isHorizontal;
            if (hasData) control.DataSets = dataSets;
            if (getSmooth) control.Smoothness = smooth;

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
                return Properties.Resources.UiPlus_Chart_Line_01;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("234215d2-d61d-4fe7-a5fd-396dfd81338f"); }
        }
    }
}