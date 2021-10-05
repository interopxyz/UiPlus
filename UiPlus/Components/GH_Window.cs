﻿using Grasshopper.Kernel;
using Grasshopper.Kernel.Parameters;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Windows.Interop;
using UiPlus.Elements;
using UiPlus.Viewer;

namespace UiPlus.Components
{
    public class GH_Window : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the GH_Window class.
        /// </summary>
        public GH_Window()
          : base("UI Window", "Window",
              "Description",
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
            pManager.AddGenericParameter("Elements", "E", "The Elements or Layouts", GH_ParamAccess.list);
            pManager.AddIntegerParameter("Owner", "O", "The application that owns the new Window", GH_ParamAccess.item,1);
            pManager[1].Optional = true;
            pManager.AddBooleanParameter("Launch", "L", "Opens the Window", GH_ParamAccess.item, false);

            Param_Integer param = (Param_Integer)pManager[1];
            foreach (UiWindow.Arrangments value in Enum.GetValues(typeof(UiWindow.Arrangments)))
            {
                param.AddNamedValue(value.ToString(), (int)value);
            }
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
            List<UiElement> elements = new List<UiElement>();
            if (!DA.GetDataList(0, elements)) return;

            int mode = 0;
            DA.GetData(1, ref mode);

            bool launch = false;
            if (!DA.GetData(2, ref launch)) return;

            UiWindow window = new UiWindow(elements);
            window.Arrangment = (UiWindow.Arrangments)mode;

            if (launch) window.Launch();
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
                return Properties.Resources.UiPlus_Window_Launch_01;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("3aa66b3a-c732-4d0d-96e3-f3c5c43b0661"); }
        }
    }
}