using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

using UiPlus.Elements;

namespace UiPlus.Components.GH_Contraols
{
    public class GH_Radio : GH_EditBase
    {
        /// <summary>
        /// Initializes a new instance of the GH_Radio class.
        /// </summary>
        public GH_Radio()
          : base("UI Radio", "Radio",
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
            pManager.AddTextParameter("Label", "L", "The control label.", GH_ParamAccess.item, "");
            pManager[1].Optional = true;
            pManager.AddBooleanParameter("State", "S", "The control's boolean status. If multiple buttons in a group are true the last one will be true.", GH_ParamAccess.item, false);
            pManager[2].Optional = true;
            pManager.AddTextParameter("Group", "G", "The Radio group.", GH_ParamAccess.item, "Group 1");
            pManager[3].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            base.RegisterOutputParams(pManager);
            pManager[0].Description = "Ui Element | Radio Control";
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            UiRadio control = new UiRadio();
            if (DA.GetData(0, ref control)) Message = "Update";

            string label = "";
            DA.GetData(1, ref label);

            bool state = false;
            DA.GetData(2, ref state);

            string group = "Group 1";
            DA.GetData(3, ref group);

            control.Label = label;
            control.State = state;
            control.Group = group;

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
                return Properties.Resources.UiPlus_Elements_Radio_01;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("423355d6-6e00-4904-b59e-81f60847ed55"); }
        }
    }
}