using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

using Sd = System.Drawing;

using UiPlus.Elements;
using Grasshopper.Kernel.Parameters;

namespace UiPlus.Components.GH_DataVis
{
    public class GH_TableCellGraphics : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the GH_DataGraphics class.
        /// </summary>
        public GH_TableCellGraphics()
          : base("Ui Cell Graphics", "Cell Graphics",
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
            pManager.AddGenericParameter("Data Series", "Ds", "The data series to modify.", GH_ParamAccess.item);
            pManager.AddColourParameter("Fill Colors", "F", "The background color", GH_ParamAccess.list);
            pManager[1].Optional = true;
            pManager.AddColourParameter("Text Colors", "T", "The foreground color", GH_ParamAccess.list);
            pManager[2].Optional = true;
            pManager.AddColourParameter("Stroke Colors", "S", "The stroke color", GH_ParamAccess.list);
            pManager[3].Optional = true;
            pManager.AddNumberParameter("Stroke Weights", "W", "The stroke weight", GH_ParamAccess.list);
            pManager[4].Optional = true;
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

            List<Sd.Color> fillColors = new List<Sd.Color>();
            bool hasFill = DA.GetDataList(1, fillColors);

            List<Sd.Color> textColors = new List<Sd.Color>();
            bool hasText = DA.GetDataList(2, textColors);

            List<Sd.Color> strokeColors = new List<Sd.Color>();
            bool hasStroke = DA.GetDataList(3, strokeColors);

            List<double> strokeWeights = new List<double>();
            bool hasWeight = DA.GetDataList(4, strokeWeights);

            dataSet.HasFillColors = hasFill;
            if (hasFill)
            {
                dataSet.HasPointGraphics = true;
                dataSet.FillColors = fillColors;
            }

            dataSet.HasTextColors = hasText;
            if (hasText)
            {
                dataSet.HasPointGraphics = true;
                dataSet.TextColors = textColors;
            }

            dataSet.HasStrokeColors = hasStroke;
            if (hasStroke)
            {
                dataSet.HasPointGraphics = true;
                dataSet.StrokeColors = strokeColors;
            }

            dataSet.HasStrokeWeights = hasWeight;
            if (hasWeight)
            {
                dataSet.HasPointGraphics = true;
                dataSet.StrokeWeights = strokeWeights;
            }


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
                return Properties.Resources.UiPlus_Modify_DataCell_01;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("62f300ed-62da-45ce-bcdd-2d25de35febd"); }
        }
    }
}