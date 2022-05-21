using Grasshopper.Kernel;
using Grasshopper.Kernel.Parameters;
using Rhino.Geometry;
using System;
using System.Collections.Generic;
using System.Windows.Interop;
using UiPlus.Elements;

namespace UiPlus.Components
{
    public class GH_Window : GH_Component
    {
        UiWindow window = new UiWindow();

        /// <summary>
        /// Initializes a new instance of the GH_Window class.
        /// </summary>
        public GH_Window()
          : base("Ui Window", "Window",
              "Populate a Ui Window's elements and open it",
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
            pManager.AddGenericParameter("Elements", "E", "A list of Controls, Layouts, Charts and other Ui Element", GH_ParamAccess.list);
            pManager.AddTextParameter("Title", "T", "The window title", GH_ParamAccess.item, "Ui+ Viewer");
            pManager[1].Optional = true;
            pManager.AddIntegerParameter("Owner", "O", "The application that owns the new Window. (In Rhino Inside Revit, use Rhino as the owner.)", GH_ParamAccess.item,1);
            pManager[2].Optional = true;
            pManager.AddBooleanParameter("Scroll", "S", "If true a scroll bar will be added to the main window", GH_ParamAccess.item,false);
            pManager[3].Optional = true;
            pManager.AddBooleanParameter("Launch", "L", "Opens the Window", GH_ParamAccess.item, false);

            Param_Integer param = (Param_Integer)pManager[2];
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
            pManager.AddGenericParameter("Window", "W", "A Window Ui Element", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            List<UiElement> elements = new List<UiElement>();
            if (!DA.GetDataList(0, elements)) return;

            string title = "Ui+ Viewer";
            bool hasTitle = DA.GetData(1, ref title);

            int mode = 0;
            DA.GetData(2, ref mode);

            bool scroll = false;
            bool hasScroll = DA.GetData(3, ref scroll);

            bool launch = false;
            if (!DA.GetData(4, ref launch)) return;

            if (launch)
            {
                window.Close();
            }

            window.Arrangment = (UiWindow.Arrangments)mode;
            window.Elements = elements;

            if (hasScroll)
            {
                if (scroll)
                {
                    window.ScrollVisible = UiWindow.ScrollVisibily.Visible;
                        }
                else
                {
                    window.ScrollVisible = UiWindow.ScrollVisibily.Hidden;
                }
                    }
            else
            {
                window.ScrollVisible = UiWindow.ScrollVisibily.Auto;
            }

            if (launch)
            {
                window.Launch();
            }

            if (hasTitle) window.Title = title;

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