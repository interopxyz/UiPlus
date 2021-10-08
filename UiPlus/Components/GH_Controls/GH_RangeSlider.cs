using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

using UiPlus.Elements;

namespace UiPlus.Components.GH_Controls
{
    public class GH_RangeSlider : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the GH_RangeSlider class.
        /// </summary>
        public GH_RangeSlider()
          : base("UI Range Slider", "Range Slider",
              "Description",
              "Ui", "Elements")
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
            pManager.AddIntervalParameter("Selection", "S", "The selection's upper and lower bounds", GH_ParamAccess.item, new Interval(0.25,0.75));
            pManager[0].Optional = true;
            pManager.AddIntervalParameter("Bounds", "B", "The sliders upper and lower bounds", GH_ParamAccess.item, new Interval(0, 1));
            pManager[1].Optional = true;
            pManager.AddNumberParameter("Increment", "I", "The slider step increment", GH_ParamAccess.item, 0.1);
            pManager[2].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Range Slider", "R", "Ui Range Slider", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            Interval selection = new Interval(0, 1);
            DA.GetData(0, ref selection);

            Interval domain = new Interval(0, 1);
            DA.GetData(1, ref domain);

            double increment = 0.1;
            DA.GetData(2, ref increment);

            UiRangeSlider control = new UiRangeSlider();
            control.CurrentValue = selection;
            control.Increment = increment;
            control.Domain = domain;

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