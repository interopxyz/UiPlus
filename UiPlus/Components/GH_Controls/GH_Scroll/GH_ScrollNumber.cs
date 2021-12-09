using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

using UiPlus.Elements;


namespace UiPlus.Components.GH_Controls
{
    public class GH_ScrollNumber : GH_EditBase
    {
        /// <summary>
        /// Initializes a new instance of the GH_ScrollNumber class.
        /// </summary>
        public GH_ScrollNumber()
          : base("UI Scroll Number", "Scroll Number",
              "Description",
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
            pManager.AddNumberParameter("Number", "N", "The control's current number.", GH_ParamAccess.item, 0);
            pManager[1].Optional = true;
            pManager.AddBooleanParameter("Wrap", "W", "If true the values will cycle. If false the values will be capped at the min and max.", GH_ParamAccess.item, false);
            pManager[2].Optional = true;
            pManager.AddIntegerParameter("Digits", "D", "The significant digits", GH_ParamAccess.item, 3);
            pManager[3].Optional = true;
            pManager.AddNumberParameter("Increment", "I", "The control's step increment.", GH_ParamAccess.item);
            pManager[4].Optional = true;
            pManager.AddIntervalParameter("Bounds", "B", "The control's low and high values.", GH_ParamAccess.item);
            pManager[5].Optional = true;

        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            base.RegisterOutputParams(pManager);
            pManager[0].Description = "Ui Element | Number Scroller Control";
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
                UiScrollNumber control = new UiScrollNumber();
                if (DA.GetData(0, ref control)) Message = "Update";

            double number = 0;
            DA.GetData(1, ref number);

            bool wrap = false;
            DA.GetData(2, ref wrap);

            int digits = 3;
            DA.GetData(3, ref digits);

            double increment = 1;
            bool hasIncrement = DA.GetData(4, ref increment);

            Interval domain = new Interval(0,10);
            bool hasDomain = DA.GetData(5, ref domain);

                control.Value = number;
                if (hasIncrement) control.Increment = increment;
                if (hasDomain) control.Minimum = domain.Min;
                if (hasDomain) control.Maximum = domain.Max;
                control.Digits = digits;

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
                return Properties.Resources.UiPlus_Elements_ScrollNumber_01;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("0c8763b6-bfa2-4282-a380-25b625e2aeed"); }
        }
    }
}