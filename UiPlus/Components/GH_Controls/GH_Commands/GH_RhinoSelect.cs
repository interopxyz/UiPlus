using Grasshopper.Kernel;
using Grasshopper.Kernel.Parameters;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

using UiPlus.Elements;

namespace UiPlus.Components.GH_Controls.GH_Commands
{
    public class GH_RhinoSelect : GH_EditBase
    {
        /// <summary>
        /// Initializes a new instance of the GH_RhinoSelect class.
        /// </summary>
        public GH_RhinoSelect()
          : base("UI Rhino Select Button", "Rh Sel Btn",
              "Description",
              "Ui", "Command")
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
            pManager.AddTextParameter("Label", "L", "The control label.", GH_ParamAccess.item, "Get");
            pManager[1].Optional = true;
            pManager.AddIntegerParameter("Filter", "F", "Optionally filter the selection type", GH_ParamAccess.item, 0);
            pManager[2].Optional = true;

            Param_Integer param = (Param_Integer)pManager[2];
            foreach (UiButtonSelect.SelectionTypes value in Enum.GetValues(typeof(UiButtonSelect.SelectionTypes)))
            {
                param.AddNamedValue(value.ToString(), (int)value);
            }

        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            base.RegisterOutputParams(pManager);
            pManager[0].Description = "Ui Element | Rhino Select Button";
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            UiButtonSelect control = new UiButtonSelect();
            if (DA.GetData(0, ref control)) Message = "Update";

            string label = "Get";
            bool hasLabel = DA.GetData(1, ref label);

            int filter = 0;
            bool hasCommands = DA.GetData(2, ref filter);

            if (hasLabel) control.Label = label;
            if (hasCommands) control.SelectionType = (UiButtonSelect.SelectionTypes) filter;

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
                return Properties.Resources.UiPlus_Elements_CommandGet_01;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("ff4ffba6-dd00-425c-bae3-69bb7784082e"); }
        }
    }
}