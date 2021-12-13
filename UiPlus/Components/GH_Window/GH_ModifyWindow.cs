using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

using UiPlus.Elements;

namespace UiPlus.Components
{
    public class GH_ModifyWindow : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the GH_ModifyWindow class.
        /// </summary>
        public GH_ModifyWindow()
          : base("Ui Modify Window", "Modify Win",
              "Modify visibility and position of a Ui Window",
              "Ui", "Window")
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
            pManager.AddGenericParameter("Window", "W", "A Ui Window element.", GH_ParamAccess.item);
            pManager[0].Optional = true;
            pManager.AddPointParameter("Position", "P", "Position the window", GH_ParamAccess.item);
            pManager[1].Optional = true;
            pManager.AddBooleanParameter("Hide Controls", "C", "Hide the window controls", GH_ParamAccess.item);
            pManager[2].Optional = true;
            pManager.AddBooleanParameter("Hide Title", "T", "Hide the title bar.", GH_ParamAccess.item);
            pManager[3].Optional = true;

        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Window", "W", "A Ui Window element.", GH_ParamAccess.item);

        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            UiWindow window = new UiWindow();
            if (!DA.GetData(0, ref window)) return;

            Point3d position = new Point3d();
            bool hasPosition = DA.GetData(1, ref position);

            bool controls = false;
            bool hasControls = DA.GetData(2, ref controls);

            bool title = false;
            bool hasTitle = DA.GetData(3, ref title);

            if (hasPosition) window.Position = position;
            if (hasControls) window.AreControlsVisible = controls;
            if (hasTitle) window.IsTitleBarVisible = title;

            DA.SetData(0, window);
            
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
                return Properties.Resources.UiPlus_Window_Modify_01;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("d5c696e0-2e0c-4baf-8416-b28a621af145"); }
        }
    }
}