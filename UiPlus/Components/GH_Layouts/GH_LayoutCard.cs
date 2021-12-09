using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

using UiPlus.Elements;

namespace UiPlus.Components
{
    public class GH_LayoutCard : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the GH_LayoutCard class.
        /// </summary>
        public GH_LayoutCard()
          : base("UI Card Layout", "Card",
              "Description",
              "Ui", "Layout")
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
            pManager.AddGenericParameter("Elements", "E", "The Elements or Layouts", GH_ParamAccess.list);
            pManager.AddTextParameter("Title", "T", "The layout's title", GH_ParamAccess.item, " ");
            pManager[1].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Element", "E", "Ui Element | Card Layout", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            List<UiElement> elements = new List<UiElement>();
            if (!DA.GetDataList(0, elements)) return;

            string title = " ";
            DA.GetData(1, ref title);

            UiLayoutCard layout = new UiLayoutCard();
            layout.Elements = elements;
            layout.Title = title;

            DA.SetData(0, layout);
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
                return Properties.Resources.UiPlus_Layouts_Card_01;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("da85c02a-2183-4b9e-b3aa-d1bf8a2db85b"); }
        }
    }
}