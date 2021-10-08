using Grasshopper.Kernel;
using Grasshopper.Kernel.Parameters;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

using UiPlus.Elements;


namespace UiPlus.Components.GH_Controls
{
    public class GH_DateTime : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the GH_DateTime class.
        /// </summary>
        public GH_DateTime()
          : base("UI Date Time", "Date Time",
              "Description",
              "Ui", "Elements")
        {
        }

        /// <summary>
        /// Set Exposure level for the component.
        /// </summary>
        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.tertiary; }
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTimeParameter("DateTime", "D", "The control datetime.", GH_ParamAccess.item, DateTime.Now);
            pManager[0].Optional = true;
            pManager.AddTextParameter("Format", "F", "Format", GH_ParamAccess.item, "06/15/2009, 01:00:00 PM");
            pManager[1].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Date Time", "D", "Ui Date Time", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            DateTime time = DateTime.Now;
            DA.GetData(0, ref time);

            string format = "06/15/2009, 01:00:00 PM";
            DA.GetData(1, ref format);

            UiDateTime control = new UiDateTime();
            control.Time = time;
            control.Format = format;

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
                return Properties.Resources.UiPlus_Elements_DateTime_01;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("1a468d1a-eb9d-4283-aa8a-a1ed6b3df331"); }
        }
    }
}