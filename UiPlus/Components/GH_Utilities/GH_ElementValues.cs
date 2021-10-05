using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

namespace UiPlus.Components.GH_Utilities
{
    public class GH_ElementValues : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the GH_ElementValues class.
        /// </summary>
        public GH_ElementValues()
          : base("UI Values", "Get Values",
              "Description",
              "Ui", "Elements")
        {
        }

        /// <summary>
        /// Set Exposure level for the component.
        /// </summary>
        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.septenary; }
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {

        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
                                        case ("RangeSlider"):
                                MahApps.Metro.Controls.RangeSlider C7 = (MahApps.Metro.Controls.RangeSlider)E.Layout.Children[0];
            if (!keys.Contains(E.Element.Name))
            {
                C7.UpperValueChanged -= (o, e) => { ExpireSolution(true); };
                C7.UpperValueChanged += (o, e) => { ExpireSolution(true); };
                C7.LowerValueChanged -= (o, e) => { ExpireSolution(true); };
                C7.LowerValueChanged += (o, e) => { ExpireSolution(true); };
            }
            OutPut.Append(new GH_ObjectWrapper(new Interval(C7.LowerValue, C7.UpperValue)), P);
            break;

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
                return null;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("023a26ba-3e69-45ea-bee5-44cd867a6004"); }
        }
    }
}