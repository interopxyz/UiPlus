﻿using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

using UiPlus.Elements;


namespace UiPlus.Components.GH_Controls
{
    public class GH_ScrollText : GH_EditBase
    {
        /// <summary>
        /// Initializes a new instance of the GH_ScrollText class.
        /// </summary>
        public GH_ScrollText()
          : base("Ui Scroll Text", "Scroll Text",
              "Scroll through a list of text items",
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
            pManager.AddTextParameter("Values", "V", "The values to select from.", GH_ParamAccess.list);
            pManager[2].Optional = true;
            pManager.AddIntegerParameter("Index", "I", "The control selected index.", GH_ParamAccess.item, 0);
            pManager[3].Optional = true;
            pManager.AddBooleanParameter("Wrap", "W", "If true the values will cycle. If false the values will be capped at the min and max.", GH_ParamAccess.item, false);
            pManager[4].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            base.RegisterOutputParams(pManager);
            pManager[0].Description = "Ui Element | Text Scroller Control";
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            UiScrollText control = new UiScrollText();
            if (DA.GetData(0, ref control)) Message = "Update";

            string label = "";
            bool hasLabel = DA.GetData(1, ref label);

            List<string> values = new List<string>();
            bool hasValues = DA.GetDataList(2, values);

            int index = 0;
            bool hasIndex = DA.GetData(3, ref index);

            bool wrap = false;
            bool hasWrap = DA.GetData(4, ref wrap);

            if (hasLabel) control.Label = label;
            if (hasIndex) control.Index = index;
            if (hasWrap) control.Wrap = wrap;
            if (hasValues) if(values.Count>0)control.Values = values;

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
                return Properties.Resources.UiPlus_Elements_ScrollText_01;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("eab675eb-02f0-4794-bd5b-5885fb34a7ab"); }
        }
    }
}