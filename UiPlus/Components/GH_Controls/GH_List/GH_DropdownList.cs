using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

using UiPlus.Elements;

namespace UiPlus.Components.GH_Controls
{
    public class GH_DropdownList : GH_EditBase
    {
        /// <summary>
        /// Initializes a new instance of the GH__DropdownList class.
        /// </summary>
        public GH_DropdownList()
          : base("UI Dropdown List", "Dropdown List",
              "Select a single item from a dropdown list of text items",
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
            pManager.AddTextParameter("Values", "V", "The control's values", GH_ParamAccess.list);
            pManager.AddIntegerParameter("Index", "I", "The index of the selected list item", GH_ParamAccess.item);
            pManager[2].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            base.RegisterOutputParams(pManager);
            pManager[0].Description = "Ui Element | Dropdown List Control";
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            UiDropdownList control = new UiDropdownList();
            if (DA.GetData(0, ref control)) Message = "Update";

            List<string> items = new List<string>();
            if (!DA.GetDataList(1, items)) return;

            int index = -1;
            bool isSelected = DA.GetData(2, ref index);

            control.Items = items;
            if (isSelected) control.Index = index;

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
                return Properties.Resources.UiPlus_Elements_Dropdown_01;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("22644080-be78-471c-a2f4-e7a4b0649cc7"); }
        }
    }
}