using Grasshopper.Kernel;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

using UiPlus.Elements;


namespace UiPlus.Components.GH_Controls
{
    public class GH_TextBox : GH_EditBase
    {
        /// <summary>
        /// Initializes a new instance of the GH_TextBox class.
        /// </summary>
        public GH_TextBox()
          : base("UI Text Box", "Text Box",
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
            pManager.AddTextParameter("Text", "T", "The control text.", GH_ParamAccess.item, "Click here to change text");
            pManager[1].Optional = true;
            pManager.AddBooleanParameter("Wrap", "W", "If true the text will wrap.", GH_ParamAccess.item, true);
            pManager[2].Optional = true;
            pManager.AddNumberParameter("Width", "X", "An optional limiting width for the text panel.", GH_ParamAccess.item);
            pManager[3].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            base.RegisterOutputParams(pManager);
            pManager[0].Description = "Ui Element | Text Box Control";
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            UiTextBox control = new UiTextBox();
            if (DA.GetData(0, ref control)) Message = "Update";

            string text = "Click here to change text";
            DA.GetData(1, ref text);

            bool wrap = true;
            DA.GetData(2, ref wrap);

            double width = 0;
            bool hasWidth = DA.GetData(3, ref width);

            control.Content = text;
            control.Wrap = wrap;
            if(hasWidth)control.Width = width;

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
                return Properties.Resources.UiPlus_Elements_TextBox_01;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("a503e372-3946-4c3f-a7e1-74d057c790aa"); }
        }
    }
}