using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

using UiPlus.Elements;

namespace UiPlus.Components.GH_Controls.GH_Commands
{
    public class GH_RhinoCommand : GH_EditBase
    {
        /// <summary>
        /// Initializes a new instance of the GH_RhinoCommand class.
        /// </summary>
        public GH_RhinoCommand()
          : base("UI Rhino Command Button", "Rh Cmd Btn",
              "Run a specified command when clicked",
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
            pManager.AddTextParameter("Label", "L", "The control label.", GH_ParamAccess.item, "Run");
            pManager[1].Optional = true;
            pManager.AddTextParameter("Commands", "C", "A list of Rhinoscript Commands", GH_ParamAccess.list);
            pManager[2].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            base.RegisterOutputParams(pManager);
            pManager[0].Description = "Ui Element | Rhino Command Button";
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            UiButtonCommand control = new UiButtonCommand();
            if (DA.GetData(0, ref control)) Message = "Update";

            string label = "Run";
            bool hasLabel = DA.GetData(1, ref label);

            List<string> commands = new List<string>();
            bool hasCommands = DA.GetDataList(2, commands);

            if(hasLabel) control.Label = label;
            if(hasCommands) control.Commands = commands;

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
                return Properties.Resources.UiPlus_Elements_CommandRun_01;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("b8c04b0a-b230-4a2f-9cef-dc9a7e8e7135"); }
        }
    }
}