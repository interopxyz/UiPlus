using Grasshopper.Kernel;
using Grasshopper.Kernel.Parameters;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

using UiPlus.Elements;

namespace UiPlus.Components.GH_Controls.GH_Static
{
    public class GH_RhinoViewer : GH_EditBase
    {
        /// <summary>
        /// Initializes a new instance of the GH_RhinoViewer class.
        /// </summary>
        public GH_RhinoViewer()
          : base("UI Rhino Viewer", "Rhino Viewer",
              "Display a Rhino viewport",
              "Ui", "Display")
        {
        }

        /// <summary>
        /// Set Exposure level for the component.
        /// </summary>
        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.secondary; }
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            base.RegisterInputParams(pManager);
            pManager.AddTextParameter("Named View", "N", "The name of a user created Rhino Named View.", GH_ParamAccess.item, "Perspective");
            pManager[1].Optional = true;
            pManager.AddIntegerParameter("Display Mode", "D", "The viewport display mode.", GH_ParamAccess.item,3);
            pManager[2].Optional = true;
            pManager.AddIntegerParameter("Projection", "P", "The camera projection type.", GH_ParamAccess.item,1);
            pManager[3].Optional = true;

            Param_Integer paramA = (Param_Integer)pManager[2];
            foreach (UiRhinoViewer.DisplayModes value in Enum.GetValues(typeof(UiRhinoViewer.DisplayModes)))
            {
                paramA.AddNamedValue(value.ToString(), (int)value);
            }

            Param_Integer paramB = (Param_Integer)pManager[3];
            foreach (UiRhinoViewer.ProjectionModes value in Enum.GetValues(typeof(UiRhinoViewer.ProjectionModes)))
            {
                paramB.AddNamedValue(value.ToString(), (int)value);
            }

        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            base.RegisterOutputParams(pManager);
            pManager[0].Description = "Ui Element | Rhino Viewer";
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            UiRhinoViewer control = new UiRhinoViewer();
            if (DA.GetData(0, ref control)) Message = "Update";

            string viewport = "Perspective";
            bool hasView = DA.GetData(1, ref viewport);

            int displayMode = 3;
            bool hasDisplayMode = DA.GetData(2, ref displayMode);

            int projectionMode = 0;
            bool hasProjectionMode = DA.GetData(3, ref projectionMode);

            if (hasView) control.Viewport = viewport;

            if (hasDisplayMode) control.DisplayMode = (UiRhinoViewer.DisplayModes)displayMode;
            if (hasProjectionMode) control.ProjectionMode = (UiRhinoViewer.ProjectionModes)projectionMode;

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
                return Properties.Resources.UiPlus_Elements_RhinoViewer_01;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("bec661dd-a535-402a-a323-c4a0c9612eab"); }
        }
    }
}