using Grasshopper.Kernel;
using Grasshopper.Kernel.Parameters;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

using UiPlus.Elements;


namespace UiPlus.Components.GH_Controls
{
    public class GH_DateTime : GH_EditBase
    {
        /// <summary>
        /// Initializes a new instance of the GH_DateTime class.
        /// </summary>
        public GH_DateTime()
          : base("UI Date Time", "Date Time",
              "Description",
              "Ui", "Control")
        {
        }

        /// <summary>
        /// Set Exposure level for the component.
        /// </summary>
        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.hidden; }
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            base.RegisterInputParams(pManager);
            pManager.AddTimeParameter("DateTime", "D", "The control date time.", GH_ParamAccess.item, DateTime.Now);
            pManager[1].Optional = true;
            pManager.AddTextParameter("Format", "F", "Format", GH_ParamAccess.item, "06/15/2009, 01:00:00 PM");
            pManager[2].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            base.RegisterOutputParams(pManager);
            pManager[0].Description = "Ui Element | Date Time Picker Control";
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            UiDateTime control = new UiDateTime();
            if (DA.GetData(0, ref control)) Message = "Update";

            DateTime time = DateTime.Now;
            bool hasTime = DA.GetData(1, ref time);

            string format = "06/15/2009, 01:00:00 PM";
            bool hasFormat = DA.GetData(2, ref format);

            if(hasTime) control.Time = time;
            if(hasFormat) control.Format = format;

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