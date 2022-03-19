using Grasshopper.Kernel;
using Grasshopper.Kernel.Parameters;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

using UiPlus.Elements;


namespace UiPlus.Components.GH_Controls.GH_Static
{
    public class GH_Label : GH_EditBase
    {
        /// <summary>
        /// Initializes a new instance of the GH_Label class.
        /// </summary>
        public GH_Label()
          : base("Ui Label", "Label",
              "Display text in several visual styles",
              "Ui", "Display")
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
            pManager.AddTextParameter("Label", "L", "The control label.", GH_ParamAccess.item);
            pManager[1].Optional = true;
            pManager.AddIntegerParameter("Style", "S", "The text font style", GH_ParamAccess.item);
            pManager[2].Optional = true;

            Param_Integer param = (Param_Integer)pManager[2];
            foreach (UiElement.FontStyles value in Enum.GetValues(typeof(UiElement.FontStyles)))
            {
                param.AddNamedValue(value.ToString(), (int)value);
            }
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            base.RegisterOutputParams(pManager);
            pManager[0].Description = "Ui Element | Label Display";
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            UiLabel control = new UiLabel();
            if (DA.GetData(0, ref control)) Message = "Update";

            string text = "Title";
            bool hasText = DA.GetData(1, ref text);

            int font = 2;
            bool hasFont = DA.GetData(2, ref font);

            if(hasText) control.Content = text;
            if(hasFont) control.Font = ((UiElement.FontStyles)font);
            
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
                return Properties.Resources.UiPlus_Elements_Label_01;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("03c39864-7794-4e2e-9666-3965c9a8b0bc"); }
        }
    }
}