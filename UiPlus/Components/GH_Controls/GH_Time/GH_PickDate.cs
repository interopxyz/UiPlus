using Grasshopper.Kernel;
using Grasshopper.Kernel.Parameters;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

using UiPlus.Elements;

namespace UiPlus.Components.GH_Controls
{
    public class GH_PickDate : GH_EditBase
    {
        /// <summary>
        /// Initializes a new instance of the GH_PickDate class.
        /// </summary>
        public GH_PickDate()
          : base("Ui Pick Date", "Pick Date",
              "Select or specify a date",
              "Ui", "Control")
        {
        }

        /// <summary>
        /// Set Exposure level for the component.
        /// </summary>
        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.quarternary; }
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            base.RegisterInputParams(pManager);
            pManager.AddTimeParameter("DateTime", "D", "The control date time.", GH_ParamAccess.item, DateTime.Now);
            pManager[1].Optional = true;
            pManager.AddBooleanParameter("Long", "L", "Is the date in a long format", GH_ParamAccess.item, false);
            pManager[2].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            base.RegisterOutputParams(pManager);
            pManager[0].Description = "Ui Element | Date Picker Control";
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            UiPickDate control = new UiPickDate();
            if (DA.GetData(0, ref control)) Message = "Update";

            DateTime date = DateTime.Now;
            bool hasDate = DA.GetData(1, ref date);

            bool mode = false;
            bool hasMode = DA.GetData(2, ref mode);

            if(hasDate) control.Date = date;
            if(hasMode) control.Long = mode;

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
                return Properties.Resources.UiPlus_Elements_PickDate_01;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("b9443085-cdb5-4d1c-8ff8-f9bd87185a90"); }
        }
    }
}