using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

using UiPlus.Elements;

namespace UiPlus.Components.GH_Controls.GH_Scroll
{
    public class GH_ScrollInteger : GH_EditBase
    {
        /// <summary>
        /// Initializes a new instance of the GH_ScrollInteger class.
        /// </summary>
        public GH_ScrollInteger()
          : base("Ui Scroll Integer", "Scroll Integer",
              "Scroll through integers between a specified interval",
              "Ui", "Control")
        {
        }

        /// <summary>
        /// Set Exposure level for the component.
        /// </summary>
        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.primary; }
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            base.RegisterInputParams(pManager);
            pManager.AddTextParameter("Label", "L", "The control label.", GH_ParamAccess.item, "");
            pManager[1].Optional = true;
            pManager.AddIntegerParameter("Integer", "I", "The control's current integer.", GH_ParamAccess.item, 0);
            pManager[2].Optional = true;
            pManager.AddBooleanParameter("Wrap", "W", "If true the values will cycle. If false the values will be capped at the min and max.", GH_ParamAccess.item, false);
            pManager[3].Optional = true;
            pManager.AddIntegerParameter("Increment", "I", "The control's step integer increment.", GH_ParamAccess.item);
            pManager[4].Optional = true;
            pManager.AddIntervalParameter("Bounds", "B", "The control's low and high integer values.", GH_ParamAccess.item);
            pManager[5].Optional = true;

        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            base.RegisterOutputParams(pManager);
            pManager[0].Description = "Ui Element | Integer Scroller Control";
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            UiScrollInteger control = new UiScrollInteger();
            if (DA.GetData(0, ref control)) Message = "Update";

            string label = "";
            bool hasLabel = DA.GetData(1, ref label);

            int number = 0;
            bool hasNumber = DA.GetData(2, ref number);

            bool wrap = false;
            bool isWrap = DA.GetData(3, ref wrap);

            int increment = 1;
            bool hasIncrement = DA.GetData(4, ref increment);

            Interval domain = new Interval(0, 10);
            bool hasDomain = DA.GetData(5, ref domain);

            if (hasLabel) control.Label = label;
            if (isWrap) control.Wrap = wrap;
            if (hasNumber) control.Value = number;
            if (hasIncrement) control.Increment = increment;
            if (hasDomain) control.Minimum = (int)domain.Min;
            if (hasDomain) control.Maximum = (int)domain.Max;

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
                return Properties.Resources.UiPlus_Elements_Integer_01;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("f694f7dd-edd3-4b09-9f5e-59f0063d40c3"); }
        }
    }
}