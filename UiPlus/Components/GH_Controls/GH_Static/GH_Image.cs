using Grasshopper.Kernel;
using Grasshopper.Kernel.Parameters;
using Rhino.Geometry;
using System;
using System.Collections.Generic;

using Sd = System.Drawing;

using UiPlus.Elements;

namespace UiPlus.Components.GH_Controls.GH_Static
{
    public class GH_Image : GH_EditBase
    {
        /// <summary>
        /// Initializes a new instance of the GH_Image class.
        /// </summary>
        public GH_Image()
          : base("Ui Image", "Image",
              "Display an image viewer",
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
            pManager.AddGenericParameter("Bitmap", "B", "The control text.", GH_ParamAccess.item);
            pManager[1].Optional = true;
            pManager.AddIntegerParameter("Fitting", "F", "The image fitting mode", GH_ParamAccess.item, 1);
            pManager[2].Optional = true;

            Param_Integer param = (Param_Integer)pManager[2];
            foreach (UiImage.FittingModes value in Enum.GetValues(typeof(UiImage.FittingModes)))
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
            pManager[0].Description = "Ui Element | Image Display";
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            UiImage control = new UiImage();
            if (DA.GetData(0, ref control)) Message = "Update";

            Sd.Bitmap bitmap = null;
            bool hasBitmap = DA.GetData(1, ref bitmap);

            int fitting = 0;
            bool hasFitting = DA.GetData(2, ref fitting);

            if(hasBitmap) control.Content = bitmap;
            if(hasFitting) control.FittingMode = (UiImage.FittingModes) fitting;

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
                return Properties.Resources.UiPlus_Elements_Image_01;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("5d32a0ee-350c-4892-a968-0448b83e392b"); }
        }
    }
}