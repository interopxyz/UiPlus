using Grasshopper.Kernel;
using Grasshopper.Kernel.Parameters;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

using UiPlus.Elements;

namespace UiPlus.Components.GH_Controls
{
    public class GH_Calendar : GH_EditBase
    {
        /// <summary>
        /// Initializes a new instance of the GH_Calendar class.
        /// </summary>
        public GH_Calendar()
          : base("UI Calendar", "Calendar",
              "Description",
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
            pManager.AddBooleanParameter("Single", "S", "If true, only a single date can be selected", GH_ParamAccess.item, false);
            pManager[2].Optional = true;
            pManager.AddIntegerParameter("Mode", "M", "Set calendar mode", GH_ParamAccess.item, 0);
            pManager[3].Optional = true;

            Param_Integer param = (Param_Integer)Params.Input[3];
            param.AddNamedValue("Month", 0);
            param.AddNamedValue("Year", 1);
            param.AddNamedValue("Decade", 2);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            base.RegisterOutputParams(pManager);
            pManager[0].Description = "Ui Element | Calendar Control";
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            UiCalendar control = new UiCalendar();
            if (DA.GetData(0, ref control)) Message = "Update";

            DateTime time = DateTime.Now;
            DA.GetData(1, ref time);

            bool single = false;
            DA.GetData(2, ref single);

            int mode = 0;
            DA.GetData(3, ref mode);

            control.Time = time;
            control.SelectSingle = single;
            control.DisplayMode = (UiCalendar.Modes)mode;

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
                return Properties.Resources.UiPlus_Elements_Calendar_01;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("261dfab2-44d1-4f42-913c-e15d2e370df8"); }
        }
    }
}