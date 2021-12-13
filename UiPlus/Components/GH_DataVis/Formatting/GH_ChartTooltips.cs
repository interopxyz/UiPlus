using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

using Sd = System.Drawing;

using UiPlus.Elements;
using Grasshopper.Kernel.Parameters;

namespace UiPlus.Components.GH_DataVis.Formatting
{
    public class GH_ChartTooltips : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the GH_DataTooltips class.
        /// </summary>
        public GH_ChartTooltips()
          : base("Ui Chart Tooltips", "Ds Tooltips",
              "Apply tooltips to a chart and modify",
              "Ui", "Chart")
        {
        }

        /// <summary>
        /// Set Exposure level for the component.
        /// </summary>
        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.hidden; }
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddGenericParameter("Chart Element", "Ec", "The chart element to update.", GH_ParamAccess.item);
            pManager.AddIntegerParameter("Format", "F", "The chart tooltip format", GH_ParamAccess.item, 1);
            pManager[1].Optional = true;
            pManager.AddBooleanParameter("Show Set", "S", "Show the set name", GH_ParamAccess.item, false);
            pManager[2].Optional = true;

            Param_Integer paramA = (Param_Integer)pManager[1];
            foreach (UiDataVis.Tooltips value in Enum.GetValues(typeof(UiDataVis.Tooltips)))
            {
                paramA.AddNamedValue(value.ToString(), (int)value);
            }
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

            int tooltip = 1;
            DA.GetData(1, ref tooltip);

            bool show = false;
            DA.GetData(2, ref show);

            chart.SetTooltip((UiDataVis.Tooltips)tooltip,show);

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
                return Properties.Resources.UiPlus_Modify_DataPopups_01;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("d9a0389d-3ae5-4cd1-817c-16c19416afaf"); }
        }
    }
}