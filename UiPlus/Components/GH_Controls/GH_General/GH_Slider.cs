using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

using UiPlus.Elements;

namespace UiPlus.Components.GH_Contraols
{
    public class GH_Slider : GH_EditBase
    {
        /// <summary>
        /// Initializes a new instance of the GH_Slider class.
        /// </summary>
        public GH_Slider()
          : base("UI Slider", "Slider",
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
            pManager.AddNumberParameter("Value", "V", "The slider value", GH_ParamAccess.item, 0.5);
            pManager[1].Optional = true;
            pManager.AddIntervalParameter("Bounds", "B", "The sliders upper and lower bounds", GH_ParamAccess.item, new Interval(0, 1));
            pManager[2].Optional = true;
            pManager.AddNumberParameter("Increment", "I", "The slider step increment", GH_ParamAccess.item, 0.1);
            pManager[3].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            base.RegisterOutputParams(pManager);
            pManager[0].Description = "Ui Element | Slider Control";
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            UiSlider control = new UiSlider();
            bool update = DA.GetData(0, ref control);
            if (update) Message = "Update";

            double val = 0.5;
            bool hasValue = DA.GetData(1, ref val);

            Interval domain = new Interval(0,1);
            bool hasDomain = DA.GetData(2, ref domain);

            double increment = 0.1;
            bool hasIncrement = DA.GetData(3, ref increment);

            if (hasValue) control.CurrentValue = val;
            if (hasDomain) control.Increment = increment;
            if (hasIncrement) control.Domain = domain;

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
                return Properties.Resources.UiPlus_Elements_Slider_01;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("9247a3db-d7c2-4997-933b-1dcc055940c6"); }
        }
    }
}