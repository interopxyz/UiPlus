using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

using Sd = System.Drawing;

using UiPlus.Elements;

namespace UiPlus.Components.GH_DataVis
{
    public class GH_DataSeriesLabel : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the GH_DataLabel class.
        /// </summary>
        public GH_DataSeriesLabel()
          : base("Ui Data Series Labels", "Ds Labels",
              "Apply labels to a data series and modify",
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
            pManager.AddGenericParameter("Data Series", "Ds", "A Ui Data Series object to modify.", GH_ParamAccess.item);
            pManager.AddColourParameter("Color", "C", "The label color", GH_ParamAccess.item);
            pManager[1].Optional = true;
            pManager.AddTextParameter("Prefix", "P", "Optional label prefix", GH_ParamAccess.item);
            pManager[2].Optional = true;
            pManager.AddTextParameter("Suffix", "S", "S", GH_ParamAccess.item);
            pManager[3].Optional = true;

        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Data Set", "Ds", "The updated data series.", GH_ParamAccess.item);
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

            Sd.Color color = Sd.Color.Black;
            bool hasColor = DA.GetData(1,ref color);

            string prefix = "";
            bool hasPrefix = DA.GetData(2, ref prefix);
            string suffix = "";
            bool hasSuffix = DA.GetData(3, ref suffix);

            dataSet.HasLabelColor = hasColor;
            if (hasColor) dataSet.LabelColor = color;

            if (hasPrefix) dataSet.LabelPrefix = prefix;
            if (hasSuffix) dataSet.LabelSuffix = suffix;

            dataSet.HasLabel = true;

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
                return Properties.Resources.UiPlus_Modify_DataLabels_01;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("f605d25b-ea78-4226-bbc7-c222bc2dab81"); }
        }
    }
}