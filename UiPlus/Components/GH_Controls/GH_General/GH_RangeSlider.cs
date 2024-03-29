﻿using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

using UiPlus.Elements;

namespace UiPlus.Components.GH_Controls
{
    public class GH_RangeSlider : GH_EditBase
    {
        /// <summary>
        /// Initializes a new instance of the GH_RangeSlider class.
        /// </summary>
        public GH_RangeSlider()
          : base("Ui Range Slider", "Range Slider",
              "Click and drag handles to return the interval between",
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
            pManager.AddIntervalParameter("Selection", "S", "The  upper and lower bounds of the selection", GH_ParamAccess.item, new Interval(0.25,0.75));
            pManager[2].Optional = true;
            pManager.AddIntervalParameter("Bounds", "B", "The upper and lower bounds of the slider", GH_ParamAccess.item, new Interval(0, 1));
            pManager[3].Optional = true;
            pManager.AddNumberParameter("Increment", "I", "The step increment of the slider", GH_ParamAccess.item, 0.1);
            pManager[4].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            base.RegisterOutputParams(pManager);
            pManager[0].Description = "Ui Element | Range Slider Control";
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            UiRangeSlider control = new UiRangeSlider();
            if (DA.GetData(0, ref control)) Message = "Update";

            string label = "";
            bool hasLabel = DA.GetData(1, ref label);

            Interval selection = new Interval(0, 1);
            bool hasSelection = DA.GetData(2, ref selection);

            Interval domain = new Interval(0, 1);
            bool hasDomain = DA.GetData(3, ref domain);

            double increment = 0.1;
            bool hasIncrement = DA.GetData(4, ref increment);

            if (hasLabel) control.Label = label;
            if (hasSelection) control.CurrentValue = selection;
            if (hasDomain) control.Domain = domain;
            if (hasIncrement) control.Increment = increment;

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
                return Properties.Resources.UiPlus_Elements_RangeSlider_01;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("d63958f1-3696-48f1-b3cc-7cd03e9ac122"); }
        }
    }
}