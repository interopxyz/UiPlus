using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

using Sd = System.Drawing;

using UiPlus.Elements;
using Grasshopper.Kernel.Parameters;

namespace UiPlus.Components.GH_DataVis.Formatting
{
    public class GH_ChartAxis : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the GH_DataAxis class.
        /// </summary>
        public GH_ChartAxis()
          : base("Ui Modify Chart", "Modify Chart",
              "Description",
              "Ui", "Chart")
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
            pManager.AddGenericParameter("Chart Element", "Ec", "The chart element to update.", GH_ParamAccess.item);
            pManager.AddTextParameter("X Axis Title", "X", "The chart x axis title", GH_ParamAccess.item);
            pManager[1].Optional = true;
            pManager.AddTextParameter("Y Axis Title", "Y", "The chart y axis title", GH_ParamAccess.item);
            pManager[2].Optional = true;

            pManager.AddIntegerParameter("Legend Location", "L", "The chart legend location", GH_ParamAccess.item, 0);
            pManager[3].Optional = true;

            Param_Integer paramA = (Param_Integer)pManager[3];
            foreach (UiDataVis.Locations value in Enum.GetValues(typeof(UiDataVis.Locations)))
            {
                paramA.AddNamedValue(value.ToString(), (int)value);
            }

            pManager.AddIntegerParameter("Format", "F", "The chart tooltip format", GH_ParamAccess.item, 0);
            pManager[4].Optional = true;

            Param_Integer paramB = (Param_Integer)pManager[4];
            foreach (UiDataVis.Tooltips value in Enum.GetValues(typeof(UiDataVis.Tooltips)))
            {
                paramB.AddNamedValue(value.ToString(), (int)value);
            }

            pManager.AddBooleanParameter("Show Series", "S", "Show the series name", GH_ParamAccess.item, false);
            pManager[5].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Element", "Ec", "The updated element.", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            UiDataVis chart = new UiDataVis();
            if (!DA.GetData(0, ref chart)) return;

            //AXIS LABELS
            string xLabel = "";
            bool hasXaxis = DA.GetData(1, ref xLabel);

            string yLabel = "";
            bool hasYaxis = DA.GetData(2, ref yLabel);

            if (hasXaxis) chart.SetAxisX(xLabel);
            if (hasYaxis) chart.SetAxisY(yLabel);

            //LEGEND
            int location = 0;
            DA.GetData(3, ref location);

            chart.SetLegend((UiDataVis.Locations)location);

            //TOOLTIPS
            int tooltip = 0;
            DA.GetData(4, ref tooltip);

            bool show = false;
            DA.GetData(5, ref show);

            chart.SetTooltip((UiDataVis.Tooltips)tooltip, show);

            DA.SetData(0, chart);
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
                return Properties.Resources.UiPlus_Modify_ModChart_01;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("e2c94505-2245-4785-8721-3c3903082a85"); }
        }
    }
}