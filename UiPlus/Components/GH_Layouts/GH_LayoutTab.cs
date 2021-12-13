using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

using UiPlus.Elements;

namespace UiPlus.Components
{
    public class GH_LayoutTabbed : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the GH_LayoutTabbed class.
        /// </summary>
        public GH_LayoutTabbed()
          : base("UI Tabbed Layout", "Tab",
              "Place elements in selected tabs",
              "Ui", "Layout")
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
            pManager.AddTextParameter("Names", "N", "The tab name associated with each element", GH_ParamAccess.list);
            pManager[1].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddGenericParameter("Element", "E", "Ui Element | Tab Layout", GH_ParamAccess.item);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            List<UiElement> elements = new List<UiElement>();
            if (!DA.GetDataList(0, elements)) return;

            List<string> names = new List<string>();
            if (!DA.GetDataList(1, names)) return;

            if (elements.Count != names.Count)
            {
                this.AddRuntimeMessage(GH_RuntimeMessageLevel.Warning, "The number of tab names must match the number of elements");
                return;
            }

            Dictionary<string, List<UiElement>> elementGroups = new Dictionary<string, List<UiElement>>();
            for(int i =0;i<elements.Count;i++)
            {
                if (!elementGroups.ContainsKey(names[i])) elementGroups.Add(names[i], new List<UiElement>());
                elementGroups[names[i]].Add(elements[i]);
            }

            UiLayoutTab layout = new UiLayoutTab();
            layout.ElementGroups = elementGroups;

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
                return Properties.Resources.UiPlus_Layouts_Tabs_01;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("58eb5800-0ed4-4e00-af71-0dd63188eb01"); }
        }
    }
}