using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

using Sd = System.Drawing;

using UiPlus.Elements;
using Grasshopper.Kernel.Parameters;

namespace UiPlus.Components.GH_DataVis
{
    public class GH_DataSeriesGraphics : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the GH_DataSetGraphics class.
        /// </summary>
        public GH_DataSeriesGraphics()
          : base("Ui Data Series Graphics", "Ds Graphics",
              "Apply graphics to an overall data series",
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
            pManager.AddGenericParameter("Data Series", "Ds", "The data series to modify.", GH_ParamAccess.item);
            pManager.AddColourParameter("Primary Color", "P", "The series's primary color", GH_ParamAccess.item);
            pManager[1].Optional = true;
            pManager.AddColourParameter("Accent Color", "A", "The series's accent color", GH_ParamAccess.item);
            pManager[2].Optional = true;
            pManager.AddNumberParameter("Weight", "W", "The stroke weight", GH_ParamAccess.item);
            pManager[3].Optional = true;
            pManager.AddIntegerParameter("Marker", "M", "The marker type", GH_ParamAccess.item);
            pManager[4].Optional = true;

            Param_Integer paramA = (Param_Integer)pManager[4];
            foreach (UiDataSet.Markers value in Enum.GetValues(typeof(UiDataSet.Markers)))
            {
                paramA.AddNamedValue(value.ToString(), (int)value);
            }

        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Data Series", "Ds", "The updated data series.", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            UiDataSet dataSet = new UiDataSet();
            if (!DA.GetData(0, ref dataSet)) return;
            dataSet = new UiDataSet(dataSet);

            Sd.Color primary = Constants.DarkGray();
            bool hasPrimary = DA.GetData(1, ref primary);

            Sd.Color accent = Sd.Color.Transparent;
            bool hasAccent = DA.GetData(2, ref accent);

            double strokeWeight = 1;
            bool hasWeight = DA.GetData(3, ref strokeWeight);

            int marker = 0;
            bool hasMark = DA.GetData(4, ref marker);

            dataSet.HasPrimaryColor = hasPrimary;
            if (hasPrimary) dataSet.PrimaryColor = primary;
            dataSet.HasSecondaryColor = hasAccent;
            if (hasAccent) dataSet.SecondaryColor = accent;
            dataSet.HasWeight = hasWeight;
            if (hasWeight) dataSet.Weight = strokeWeight;
            if (hasMark) dataSet.Marker = (UiDataSet.Markers)marker;


            DA.SetData(0, dataSet);
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
                return Properties.Resources.UiPlus_Modify_DataSetGraphics_01;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("fb2a4f38-f1c4-4347-9778-5edb1184e9e2"); }
        }
    }
}